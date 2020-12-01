using ReceiptRecognizer.ObjectRecognizer;
using ReceiptRecognizer.TextRecognizer;
using System.Threading.Tasks;

namespace ReceiptRecognizer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var recognizer = new ReceiptRecognizer(new EmguLargestAreaRecognizer(), new TesseractRecognizer());
            await recognizer.RecognizeTestImages();
        }
    }
}
