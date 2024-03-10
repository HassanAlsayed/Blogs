using Blogs.Models.Domain;

namespace Blogs.Repositories
{
    public interface IBlogPostLike
    {
        Task<int> GetTotalLikes(Guid blogPostId);
        Task<BlogPostLike> AddLikeForPost(BlogPostLike blogPostLike);
    }
}
