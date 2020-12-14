using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace Recognizer.TextRecognizer
{
    public class TesseractRecognizer : ITextRecognizer
    {
        private const string TessDataPath = @"..\ReceiptRecognizer\assets\tessdata\best";
        private bool isFirstTry = true;

        public async Task<string> GetText(Bitmap image, string language)
        {
            try
            {
                var dir = new DirectoryInfo(TessDataPath);
                using var ocrEngineBest = new TesseractEngine(TessDataPath, language, EngineMode.Default);
                using var img = Pix.LoadFromMemory(ImageToByte(image));
                var imgGray = img.ConvertRGBToGray();

                var extractText = new Task<string>(() => ocrEngineBest.Process(imgGray).GetText());
                extractText.Start();

                return await extractText;
            } 
            catch (TesseractException ex)
            {
                if (isFirstTry)
                {
                    return await GetText(image, "lit");
                }
                throw ex;
            }
        }

        private byte[] ImageToByte(Image img)
        {
            var converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
    }
}
