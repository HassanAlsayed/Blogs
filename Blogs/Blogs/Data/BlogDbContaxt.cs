using Blogs.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blogs.Data
{
    public class BlogDbContaxt : DbContext
    {
        public BlogDbContaxt(DbContextOptions<BlogDbContaxt> options) : base(options) { }
        
       public DbSet<BlogPost> BlogPosts { get; set; }
       public DbSet<Tag> Tags { get; set; }
       public DbSet<BlogPostLike> Likes { get; set; }
       public DbSet<BlogPostComment> Comments { get; set; }
    }
}
