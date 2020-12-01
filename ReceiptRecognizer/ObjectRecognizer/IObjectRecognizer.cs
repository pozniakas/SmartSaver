using Emgu.CV.Util;
using System.Drawing;
using System.Threading.Tasks;

namespace ReceiptRecognizer
{
    public interface IObjectRecognizer
    {
        public VectorOfVectorOfPoint GetContours(Bitmap bitmap);
        public Rectangle FindCropRectangle(Point[] points);
        public Task<Bitmap> GetRecognizedImage(Bitmap image);
    }
}
