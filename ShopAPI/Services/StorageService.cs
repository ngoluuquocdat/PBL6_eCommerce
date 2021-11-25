using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace ShopAPI.Services
{
    public class StorageService : IStorageService
    {
        private readonly string _storageFolder;
        private const string STORAGE_FOLDER_NAME = "storage";

        public StorageService(IWebHostEnvironment webHostEnvironment)
        {
            _storageFolder = Path.Combine(webHostEnvironment.WebRootPath, STORAGE_FOLDER_NAME);
        }

        public string GetFileUrl(string fileName)
        {
            return $"/{STORAGE_FOLDER_NAME}/{fileName}";
        }

        public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(_storageFolder, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_storageFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }
    }
}