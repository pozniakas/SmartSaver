using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recognizer.ObjectRecognizer
{
    public class EmguLargestAreaRecognizer : IObjectRecognizer
    {
        private readonly Tuple<int, int> CannyThreshold = new Tuple<int, int>(100, 200);
        public Rectangle CropRectangle { get; private set; }

        public EmguLargestAreaRecognizer() { }

        public VectorOfVectorOfPoint GetContours(Bitmap bitmap)
        {
            using var emguImage = bitmap.ToImage<Bgr, byte>();

            // Grayscale
            var grayScaleImage = emguImage.Convert<Gray, byte>();

            // Applying GaussianBlur
            //var blurredImage = grayScaleImage.SmoothGaussian(5, 5, 0, 0);
            var blurredImage = new Image<Gray, byte>(grayScaleImage.Size);
            CvInvoke.GaussianBlur(grayScaleImage, blurredImage, new Size(5, 5), 0);

            // Applying Canny algorithm
            using var cannyImage = new UMat();
            CvInvoke.Canny(blurredImage, cannyImage, 50, 100);

            //var name = @"C:\Users\aleks\Workspace\SmartSaver\ReceiptRecognizer\assets\Result\"
            //             + DateTime.Now.ToString("mm.ss.ff") + "canny.jpg";
            //cannyImage.ToBitmap().Save(name, ImageFormat.Jpeg);

            // Finding contours
            var contours = new VectorOfVectorOfPoint();
            CvInvoke.FindContours(cannyImage, contours, null, RetrType.Tree, ChainApproxMethod.ChainApproxSimple);

            return contours;
        }

        private Point[] GetLargestContour(VectorOfVectorOfPoint contours)
        {
            Point[] largestContour = new Point[0];
            var maxPeri = 0.0;

            for (int i = 0; i < contours.Size; i++)
            {
                var contourVector = contours[i];
                using var contour = new VectorOfPoint();

                var peri = CvInvoke.ArcLength(contourVector, true);
                var area = CvInvoke.ContourArea(contourVector);
                CvInvoke.ApproxPolyDP(contourVector, contour, 0.1 * peri, true);

                if (contour != null) // && contour.ToArray().Length == 4 && CvInvoke.IsContourConvex(contour)
                {
                    if (peri > maxPeri)
                    {
                        largestContour = contour.ToArray();
                        maxPeri = peri;
                    }
                }
            }

            return largestContour;
        }

        public Rectangle FindCropRectangle(Point[] points)
        {
            var listOfPoints = new List<Point>(points);

            var minX = listOfPoints.Min(p => p.X);
            var minY = listOfPoints.Min(p => p.Y);

            var maxX = listOfPoints.Max(p => p.X);
            var maxY = listOfPoints.Max(p => p.Y);

            return new Rectangle(new Point(minX, minY), new Size(maxX - minX, maxY - minY));
        }

        public async Task<Bitmap> GetRecognizedImage(Bitmap image)
        {
            var getContours = new Task<VectorOfVectorOfPoint>(() => GetContours(image));
            getContours.Start();

            var largestContour = GetLargestContour(await getContours);
            var cropRectangle = FindCropRectangle(largestContour);

            return image.Clone(cropRectangle, PixelFormat.Format32bppArgb);
        }
    }
}
