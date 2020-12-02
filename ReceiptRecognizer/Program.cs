using Recognizer.ObjectRecognizer;
using Recognizer.TextRecognizer;
using System.Threading.Tasks;

namespace Recognizer
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
