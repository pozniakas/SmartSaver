using ReceiptRecognizer.ObjectRecognizer;
using ReceiptRecognizer.TextRecognizer;

namespace ReceiptRecognizer
{
    class Program
    {
        static void Main(string[] args)
        {
            var recognizer = new ReceiptRecognizer(new EmguLargestAreaRecognizer(), new TesseractRecognizer());
            recognizer.RecognizeTestImages();
        }
    }
}
