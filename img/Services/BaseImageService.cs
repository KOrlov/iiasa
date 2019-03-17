using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;

namespace img.Services
{
    public class BaseImageService:IImageService
    {
        private string path = "c:\\temp\\";
        private MagickImage img;
        public BaseImageService()
        {
            
        }

        public string Create(byte[] buf)
        {
            try
            {
                img = new MagickImage();
                img.Read(buf);
                var hash = MD5.Create().ComputeHash(buf);
                var fileName = string.Concat(hash.Select(c => c.ToString("X2")));
                File.WriteAllBytes(path+ fileName,buf);
                return fileName;

            }
            catch
            {
                return "not an image";
            }

        }

        public string Exif(string filename)
        {
            try
            {
                var buf = System.IO.File.ReadAllBytes(path+filename);
                
                img = new MagickImage();
                img.Read(buf);
                var exif = img.GetExifProfile();
                StringBuilder sb = new StringBuilder();
                foreach (var s in exif.Values)
                {
                    sb.Append(s.Tag.ToString()+":"+s.Value + "\n");
                }
                
                return sb.ToString();

            }
            catch
            {
                return "not an image";
            }
        }




        public byte[] Enhance(string filename)
        {
            try
            {
                var buf = System.IO.File.ReadAllBytes(path + filename);
                img = new MagickImage();
                img.Read(buf);
                img.Enhance();

                return img.ToByteArray();

            }
            catch
            {
                return null;
            }
        }
        public byte[] Resize(int x, int y, string filename)
        {
            try
            {
                var buf = System.IO.File.ReadAllBytes(path + filename);
                img = new MagickImage();
                img.Read(buf);
                img.Resize(x,y);

                return img.ToByteArray();

            }
            catch
            {
                return null;
            }
        }
        public string Format()
        {
            try
            {

                return img.Format.ToString();

            }
            catch
            {
                return "not an image";
            }
        }

        public byte[] Original(string filename)
        {
            try
            {
                var buf = System.IO.File.ReadAllBytes(path + filename);
                img = new MagickImage();
                img.Read(buf);


                return img.ToByteArray();

            }
            catch
            {
                return null;
            }
        }


    }
}



