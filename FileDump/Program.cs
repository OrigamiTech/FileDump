using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace FileDump
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Image Width: ");
            int width = int.Parse(Console.ReadLine());
            foreach(string path in args)
            {
                Bitmap b = new Bitmap(1, 1);
                byte[] allbytes = rawData(path);
                b = new Bitmap(width, (int)Math.Ceiling((double)allbytes.Length / width));
                for (int i = 0; i < allbytes.Length; i++)
                    b.SetPixel(i % (int)width, i / (int)width, Color.FromArgb(allbytes[i], allbytes[i], allbytes[i]));
                b.Save(path + "." + width.ToString("0") + ".png", System.Drawing.Imaging.ImageFormat.Png);
                b.Dispose();
            }
        }

        static byte[] rawData(string filename)
        { return File.ReadAllBytes(filename); }
    }
}
