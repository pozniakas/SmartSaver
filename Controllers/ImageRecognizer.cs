using System;
using System.Diagnostics;
using System.IO;
using Tesseract;

namespace SmartSaver.Controllers
{
    public class ImageRecognizer
    {
        //Install-Package Tesseract -Version 4.1.0-beta1
        private readonly string TessDataPath = @".\assets\tessdata\best";

        public void Recognize()
        {
            DirectoryInfo dir = new DirectoryInfo(@"..\..\..\assets\Receipts");

            foreach (var imgPath in dir.GetFiles("r1.jpg"))
            {
                Debug.WriteLine(imgPath);

                using var ocrEngineBest = new TesseractEngine(TessDataPath, "lit", EngineMode.Default);
                //ocrEngineBest.SetVariable("tessedit_char_whitelist", "0123456789,.");

                using var img = Pix.LoadFromFile(imgPath.ToString());
                var imgGray = img.ConvertRGBToGray();

                var res = ocrEngineBest.Process(imgGray).GetText();

                var destPath = imgPath.Directory.Parent.FullName + @"\receiptsTxt\" + imgPath.Name + ".txt";
                Debug.WriteLine(destPath);
                
                File.WriteAllText(destPath, res);
            }
        }
    }
}
