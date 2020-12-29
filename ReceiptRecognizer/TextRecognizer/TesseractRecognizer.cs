using System;
using System.Drawing;
using System.Threading.Tasks;
using Tesseract;

namespace Recognizer.TextRecognizer
{
    public class TesseractRecognizer : ITextRecognizer, IDisposable
    {
        private const string TessDataPath = @"..\ReceiptRecognizer\assets\tessdata\best";
        //private const string TessDataPath = @".\assets\tessdata\best";

        private TesseractEngine _tesseractEngine;

        public Page RecognizedPage { get; private set; }

        public TesseractRecognizer()
        {
            _tesseractEngine = new TesseractEngine(TessDataPath, "lit", EngineMode.Default);
        }

        public void Dispose()
        {
            _tesseractEngine.Dispose();
        }

        public async Task<string> GetText(Bitmap image)
        {
            try
            {
                if (_tesseractEngine.IsDisposed)
                {
                    _tesseractEngine = new TesseractEngine(TessDataPath, "lit", EngineMode.Default);
                }

                using var img = Pix.LoadFromMemory(ImageToByte(image));
                var imageToRecognize = img
                    .ConvertRGBToGray()
                    //.BinarizeSauvola(10, 0.35f, false)
                    //.BinarizeOtsuAdaptiveThreshold(10, 10, 0, 0, 0)
                    .Deskew()
                    ;

                var processTask = new Task<Page>(() => _tesseractEngine.Process(imageToRecognize));
                processTask.Start();

                RecognizedPage = await processTask;

                return RecognizedPage.GetText();
            }
            catch (TesseractException ex)
            {
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
