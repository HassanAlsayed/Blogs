 using Blogs.Models.Domain;
using Blogs.Models.DTO;
using Blogs.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blogs.Controllers
{
    public class AdminBlogPostController : Controller
    {
        private readonly ITagRepo _repo;
        private readonly IBlogPostRepo _postRepo;
        private readonly IWebHostEnvironment _webHost;

        public AdminBlogPostController(ITagRepo repo,IBlogPostRepo postRepo,IWebHostEnvironment webHost)
        {
            _repo = repo;
            _postRepo = postRepo;
            _webHost = webHost;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var blogPosts = await _postRepo.GetAllAsync();
            return View(blogPosts);
        }

        
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await _repo.GetAllAsync();

            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
        };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPost,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwRoot = _webHost.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwRoot, @"images\product");

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    addBlogPost.FeaturedImageUrl = @"\images\product\" + fileName;
                }

                var blogPost = new BlogPost
                {

                    Heading = addBlogPost.Heading,
                    PageTitle = addBlogPost.PageTitle,
                    Content = addBlogPost.Content,
                    ShortDescription = addBlogPost.ShortDescription,
                    FeaturedImageUrl = addBlogPost.FeaturedImageUrl,
                    PublishedDate = addBlogPost.PublishedDate,
                    Author = addBlogPost.Author,
                    Visible = addBlogPost.Visible,
                };


                // Map tags from selected tags
                var selectedTags = new List<Tag>();
                foreach (var selectTagId in addBlogPost.SelectedTags)
                {
                    var selectedTagId = Guid.Parse(selectTagId);
                    var existingTag = await _repo.GetAsync(selectedTagId);
                    if (existingTag != null)
                    {
                        selectedTags.Add(existingTag);
                    }

                }

                // Map tags back to the domain model
                blogPost.Tags = selectedTags;

                await _postRepo.AddAsync(blogPost);
                return RedirectToAction("GetAll");
            }
            return View();
             
        }



        [HttpGet]
        public async Task<IActionResult> Delete(Guid BlogId)
        {
            var blogPost = await _postRepo.GetAsync(BlogId);
            return View(blogPost);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteBlog(Guid BlogId)
        {
            var blogPost = await _postRepo.DeleteAsync(BlogId);
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid BlogId)
        {
            var blogPost = await _postRepo.GetAsync(BlogId);

            if(blogPost != null)
            {
                var updateBlogPost = new UpdateBlogPostRequest
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    Author = blogPost.Author,
                    PublishedDate = blogPost.PublishedDate,
                    ShortDescription = blogPost.ShortDescription,
                   
                    Visible = blogPost.Visible,

                };
                return View(updateBlogPost);
            }
            return View(null);
            
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateBlogPostRequest updateBlogPost,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwRoot = _webHost.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwRoot, @"images\product");

                    if (!String.IsNullOrEmpty(updateBlogPost.FeaturedImageUrl))
                    {
                        // Delete old image
                        var old = Path.Combine(wwRoot, updateBlogPost.FeaturedImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(old))
                        {
                            System.IO.File.Delete(old);
                        }

                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    updateBlogPost.FeaturedImageUrl = @"\images\product\" + fileName;
                }

                var blogPost = new BlogPost
                {
                    Id = updateBlogPost.Id,
                    Heading = updateBlogPost.Heading,
                    PageTitle = updateBlogPost.PageTitle,
                    Content = updateBlogPost.Content,
                    FeaturedImageUrl = updateBlogPost.FeaturedImageUrl,
                    Author = updateBlogPost.Author,
                    PublishedDate = updateBlogPost.PublishedDate,
                    ShortDescription = updateBlogPost.ShortDescription,

                    Visible = updateBlogPost.Visible,

                };
                var updateBlog = await _postRepo.UpdateAsync(blogPost);

                if (updateBlog is not null)
                {
                    return RedirectToAction("GetAll");
                }
            }
            return View();

            }

    }

}




   
    //    // Delete old image
    //    var old = Path.Combine(wwRoot, productVM.Product.ImageUrl.TrimStart('\\'));

    //    if (System.IO.File.Exists(old))
    //    {
    //        System.IO.File.Delete(old);
    //    }
    //}

   
