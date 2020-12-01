using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace ReceiptRecognizer.TextRecognizer
{
    public class TesseractRecognizer : ITextRecognizer
    {
        private const string TessDataPath = @".\assets\tessdata\best";

        public async Task<string> GetText(Bitmap image)
        {
            using var ocrEngineBest = new TesseractEngine(TessDataPath, "lit", EngineMode.Default);
            using var img = Pix.LoadFromMemory(ImageToByte(image));
            var imgGray = img.ConvertRGBToGray();

            var extractText = new Task<string>(() => ocrEngineBest.Process(imgGray).GetText());
            extractText.Start();

            return await extractText;
        }

        private byte[] ImageToByte(Image img)
        {
            var converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
    }
}
