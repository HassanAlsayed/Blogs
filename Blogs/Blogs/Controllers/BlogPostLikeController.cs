using Blogs.Models.Domain;
using Blogs.Models.DTO;
using Blogs.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blogs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLike _like;

        public BlogPostLikeController(IBlogPostLike like)
        {
            _like = like;
        }
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest blogPost)
        {
            var model = new BlogPostLike
            {
                BlogPostId = blogPost.BlogPostId,
                UserId = blogPost.UserId,
            };
            await _like.AddLikeForPost(model);
            return Ok();
        }

        [HttpGet]
        [Route("{blogPostId:Guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikesFroBlog([FromRoute] Guid blogPostId)
        {
            var totalLikes = await _like.GetTotalLikes(blogPostId);
            return Ok(totalLikes);
        }
    }
}
