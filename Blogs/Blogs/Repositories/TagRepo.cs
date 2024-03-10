using Blogs.Data;
using Blogs.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Blogs.Repositories
{
    public class TagRepo : ITagRepo
    {
        private readonly BlogDbContaxt _db;

        public TagRepo(BlogDbContaxt db)
        {
            _db = db;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await _db.Tags.AddAsync(tag);
            await _db.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid TagId)
        {
           var tagId = await _db.Tags.FirstOrDefaultAsync(t => t.Id == TagId);

            if(tagId == null)
            {
                return null;
            }
            _db.Tags.Remove(tagId);
            await _db.SaveChangesAsync();
            return tagId;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            var tags = await _db.Tags.ToListAsync();
            return tags;
        }

        public async Task<Tag?> GetAsync(Guid TagId)
        {
            var tagId = await _db.Tags.FirstOrDefaultAsync(t => t.Id == TagId);

            if (tagId == null)
            {
                return null;
            }
            return tagId;
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
          var existingTag = await _db.Tags.FirstOrDefaultAsync(x => x.Id == tag.Id);
            if(existingTag is not null)
            {
               existingTag.Name = tag.Name;
               existingTag.DisplayName = tag.DisplayName;

                await _db.SaveChangesAsync();
                return existingTag;
            }
            return null;

        }
    }
}
