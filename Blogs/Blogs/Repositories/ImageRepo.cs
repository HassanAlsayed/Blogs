
using Blogs.Data;

namespace Blogs.Repositories
{
    public class ImageRepo : IImageRepo
    {
        private readonly BlogDbContaxt _db;

        public ImageRepo(BlogDbContaxt db)
        {
            _db = db;
        }
        public Task<string> UploadImage(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
