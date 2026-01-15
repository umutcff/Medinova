namespace Medinova.Helpers
{
    public static class ExtensionMethods
    {
        public static bool CheckSize(this IFormFile file,int mb)
        {
            return file.Length < mb * 1024 * 1024;
        }

        public static bool CheckType(this IFormFile file,string type = "image")
        {
            return file.ContentType.Contains(type);
        }

        public static async Task<string> FileUploadAsync(this IFormFile file, string folderPath)
        {
            var uniqueFileName = Guid.NewGuid().ToString() + file.FileName;
            string uniqueFolderPath = Path.Combine(folderPath, uniqueFileName);
            FileStream fileStream = new(uniqueFolderPath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return uniqueFileName;
        }

        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
