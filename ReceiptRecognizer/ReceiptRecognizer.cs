using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Tesseract;

namespace ReceiptRecognizer
{
    public class ReceiptRecognizer
    {
        private const string TessDataPath = @".\assets\tessdata\best";
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
                    currentImageFile = imgPath;
                    var resizedImage = GetResizedImage(imgPath);
                    //var fileWithTresholds = currentImageFile.Directory.Parent.FullName + @"\Resized\" + currentImageFile.Name + ".jpg";
                    //resizedImage.Save(fileWithTresholds);

                    var cropped = Crop(resizedImage);
                    var fileName = currentImageFile.Directory.Parent.FullName + @"\Resized\" + currentImageFile.Name + ".jpg";
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
            //FixImageOrientation(originalImage);

            double aspectRatio = (double)originalImage.Width / originalImage.Height;

            //var res =  ResizeImage(originalImage, 500, (int)(500 / aspectRatio));
            //resizedImage.Save(@"C:\Users\aleks\Workspace\SmartSaver\ReceiptRecognizer\assets\resized.jpg");
            return new Bitmap(originalImage, 500, (int)(500 / aspectRatio));
        }

        static void FixImageOrientation(Image srce)
        {
            const int ExifOrientationId = 0x112;
            var properties = new List<int>(srce.PropertyIdList);
            // Read orientation tag
            if (!properties.Contains(ExifOrientationId)) return;

            var prop = srce.GetPropertyItem(ExifOrientationId);
            var orient = BitConverter.ToInt16(prop.Value, 0);
            // Force value to 1
            prop.Value = BitConverter.GetBytes((short)1);
            //srce.SetPropertyItem(prop);
            srce.RemovePropertyItem(ExifOrientationId);
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

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
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
