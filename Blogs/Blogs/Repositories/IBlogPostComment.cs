using Blogs.Models.Domain;
using Blogs.Models.DTO;

namespace Blogs.Repositories
{
    public interface IBlogPostComment
    {
        Task<BlogPostComment> AddComment(BlogPostComment comment);
        Task<IEnumerable<BlogPostComment>> GetAllByIdComments(Guid id);
    }
}
