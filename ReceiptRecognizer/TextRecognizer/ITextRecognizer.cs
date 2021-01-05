using System;
using System.Drawing;
using System.Threading.Tasks;
using Tesseract;

namespace Recognizer.TextRecognizer
{
    public interface ITextRecognizer : IDisposable
    {
        public Page RecognizedPage { get; }
        public Task<string> GetText(Bitmap image);
    }
}
