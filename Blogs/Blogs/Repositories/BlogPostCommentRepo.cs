
using Blogs.Data;
using Blogs.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blogs.Repositories
{
    public class BlogPostCommentRepo : IBlogPostComment
    {
        private readonly BlogDbContaxt _db;

        public BlogPostCommentRepo(BlogDbContaxt db)
        {
            _db = db;
        }
        public async Task<BlogPostComment> AddComment(BlogPostComment comment)
        {
            await _db.Comments.AddAsync(comment);
            await _db.SaveChangesAsync();
            return comment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetAllByIdComments(Guid id)
        {
           return await _db.Comments.Where(c => c.BlogPostId == id).ToListAsync();
            
        }
    }
}
