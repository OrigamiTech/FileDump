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
            int width = 32;
            bool
                binary = false,
                argb = false,
                fbpp = false,
                fbppf = false;
            Console.Write("Image Width: ");
            string strWidth = Console.ReadLine().Trim().ToLower();
            if (strWidth == "auto")
                ;//width = square root, or closest.
            width = int.Parse(strWidth);
            Console.Write("Binary mode? ");
            binary = Console.ReadLine().Trim().ToLower()[0] == 'y';
            if (!binary)
            {
                Console.Write("ARGB color mode? ");
                argb = Console.ReadLine().Trim().ToLower()[0] == 'y';
            }
            if (!binary&&!argb)
            {
                Console.Write("4bpp mode? ");
                fbpp = Console.ReadLine().Trim().ToLower()[0] == 'y';
            }
            if (fbpp)
            {
                Console.Write("4bpp flip? ");
                fbppf = Console.ReadLine().Trim().ToLower()[0] == 'y';
            }
            foreach (string path in args)
            {
                Bitmap b = new Bitmap(1, 1);
                byte[] allbytes = File.ReadAllBytes(path);
                b = new Bitmap(width, (int)Math.Ceiling((double)(allbytes.Length * (binary ? 8 : (argb ? 0.25 : (fbpp ? 2 : 1)))) / width));
                for (int i = 0; i < allbytes.Length * (binary ? 8 : (argb ? 0.25 : (fbpp ? 2 : 1))); i++)
                    b.SetPixel(i % (int)width, i / (int)width, binary ? (((allbytes[i / 8] >> (7 - (i % 8))) & 0x01) != 0 ? Color.Black : Color.White) : (argb ? Color.FromArgb(allbytes[i * 4], allbytes[i * 4 + 1], allbytes[i * 4 + 2], allbytes[i * 4 + 3]) : (fbpp ? Color.FromArgb((allbytes[i / 2] & ((i % 2 == 0) ^ fbppf ? 0xF0 : 0x0F)) << ((i % 2 == 0) ^ fbppf ? 0 : 4), (allbytes[i / 2] & ((i % 2 == 0) ^ fbppf ? 0xF0 : 0x0F)) << ((i % 2 == 0) ^ fbppf ? 0 : 4), (allbytes[i / 2] & ((i % 2 == 0) ^ fbppf ? 0xF0 : 0x0F)) << ((i % 2 == 0) ^ fbppf ? 0 : 4)) : Color.FromArgb(allbytes[i], allbytes[i], allbytes[i]))));
                b.Save(path + "." + width.ToString("0") + (binary ? "b" : (argb ? "a" : (fbpp? "f" : ""))) + ".png", System.Drawing.Imaging.ImageFormat.Png);
                b.Dispose();
            }
        }
    }
}