using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace ReceiptRecognizer
{
    public class ReceiptRecognizer
    {
        // To be removed
        private FileInfo currentImageFile;

        private IObjectRecognizer _objectRecognizer;
        private ITextRecognizer _textRecognizer;

        public ReceiptRecognizer(IObjectRecognizer objectRecognizer)
        {
            _objectRecognizer = objectRecognizer;
        }

        public ReceiptRecognizer(IObjectRecognizer objectRecognizer, ITextRecognizer textRecognizer)
        {
            _objectRecognizer = objectRecognizer;
            _textRecognizer = textRecognizer;
        }

        public void RecognizeTestImages()
        {
            try
            {
                var dir = new DirectoryInfo(@"..\..\..\assets\Receipts");
                foreach (var imgPath in dir.GetFiles("*.jpg"))
                {
                    //currentImageFile = imgPath;
                    var fileName = imgPath.Directory.Parent.FullName + @"\Result\" + imgPath.Name + "cropped.jpg";
                    //fileName = imgPath.Directory.Parent.FullName + @"\Result\" + DateTime.Now.ToString("mm.ss.ff") + "cropped.jpg";

                    var resizedImage = GetResizedImage(imgPath);
                    var cropped = Crop(resizedImage);

                    cropped.Save(fileName);

                    //var text = Recognize(cropped);

                    //var destPath = imgPath.Directory.Parent.FullName + @"\receiptsTxt\" + imgPath.Name + ".txt";
                    //Debug.WriteLine(destPath);

                    //File.WriteAllText(destPath, res);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        private Bitmap Crop(Bitmap initialImage)
        {
            var croppedImage = initialImage;
            try
            {
                croppedImage = _objectRecognizer.GetRecognizedImage(initialImage);

                return croppedImage;
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
                //throw;
            }
            return croppedImage;
        }

        private Bitmap GetResizedImage(FileInfo imagePath)
        {
            using var originalImage = Image.FromFile(imagePath.FullName);
            FixImageOrientation(originalImage);

            double aspectRatio = (double)originalImage.Width / originalImage.Height;
            return new Bitmap(originalImage, 500, (int)(500 / aspectRatio));
        }

        private void FixImageOrientation(Image src)
        {
            const int ExifOrientationId = 0x112;
            PropertyItem pi = src.PropertyItems.Select(x => x).FirstOrDefault(x => x.Id == ExifOrientationId);
            if (pi == null) return;

            byte orientation = pi.Value[0];
            switch (orientation)
            {
                case 2: src.RotateFlip(RotateFlipType.RotateNoneFlipX);   break;
                case 3: src.RotateFlip(RotateFlipType.RotateNoneFlipXY);  break;
                case 4: src.RotateFlip(RotateFlipType.RotateNoneFlipY);   break;
                case 5: src.RotateFlip(RotateFlipType.Rotate90FlipX);     break;
                case 6: src.RotateFlip(RotateFlipType.Rotate90FlipNone);  break;
                case 7: src.RotateFlip(RotateFlipType.Rotate90FlipY);     break;
                case 8: src.RotateFlip(RotateFlipType.Rotate90FlipXY);    break;
                default: break;
            }

            src.RemovePropertyItem(ExifOrientationId);
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
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
    }
}
