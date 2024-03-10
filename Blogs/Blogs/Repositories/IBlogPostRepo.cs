using Blogs.Models.Domain;

namespace Blogs.Repositories
{
    public interface IBlogPostRepo
    {
        public Task<IEnumerable<BlogPost>> GetAllAsync();
        public Task<BlogPost?> GetAsync(Guid BlogId);
        public Task<BlogPost> AddAsync(BlogPost post);
        public Task<BlogPost?> UpdateAsync(BlogPost post);
        public Task<BlogPost?> DeleteAsync(Guid BlogId);
    }
}
