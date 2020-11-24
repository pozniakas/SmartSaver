using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptRecognizer.ObjectRecognizer
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
            CvInvoke.Canny(blurredImage, cannyImage, CannyThreshold.Item1, CannyThreshold.Item2);

            // Finding largest contours
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

                if (contour != null && contour.ToArray().Length == 4 && CvInvoke.IsContourConvex(contour))
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
            var lastPoint = points[points.Length - 1];
            var upperLeft = new Coordinate<int> { X = lastPoint.X, Y = lastPoint.Y };
            var downRight = new Coordinate<int> { X = 0, Y = 0 };

            foreach (var point in points)
            {
                if (point.X < upperLeft.X)
                {
                    upperLeft.X = point.X;
                }
                else if (point.Y < upperLeft.Y)
                {
                    upperLeft.Y = point.Y;
                }

                if (point.X > downRight.X)
                {
                    downRight.X = point.X;
                }
                else if (point.Y > downRight.Y)
                {
                    downRight.Y = point.Y;
                }
            }

            return downRight.X - upperLeft.X != 0 && downRight.Y - upperLeft.Y != 0
                ? new Rectangle(upperLeft.X, upperLeft.Y, downRight.X - upperLeft.X, downRight.Y - upperLeft.Y)
                : new Rectangle(upperLeft.X, upperLeft.Y, downRight.X, downRight.Y);
        }

        public Bitmap GetRecognizedImage(Bitmap image)
        {
            var getContours = new Task<VectorOfVectorOfPoint>(() => GetContours(image));
            getContours.Start();

            var largestContour = GetLargestContour(getContours.Result);
            var cropRectangle = FindCropRectangle(largestContour);

            return null;// image.Clone(cropRectangle, PixelFormat.Format32bppArgb);
        }
    }
}
