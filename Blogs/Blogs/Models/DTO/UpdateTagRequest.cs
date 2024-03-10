namespace Blogs.Models.DTO
{
    public class UpdateTagRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
