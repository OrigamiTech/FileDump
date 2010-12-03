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
            Console.Write("Binary mode? ");
            bool binary = Console.ReadLine().Trim().ToLower()[0] == 'y';
            foreach (string path in args)
            {
                Bitmap b = new Bitmap(1, 1);
                byte[] allbytes = File.ReadAllBytes(path);
                b = new Bitmap(width, (int)Math.Ceiling((double)(allbytes.Length * (binary ? 8 : 1)) / width));
                for (int i = 0; i < allbytes.Length * (binary ? 8 : 1); i++)
                    b.SetPixel(i % (int)width, i / (int)width, binary ? (((allbytes[i / 8] >> (7 - (i % 8))) & 0x01) != 0 ? Color.Black : Color.White) : Color.FromArgb(allbytes[i], allbytes[i], allbytes[i]));
                b.Save(path + "." + width.ToString("0") + (binary ? "b" : "") + ".png", System.Drawing.Imaging.ImageFormat.Png);
                b.Dispose();
            }
        }
    }
}