using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace garvel
{
   public class GVimage
    {
        public Mat oriImg;
        public Image markedImg;
        public string imgName;
        public string imgPath;
        public int width;
        public int height;
        public double dpiX;
        public double dpiY;
        public double mmX;
        public double mmY;
        public double pixelWidth;
        public double pixelHeight;
        public double pixelSize;

        public bool loadImgFile()
        {
            try
            {
                OpenCvSharp.Extensions.BitmapConverter.ToBitmap(new Mat(imgPath));
            }
            catch
            {
                MessageBox.Show("This file is not supported.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            oriImg = new Mat(imgPath);
            string[] splitPath = imgPath.Split(new string[] { "\\" }, StringSplitOptions.None);
            imgName = splitPath[splitPath.Length - 1];
            //setImgInfo();
            return true;
        }
        public void setImgInfo(bool custom = false)
        {
            width = oriImg.Width;
            height = oriImg.Height;

            if (custom)
            {
                dpiX = (double)(width * 2.54 / mmX);
                dpiY = (double)(height * 2.54 / mmY);
            }
            else
            {
                Bitmap tmpBm = Mat2bm(oriImg);
                dpiX = (double)tmpBm.HorizontalResolution;
                dpiY = (double)tmpBm.VerticalResolution;
                tmpBm.Dispose();

                mmX = width / dpiX * 2.54 * 10;
                mmY = height / dpiY * 2.54 * 10;
            }
            if (double.IsPositiveInfinity(dpiX) || double.IsPositiveInfinity(dpiY))
                dpiX = dpiY = 0;

            pixelWidth = mmX / width;
            pixelHeight = mmY / height;
            pixelSize = pixelWidth * pixelHeight;
        }
        public Bitmap Mat2bm(Mat matImg)
        {
            return OpenCvSharp.Extensions.BitmapConverter.ToBitmap(matImg);
        }

    }
}
