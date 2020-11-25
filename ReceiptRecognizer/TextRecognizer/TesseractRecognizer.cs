using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Tesseract;

namespace ReceiptRecognizer.TextRecognizer
{
    public class TesseractRecognizer : ITextRecognizer
    {
        private const string TessDataPath = @".\assets\tessdata\best";

        public string GetText(Bitmap image)
        {
            using var ocrEngineBest = new TesseractEngine(TessDataPath, "lit", EngineMode.Default);
            using var img = Pix.LoadFromMemory(ImageToByte(image));
            var imgGray = img.ConvertRGBToGray();

            return ocrEngineBest.Process(imgGray).GetText();
        }

        private byte[] ImageToByte(Image img)
        {
            var converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
    }
}
