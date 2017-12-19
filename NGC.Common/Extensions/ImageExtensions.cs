using NGC.Model;
using System;
using System.DrawingCore;
using System.DrawingCore.Drawing2D;
using System.DrawingCore.Imaging;
using System.IO;

namespace NGC.Common.Extensions
{
    public static class ImageExtensions
    {
        public static string Draw(this MerakiTextImage image,Customer c)
        {
            string base64 = "";
            using(var f = new Font(image.FontName, 24))
            using(var original = Image.FromStream(new MemoryStream(image.Bytes)))
            {
                var ogfx = Graphics.FromImage(original);
                string text = string.Format(image.Text, c.Name, c.Surname1, c.Surname2);
                var size = ogfx.MeasureString(text, f);
                using (var bmp = new Bitmap((int)size.Width, (int)size.Height))
                {
                    Graphics bgfx = Graphics.FromImage(bmp);
                    bgfx.FillRectangle(Brushes.Transparent, 0, 0, size.Width, size.Height);
                    bgfx.DrawString(text, f, Brushes.Black, 0, 0);
                    using(var oStream = new MemoryStream())
                    using (var result = new Bitmap(original, original.Size))
                    {
                        var rgfx = Graphics.FromImage(result);
                        rgfx.DrawImageUnscaled(original, 0, 0);
                        double sx = image.Width / size.Width;
                        double sy = image.Height / size.Height;
                        double s = Math.Max(sx, sy);
                        rgfx.DrawImage(bmp,new RectangleF() { X = (float)image.X, Y= (float)image.Y, Width = (float)image.Width,Height=(float)image.Height}
                        , new RectangleF() { X = 0, Y = 0, Height = size.Height, Width = size.Width }, GraphicsUnit.Pixel);
                        result.Save(oStream, ImageFormat.Png);
                        byte[] bytes = oStream.ToArray();
                        base64 = Convert.ToBase64String(bytes);
                    }
                }
            }
            return base64;
        }
        public static string ToBase64(this byte[] bytes)
        {
           return  Convert.ToBase64String(bytes ?? new byte[] { });
        }

        public static byte[] FromBase64ToByteArray(this string base64)
        {
            return Convert.FromBase64String(base64);
        }
    }
}
