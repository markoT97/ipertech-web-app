using Microsoft.AspNetCore.Http;
using System.IO;

namespace IpertechCompany.WebAPI.Utils
{
    public class FilesManagement
    {
        public static bool SaveFile(IFormFile file, string locationPath)
        {
            using (var fs = new FileStream(locationPath, FileMode.Create))
            {
                file.CopyTo(fs);
            }
            return File.Exists(locationPath);
        }
    }
}
