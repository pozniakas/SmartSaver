using Emgu.CV;
using ReceiptRecognizer.ObjectRecognizer;
using System;

namespace ReceiptRecognizer
{
    class Program
    {
        static void Main(string[] args)
        {
            var recognizer = new ReceiptRecognizer(new EmguLargestAreaRecognizer());
            recognizer.RecognizeTestImages();
        }
    }
}
