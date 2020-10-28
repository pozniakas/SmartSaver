using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Tesseract;

namespace SmartSaver.Controllers
{
    public class ImageRecognizer
    {
        private const string TessDataPath = @".\assets\tessdata\best";

        public bool IsLoaded()
        {
            try
            {
                var isLoaded = CvInvoke.CheckLibraryLoaded();
                Debug.WriteLine("Image recognizer is loaded");
                return isLoaded;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public void RecognizeTestImages()
        {
            try
            {
                var dir = new DirectoryInfo(@"..\..\..\assets\Receipts");
                foreach (var imgPath in dir.GetFiles("*.jpg"))
                {
                    var res = Recognize(imgPath);

                    var destPath = imgPath.Directory.Parent.FullName + @"\receiptsTxt\" + imgPath.Name + ".txt";
                    Debug.WriteLine(destPath);

                    File.WriteAllText(destPath, res);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        private string Recognize(FileSystemInfo imagePath)
        {
            Debug.WriteLine(imagePath);

            #region Original Image
            using var originalImage = Image.FromFile(imagePath.FullName);
            double scaleIndex = (double) originalImage.Width / originalImage.Height;
            #endregion

            // Auto crop Image

            // Scaled Image
            var resizedImage = ResizeImage(originalImage, 500, (int)(500 / scaleIndex));

            return ProcessImage(resizedImage);

            // Split Image
            // SplitImage(resizedImage);
        }

        private void AutoCrop(string path)
        {
            using var image = new Image<Bgr, byte>(path);
            // Grayscale
            var grayScaleImage = image.Convert<Gray, byte>();

            // Applying GaussianBlur
            var blurredImage = grayScaleImage.SmoothGaussian(5, 5, 0, 0);
            // OR
            CvInvoke.GaussianBlur(grayScaleImage, blurredImage, new Size(5, 5), 0);

            // Applying Canny algorithm
            var cannyImage = new UMat();
            CvInvoke.Canny(blurredImage, cannyImage, 50, 150);

            // Finding largest contours
            var contours = new VectorOfVectorOfPointF();
            CvInvoke.FindContours(cannyImage, contours, null, RetrType.Tree, ChainApproxMethod.ChainApproxSimple);

            for (int i = 0; i < contours.Size; i++)
            {
                var contourVector = contours[i];
                using var contour = new VectorOfPoint();
                var peri = CvInvoke.ArcLength(contourVector, true);
                CvInvoke.ApproxPolyDP(contourVector, contour, 0.1 * peri, true);
                if (contour.ToArray().Length == 4 && CvInvoke.IsContourConvex(contour))
                {
                    Debug.WriteLine(contour.ToString());
                    // return contour;
                }
            }
        }

        /// <summary>
        ///     Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        private Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using var graphics = Graphics.FromImage(destImage);
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            using var wrapMode = new ImageAttributes();
            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
            graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);

            return destImage;
        }

        private IEnumerable<Image> SplitImage(Image image)
        {
            //var newBitmap = new Bitmap(104, 104);

            //var graphics = Graphics.FromImage(newBitmap);
            //graphics.DrawImage(originalImage, new Rectangle(0, 0, 104, 104), new Rectangle(0, 0, 104, 104), GraphicsUnit.Pixel);
            //graphics.Dispose();


            //var imgarray = new Image[9];
            //var img = image;
            //for (int i = 0; i < 3; i++)
            //{
            //    for (int j = 0; j < 3; j++)
            //    {
            //        var index = i * 3 + j;
            //        imgarray[index] = new Bitmap(104, 104);

            //        var graphics = Graphics.FromImage(imgarray[index]);
            //        graphics.DrawImage(img, new Rectangle(0, 0, 104, 104), new Rectangle(i * 104, j * 104, 104, 104), GraphicsUnit.Pixel);
            //        graphics.Dispose();
            //    }
            //}

            return new List<Image>();
        }

        private string ProcessImage(Image image)
        {
            using var ocrEngineBest = new TesseractEngine(TessDataPath, "lit", EngineMode.Default);
            using var img = Pix.LoadFromMemory(ImageToByte(image));
            var imgGray = img.ConvertRGBToGray();

            return ocrEngineBest.Process(imgGray).GetText();
        }

        private byte[] ImageToByte(Image img)
        {
            var converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
    }
}
