using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blogs.Models.DTO
{
    public class BlogPostLikeDTO
    {
        public Guid Id{ get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        [ValidateNever]
        public string? FeaturedImageUrl { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }
        public int TolalLikes { get; set; }
        public string CommentDescription { get; set; }
       
        public IEnumerable<BlogComment> Comments { get; set; }
    }
}
