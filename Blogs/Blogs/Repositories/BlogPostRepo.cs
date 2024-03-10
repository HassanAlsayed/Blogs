using Blogs.Data;
using Blogs.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blogs.Repositories
{ 
    public class BlogPostRepo : IBlogPostRepo
    {
        private readonly BlogDbContaxt _db;

        public BlogPostRepo(BlogDbContaxt db)
        {
            _db = db;
        }
        public async Task<BlogPost> AddAsync(BlogPost post)
        {
            await _db.BlogPosts.AddAsync(post);
            await _db.SaveChangesAsync();
            return post;
           
        }

        public async Task<BlogPost?> DeleteAsync(Guid BlogId)
        {
            var blogPost = await _db.BlogPosts.FirstOrDefaultAsync(x => x.Id == BlogId);
            if (blogPost != null)
            {
                _db.BlogPosts.Remove(blogPost);
                await _db.SaveChangesAsync();
                return blogPost;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _db.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid BlogId)
        {
            var blogPost = await _db.BlogPosts.FirstOrDefaultAsync(x => x.Id == BlogId);
            if(blogPost is not null)
            {
                return blogPost;
            }
            return null;

        }

        public async Task<BlogPost?> UpdateAsync(BlogPost post)
        {
           var blogPost = await _db.BlogPosts.FirstOrDefaultAsync(x => x.Id == post.Id);
            if (blogPost is not null)
            {
                blogPost.Heading = post.Heading;
                blogPost.Author = post.Author;
                blogPost.PageTitle = post.PageTitle;
                blogPost.ShortDescription = post.ShortDescription;
                blogPost.PublishedDate = post.PublishedDate;
                blogPost.FeaturedImageUrl = post.FeaturedImageUrl;
                blogPost.Content = post.Content;
                blogPost.Visible = post.Visible;
                blogPost.Tags = post.Tags;
               await _db.SaveChangesAsync();
                return blogPost;
            }
            return null;
        }
    }
}
