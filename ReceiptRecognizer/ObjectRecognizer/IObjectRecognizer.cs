using Emgu.CV.Util;
using System.Drawing;

namespace ReceiptRecognizer
{
    public interface IObjectRecognizer
    {
        public VectorOfVectorOfPoint GetContours(Bitmap bitmap);
        public Rectangle FindCropRectangle(Point[] points);
        public Bitmap GetRecognizedImage(Bitmap image);
    }
}
