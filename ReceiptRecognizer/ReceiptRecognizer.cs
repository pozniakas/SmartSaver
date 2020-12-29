using DbEntities.Entities;
using Recognizer.TextRecognizer;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tesseract;

namespace Recognizer
{
    public class ReceiptRecognizer : IDisposable
    {
        private IObjectRecognizer _objectRecognizer;
        private ITextRecognizer _textRecognizer;

        public ReceiptRecognizer(IObjectRecognizer objectRecognizer, ITextRecognizer textRecognizer)
        {
            _objectRecognizer = objectRecognizer;
            _textRecognizer = textRecognizer;
        }

        public async Task<Transaction> Recognize(Stream stream)
        {
            using var originalImage = Image.FromStream(stream);
            return await Recognize(originalImage);
        }

        public async Task<Transaction> Recognize(Image originalImage)
        {
            try
            {
                var resized = GetResizedImage(originalImage, 1000);
                var cropped = await GetCroppedImage(resized);

                var page = await ReadImage(cropped);
                var receiptText = new string(page.GetText());
                page.Dispose();

                var transaction = TextToTransaction(receiptText);

                return transaction;
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"Exception (on recognition): \n{exception.Message}\n{exception.StackTrace}");
                throw exception;
            }
        }

        public Transaction TextToTransaction(string text)
        {
            var interpreter = new ReceiptTextInterpreter(text);

            return new Transaction
            {
                TrTime = interpreter.DateTime,
                Amount = - interpreter.Amount,
                CounterParty = interpreter.CounterParty,
                Details = interpreter.Details
            };
        }

        public async Task<Page> ReadImage(Bitmap image)
        {
            await _textRecognizer.GetText(image);
            return _textRecognizer.RecognizedPage;
        }

        public async Task<Bitmap> GetCroppedImage(Bitmap image)
        {
            return await _objectRecognizer.GetRecognizedImage(image);
        }

        public Bitmap GetResizedImage(Image image, int width = 500)
        {
            FixImageOrientation(image);

            double aspectRatio = (double)image.Width / image.Height;
            return new Bitmap(image, width, (int)(width / aspectRatio));
        }

        private void FixImageOrientation(Image src)
        {
            const int ExifOrientationId = 0x112;
            PropertyItem pi = src.PropertyItems.Select(x => x).FirstOrDefault(x => x.Id == ExifOrientationId);
            if (pi == null) return;

            byte orientation = pi.Value[0];
            switch (orientation)
            {
                case 2: src.RotateFlip(RotateFlipType.RotateNoneFlipX); break;
                case 3: src.RotateFlip(RotateFlipType.RotateNoneFlipXY); break;
                case 4: src.RotateFlip(RotateFlipType.RotateNoneFlipY); break;
                case 5: src.RotateFlip(RotateFlipType.Rotate90FlipX); break;
                case 6: src.RotateFlip(RotateFlipType.Rotate90FlipNone); break;
                case 7: src.RotateFlip(RotateFlipType.Rotate90FlipY); break;
                case 8: src.RotateFlip(RotateFlipType.Rotate90FlipXY); break;
                default: break;
            }

            src.RemovePropertyItem(ExifOrientationId);
        }

        public void Dispose()
        {
            _textRecognizer.Dispose();
        }
    }
}
