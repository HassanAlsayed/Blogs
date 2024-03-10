using Blogs.Models.Domain;
using Blogs.Models.DTO;
using Blogs.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Blogs.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepo _blogPost;
        private readonly IBlogPostLike _like;
        private readonly IBlogPostComment _comment;
        private readonly UserManager<IdentityUser> _manager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public BlogsController(IBlogPostRepo blogPost,
            IBlogPostLike like,
            IBlogPostComment comment,
            UserManager<IdentityUser> manager,
            SignInManager<IdentityUser> signInManager)
        {
            _blogPost = blogPost;
            _like = like;
            _comment = comment;
            _manager = manager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(Guid blogId)
        {
            var blogPost = await _blogPost.GetAsync(blogId);

            var blogPostLike = new BlogPostLikeDTO();

            if (blogPost is not null)
            {
              var totalLikes = await _like.GetTotalLikes(blogId);

              var commnets = await _comment.GetAllByIdComments(blogId);

                var blogComment = new List<BlogComment>();
                foreach (var commnet in commnets)
                {
                    blogComment.Add(new BlogComment
                    {
                        
                        Description = commnet.Description,
                        DateAdded = commnet.DateAdded
                    });

                }

                 blogPostLike = new BlogPostLikeDTO
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    Author = blogPost.Author,
                    PageTitle = blogPost.PageTitle,
                    ShortDescription = blogPost.ShortDescription,
                    PublishedDate = blogPost.PublishedDate,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    Content = blogPost.Content,
                    Visible = blogPost.Visible,
                     TolalLikes = totalLikes,
                     Comments = blogComment

                 };
            }

            return View(blogPostLike);
        }

        [HttpPost]
        public async Task<IActionResult> Index(BlogPostLikeDTO blog)
        {
            if (_signInManager.IsSignedIn(User)){ 
            var BlogComment = new BlogPostComment
            {
                BlogPostId = blog.Id,
                Description = blog.CommentDescription,
                UserId = Guid.Parse(_manager.GetUserId(User)),
                DateAdded = DateTime.Now,

                };
                await _comment.AddComment(BlogComment);
                return View(blog);
                 }
            return View();
        }

    }
}
