using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ArabDev.Services.Services.Helper
{
    public static class DocumentSetting
    {
        public static async Task<string> UploadFileAsync(IFormFile file, string folderName)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", folderName);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/Files/{folderName}/{fileName}"; // يُرجع المسار النسبي
        }

        public static void DeleteFile(string fileName, string folderName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", folderName, fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}