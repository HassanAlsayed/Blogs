using System.ComponentModel.DataAnnotations;

namespace Blogs.Models.Domain
{
    public class Tag
    {
       
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string DisplayName { get; set; }
    
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}
