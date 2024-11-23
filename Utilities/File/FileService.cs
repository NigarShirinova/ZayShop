
using Microsoft.AspNetCore.Hosting;

namespace ZayShop.Utilities.File
{
	public class FileService : IFileService
	{
		private readonly IWebHostEnvironment _webHostEnviroment;

		public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnviroment = webHostEnvironment;
        }
        public string Upload(IFormFile file, string folder)
		{
			var fileName = $"{Guid.NewGuid()} {file.FileName}";
			var filePath = Path.Combine(_webHostEnviroment.WebRootPath, folder, fileName);
			using (var fileStream = new FileStream (filePath, FileMode.Create, FileAccess.ReadWrite)) 
			{ 
				file.CopyTo (fileStream);
			}
			return fileName ;
		}

		public void Delete(string folder, string fileName)
		{
			var filePath = Path.Combine(_webHostEnviroment.WebRootPath, folder, fileName);

			if (System.IO.File.Exists(filePath)) System.IO.File.Delete(filePath);
		}


		public bool IsImage(string contentType)
		{
			if(contentType.Contains("image/"))  return true;
			return false;
		}

		public bool IsAvailableSize(long length, long maxSize=100)
		{
			if(length/1024 > maxSize) return false;
			return true;
		}

		

	}
}
