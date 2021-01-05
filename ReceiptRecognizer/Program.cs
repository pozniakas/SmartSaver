using DbEntities.Entities;
using Recognizer.ObjectRecognizer;
using Recognizer.TextRecognizer;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recognizer
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            var recognizer = new ReceiptRecognizer(new EmguLargestAreaRecognizer(), new TesseractRecognizer());
            var image = Image.FromFile(@"C:\Users\aleks\Desktop\test\setOfImages\IMG_20201228_180236x.jpg");
            var transaction = await recognizer.Recognize(image);
            Console.WriteLine(transaction.ConvertToString());

            //await TestReco();
        }

        static async Task TestReco()
        {
            var workingDirectory = new DirectoryInfo(@"C:\Users\aleks\Desktop\test\setOfImages");

            using var recognizer = new ReceiptRecognizer(new EmguLargestAreaRecognizer(), new TesseractRecognizer());
            var csvText = "";
            var width = 1000;

            foreach (var imageFile in workingDirectory.GetFiles("*x.jpg"))
            {
                csvText += imageFile.Name + ";";
                var stopWatch = new Stopwatch();
                stopWatch.Start();

                using var image = Image.FromFile(imageFile.FullName);
                var fileName = new string(imageFile.FullName.TakeWhile(c => !c.Equals('.')).ToArray());
                Console.WriteLine(fileName);

                var resized = recognizer.GetResizedImage(image, width);
                var cropped = await recognizer.GetCroppedImage(resized);

                //resized.Save(fileName + ".min.jpg");
                //cropped.Save(fileName + ".crop.jpg");
                csvText += cropped.Width + ";" + cropped.Height + ";";

                // Text recognition
                var page = await recognizer.ReadImage(cropped);
                //Console.WriteLine($"Mean confidence: {page.GetMeanConfidence()}");
                csvText += page.GetMeanConfidence() + ";";
                //Console.WriteLine($"BEGIN TEXT;\n{page.GetText()}\nEND OF TEXT;");

                var receiptText = new string(page.GetText());
                page.Dispose();

                var transaction = recognizer.TextToTransaction(receiptText);
                File.WriteAllText(imageFile.FullName + ".txt", receiptText, Encoding.UTF8);

                //Console.WriteLine(transaction.ConvertToString());
                //Console.WriteLine($" >>> Recognition time: {stopWatch.Elapsed}");
                csvText += transaction.ConvertToCsvRow() + ";";
                csvText += stopWatch.Elapsed + ";";
                csvText += stopWatch.Elapsed.TotalMilliseconds + "\n";
            }

            File.WriteAllText(workingDirectory.FullName + @"\results" + width + ".csv", csvText, Encoding.UTF8);
        }

        public static string ConvertToString(this Transaction transaction)
        {
            return string.Concat(
                "Transaction: \n\t",
                transaction.CounterParty, "\n\t",
                transaction.Amount, "\n\t",
                transaction.TrTime.ToString("yyyy-MM-dd HH:mm:ss"), "\n\t"
                );
        }

        public static string ConvertToCsvRow(this Transaction transaction)
        {
            return string.Concat(
                transaction.Amount, ";",
                transaction.TrTime.ToString("yyyy-MM-dd HH:mm:ss"), ";",
                transaction.CounterParty
                );
        }
    }
}
