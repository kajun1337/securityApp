namespace securityApp.Helper
{
    public class FileHandler
    {
        public const string folderName = "FilesToUpload";
        public readonly string folderPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        public async void CreateFile(IFormFile file)
        {
            var filePath = Path.Combine(folderPath, file.FileName);

            Console.WriteLine(filePath);
            if (!File.Exists(filePath))
            {
                using (var fileContentStream = new MemoryStream())
                {
                    await file.CopyToAsync(fileContentStream);
                    await File.WriteAllBytesAsync(Path.Combine(folderPath, file.FileName),
                        fileContentStream.ToArray());
                }
            }


        }
    }
}
