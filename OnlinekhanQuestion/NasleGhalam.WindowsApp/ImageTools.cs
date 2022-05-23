using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasleGhalam.WindowsApp
{
    public class ImageTools
    {
        public static void SaveImageOfWordPdf(String path, string distnation)
        {
            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = System.Windows.Forms.Application.StartupPath + "\\pdftopng.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = "-f 1 -r 600 \"" + path + "\" \"" + distnation + "\"";

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }


                var image = new Bitmap(distnation + "-000001.png");

                var resizedImage = GetImageWithRatioSize(image, 1 / 5d, 1 / 5d);
                //resizedImage.Save(distnation+"123.png", ImageFormat.Png);
                var rectangle = GetCropArea(resizedImage, 20);
                var croppedImage = CropImage(resizedImage, rectangle);
                croppedImage.Save(distnation + ".png", ImageFormat.Png);
                croppedImage.Dispose();

                File.Delete(distnation + "-000001.png");


            }
            catch (Exception ex)
            {
                // Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                File.Delete(distnation + "-000001.png");

            }


        }

        public static Bitmap GetImageWithRatioSize(Image stream, double widthRatio, double heightRatio)

        {

            using (var image = stream)

            {

                var ratioHeight = image.Height * heightRatio;

                var ratioWidth = image.Width * widthRatio;



                var originalWidth = image.Width;

                var originalHeight = image.Height;



                int newWidth;

                int newHeight;



                if (originalWidth > ratioWidth || originalHeight > ratioHeight)

                {

                    var ratioX = ratioWidth / originalWidth;

                    var ratioY = ratioHeight / originalHeight;

                    var ratio = Math.Min(ratioX, ratioY);

                    newWidth = (int)(originalWidth * ratio);

                    newHeight = (int)(originalHeight * ratio);

                }

                else

                {

                    newWidth = originalWidth;

                    newHeight = originalHeight;

                }



                return new Bitmap(image, new Size(newWidth, newHeight));



            }

        }



        public static Bitmap CropImage(Bitmap srcImage, System.Drawing.Rectangle cropArea)

        {

            Bitmap bmp = new Bitmap(cropArea.Width, cropArea.Height);
            using (Graphics gph = Graphics.FromImage(bmp))
            {
                gph.DrawImage(srcImage, new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), cropArea, GraphicsUnit.Pixel);
            }
            return bmp;
            // return srcImage.Clone(cropArea, srcImage.PixelFormat);

        }



        public static System.Drawing.Rectangle GetCropArea(Bitmap data, int padding)

        {

            int x1 = 0, y1 = 0, x2 = 0, y2 = 0;



            //// find x1

            //for (var i = 0; i < data.Width; i++)

            //{

            //    for (var j = 0; j < data.Height; j++)

            //    {

            //        if (!HasOpacity(data.GetPixel(i, j))) continue;

            //        x1 = i;

            //        break;

            //    }

            //    if (x1 > 0)

            //        break;

            //}



            // find y1

            for (var j = 0; j < data.Height; j++)

            {

                for (var i = x1; i < data.Width; i++)

                {

                    if (!IsWhite(data.GetPixel(i, j))) continue;

                    y1 = j;

                    break;

                }

                if (y1 > 0)

                    break;

            }



            // find x2

            for (var i = data.Width - 1; i > x1; i--)

            {

                for (var j = data.Height - 1; j > y1; j--)

                {

                    if (!IsWhite(data.GetPixel(i, j))) continue;

                    x2 = i;

                    break;

                }

                if (x2 > 0)

                    break;

            }



            // find y2

            for (var j = data.Height - 1; j > y1; j--)

            {

                for (var i = x2; i > x1; i--)

                {

                    if (!IsWhite(data.GetPixel(i, j))) continue;

                    y2 = j;

                    break;

                }

                if (y2 > 0)

                    break;

            }


            x2 = 911;

            x1 = data.Width - x2 - padding;

            y1 -= padding;

            x2 += padding;

            y2 += padding;




            var width = x2 - x1;

            var height = y2 - y1;



            return new System.Drawing.Rectangle(x1, y1, width, height);

        }



        public static bool HasOpacity(Color c)
        {
            return c.A > 0;
        }

        public static bool IsWhite(Color c)
        {
            if (c.R == 255 && c.B == 255 && c.G == 255)
                return false;
            return true;
        }
    }
}
