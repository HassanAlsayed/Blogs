using Blogs.Models.Domain;

namespace Blogs.Repositories
{
    public interface ITagRepo
    {
        public Task<IEnumerable<Tag>> GetAllAsync(); 
        public Task<Tag?> GetAsync(Guid TagId);
        public Task<Tag> AddAsync(Tag tag);
        public Task<Tag?> UpdateAsync(Tag tag);
        public Task<Tag?> DeleteAsync(Guid TagId);


    }
}
