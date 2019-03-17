using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace img.Services
{
    public interface IImageService
    {

        string Create(byte[] buf);
        string Exif(string filename);
        
        byte[] Enhance(string name);
        byte[] Original(string name);
        byte[] Resize(int x, int y, string name);
        string Format();
    }
}
