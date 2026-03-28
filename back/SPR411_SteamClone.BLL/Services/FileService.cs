using Microsoft.AspNetCore.Http;
using SPR411_SteamClone.BLL.Settings;

namespace SPR411_SteamClone.BLL.Services
{
    public class FileService
    {
        private readonly string _storagePath;

        public FileService()
        {
            var root = Directory.GetCurrentDirectory();
            _storagePath = Path.Combine(root, StaticFilesSettings.Storage);
        }

        public async Task<ServiceResponse> SaveImageAsync(IFormFile image, string destPath)
        {
            var types = image.ContentType.Split("/");

            if (types.Length != 2 || types[0] != "image")
            {
                return ServiceResponse.Error("Файл не є зображенням");
            }

            string folderPath = Path.Combine(_storagePath, destPath);

            if(!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string ext = Path.GetExtension(image.FileName);
            string imageName = Guid.NewGuid().ToString() + ext;
            string imagePath = Path.Combine(folderPath, imageName);

            using var stream = File.Create(imagePath);
            await image.CopyToAsync(stream);

            return ServiceResponse.Success("Зображення успішно збережене", imageName);
        }

        public async Task<ServiceResponse[]> SaveImagesAsync(IEnumerable<IFormFile> images, string destPath)
        {
            var tasks = new List<Task<ServiceResponse>>();

            foreach (var image in images)
            {
                tasks.Add(SaveImageAsync(image, destPath));
            }

            var res = await Task.WhenAll(tasks);

            return res;
        }
    }
}
