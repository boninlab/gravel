using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Diagnostics;

namespace garvel
{
    public class Picture
    {
        private double zoom, zoomFit;
        private int width, height, x, y, priorMouseX, priorMouseY;

        //set and expose required picture properties for MainForm mainImage_Paint event 
        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public int X { get { return x; } }
        public int Y { get { return y; } }

        public void InitialiseMousePosition(int mouseX, int mouseY)
        {
            priorMouseX = mouseX;
            priorMouseY = mouseY;
        }

        public void Drag(Image image, MouseEventArgs mouse, ref PictureBox pictureBox)
        {
            int dX = mouse.X - priorMouseX;
            int dY = mouse.Y - priorMouseY;

            priorMouseX = mouse.X;
            priorMouseY = mouse.Y;

            x += dX;
            y += dY;
            AdjustPosition(ref pictureBox);
        }

        public void Zoom(Image image, ref PictureBox pictureBox, MouseEventArgs mouse)
        {
            //double delta = (double)mouse.Delta / 1200;

            //if (delta > 0 && zoom + delta <= 10 || delta < 0 && zoom + delta > 0)
            //    zoom += delta;

            if (mouse.Delta > 0)
                zoom += 0.1;

            else if (mouse.Delta < 0 && zoom > zoomFit)
                zoom -= 0.1;

            int zoomPointX = mouse.X;
            int zoomPointY = mouse.Y;
            
            int offsetX = zoomPointX - X;
            int offsetY = zoomPointY - Y;

            int oldWidth = Width;
            int oldHeight = Height;

            width = (int)(image.Width * zoom);
            height = (int)(image.Height * zoom);

            offsetX = (int)(offsetX * ((double)Width / (double)oldWidth));
            offsetY = (int)(offsetY * ((double)Height / (double)oldHeight));

            x = zoomPointX - offsetX;
            y = zoomPointY - offsetY;
            AdjustPosition(ref pictureBox);
        }

        public void AdjustPosition(ref PictureBox pictureBox)
        {
            x = (width <= pictureBox.Width) ? (pictureBox.Width - width) / 2 : x;
            y = (height <= pictureBox.Height) ? (pictureBox.Height - height) / 2 : y;

            if (width > pictureBox.Width && x > 0)
                x = 0;

            if (width > pictureBox.Width && x + width < pictureBox.Width)
                x = pictureBox.Width - width;

            if (height > pictureBox.Height && y > 0)
                y = 0;

            if (height > pictureBox.Height && y + height < pictureBox.Height)
                y = pictureBox.Height - height;
        }

        public void Fit(Image image, ref PictureBox pictureBox)
        {
            double ratioX = (double)pictureBox.Width / image.Width;
            double ratioY = (double)pictureBox.Height / image.Height;
            zoom = Math.Min(ratioX, ratioY);
            zoomFit = zoom; //store old

            width = (int)(image.Width * zoom);
            height = (int)(image.Height * zoom);

            x = (int)((pictureBox.Width - Width) / 2);
            y = (int)((pictureBox.Height - Height) / 2);
        }
    }
}
