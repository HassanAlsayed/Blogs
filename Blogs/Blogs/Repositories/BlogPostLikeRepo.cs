using Blogs.Data;
using Blogs.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blogs.Repositories
{
    public class BlogPostLikeRepo : IBlogPostLike
    {
        private readonly BlogDbContaxt _db;

        public BlogPostLikeRepo(BlogDbContaxt db)
        {
            _db = db;
        }

        public async Task<BlogPostLike> AddLikeForPost(BlogPostLike blogPostLike)
        {
            await _db.Likes.AddAsync(blogPostLike);
            await _db.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
          return await _db.Likes.CountAsync(x => x.BlogPostId == blogPostId );
        }
    }
}
