namespace ZayShop.Utilities.File
{
	public interface IFileService
	{
		string Upload(IFormFile file, string folder);
		bool IsImage(string contentType);
		bool IsAvailableSize(long length, long maxSize = 100);

		void Delete(string folder, string fileName);
	}
}
