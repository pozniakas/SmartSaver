using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Tesseract;

namespace SmartSaver.Controllers
{
    public class ImageRecognizer
    {
        private readonly string TessDataPath = @".\assets\tessdata\best";

        public void RecognizeTestImages()
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(@"..\..\..\assets\Receipts");
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
                throw;
            }
        }

        private string Recognize(FileInfo imagePath)
        {
            Debug.WriteLine(imagePath);

            // Original Image
            using var originalImage = Image.FromFile(imagePath.FullName);
            double scaleIndex = (double) originalImage.Width / originalImage.Height;

            // Auto crop Image

            // Scaled Image
            var resizedImage = ResizeImage(originalImage, 500, (int)(500 / scaleIndex));

            return ProccessImage(resizedImage);

            // Split Image
            // SplitImage(resizedImage);
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

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using var wrapMode = new ImageAttributes();
                wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
            }

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

        private string ProccessImage(Image image)
        {
            using var ocrEngineBest = new TesseractEngine(TessDataPath, "lit", EngineMode.Default);
            using var img = Pix.LoadFromMemory(ImageToByte(image));
            var imgGray = img.ConvertRGBToGray();

            return ocrEngineBest.Process(imgGray).GetText();
        }

        private byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
    }
}
