using DbEntities.Entities;
using Recognizer.TextRecognizer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Recognizer
{
    public class ReceiptRecognizer
    {
        private IObjectRecognizer _objectRecognizer;
        private ITextRecognizer _textRecognizer;

        public ReceiptRecognizer(IObjectRecognizer objectRecognizer, ITextRecognizer textRecognizer)
        {
            _objectRecognizer = objectRecognizer;
            _textRecognizer = textRecognizer;
        }

        public async Task RecognizeTestImages()
        {
            try
            {
                var tasks = new List<Task<Transaction>>();

                var dir = new DirectoryInfo(@"..\..\..\assets\Receipts");
                foreach (var imgPath in dir.GetFiles("*.jpg"))
                {
                    using var originalImage = Image.FromFile(imgPath.FullName);
                    tasks.Add( Recognize(originalImage, imgPath) );
                }

                await Task.WhenAll(tasks);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        public async Task<Transaction> Recognize(Stream stream)
        {
            using var originalImage = Image.FromStream(stream);
            return await Recognize(originalImage);
        }

        public async Task<Transaction> Recognize(Image originalImage, FileInfo file = null)
        {
            if (file != null)
            {
                Console.WriteLine(file.Name);
            }

            try
            {
                Statistics.AddRecognizingImage();

                var resizedImage = GetResizedImage(originalImage);

                // Console.WriteLine($"{file.Name} Cropping...");
                var croppedImage = await _objectRecognizer.GetRecognizedImage(resizedImage);

                // Console.WriteLine($"{file.Name} Getting text...");
                var receiptText = await _textRecognizer.GetText(croppedImage, "lit");
                var transaction = TextToTransaction(receiptText);

                if (file != null)
                {
                    var fileName = file.Directory.Parent.FullName + @"\Result\" + file.Name + "cropped.jpg";
                    croppedImage.Save(fileName);
                }
                // Console.WriteLine($"{file.Name} Completed successfully. Amount = {transaction.Amount}");

                return transaction;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"EXCEPTION: {exception}");
                throw exception;
            }
        }

        public Transaction TextToTransaction(string text)
        {
            var match = Regex.Match(text, "^.*(moketi|mokėti|suma).*", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            var amount = Regex.Match(match.Value, @"((0(\.|,)\d\d)|([1-9]\d*((\.|,)\d\d)?))", RegexOptions.IgnoreCase);

            return new Transaction
            {
                Amount = decimal.TryParse(amount.Value, out decimal parsedAmount) ? parsedAmount : 0
            };
        }

        private Bitmap GetResizedImage(Image image)
        {
            FixImageOrientation(image);

            double aspectRatio = (double)image.Width / image.Height;
            return new Bitmap(image, 500, (int)(500 / aspectRatio));
        }

        private void FixImageOrientation(Image src)
        {
            const int ExifOrientationId = 0x112;
            PropertyItem pi = src.PropertyItems.Select(x => x).FirstOrDefault(x => x.Id == ExifOrientationId);
            if (pi == null) return;

            byte orientation = pi.Value[0];
            switch (orientation)
            {
                case 2: src.RotateFlip(RotateFlipType.RotateNoneFlipX);   break;
                case 3: src.RotateFlip(RotateFlipType.RotateNoneFlipXY);  break;
                case 4: src.RotateFlip(RotateFlipType.RotateNoneFlipY);   break;
                case 5: src.RotateFlip(RotateFlipType.Rotate90FlipX);     break;
                case 6: src.RotateFlip(RotateFlipType.Rotate90FlipNone);  break;
                case 7: src.RotateFlip(RotateFlipType.Rotate90FlipY);     break;
                case 8: src.RotateFlip(RotateFlipType.Rotate90FlipXY);    break;
                default: break;
            }

            src.RemovePropertyItem(ExifOrientationId);
        }
    }
}
