using System.Diagnostics;
using Tesseract;

namespace SmartSaver.Controllers
{
    public class ImageRecognizer
    {
        public void Recognize()
        {
            string path = @".\assets\receipt1.jpg";

            var ocrengine = new TesseractEngine(@".\assets\tessdata", "lit", EngineMode.Default);
            var img = Pix.LoadFromFile(path);
            //img = img.ConvertRGBToGray();
            var res = ocrengine.Process(img);
            Debug.Write(res.GetText());
        }
    }
}
