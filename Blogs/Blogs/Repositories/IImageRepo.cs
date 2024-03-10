namespace Blogs.Repositories
{
    public interface IImageRepo
    {
        public Task<string> UploadImage(IFormFile file);
    }
}
