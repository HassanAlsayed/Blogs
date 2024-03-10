using Blogs.Data;
using Blogs.Models.Domain;
using Blogs.Models.DTO;
using Blogs.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Blogs.Controllers
{
    public class AdminTagController : Controller
    {
        private readonly ITagRepo _repo;

        public AdminTagController(ITagRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _repo.GetAllAsync();
            return View(tags);
        }

        
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            //mapping dto(addtagRequest) to domain model(Tag)
           
                var tag = new Tag
                {
                    Name = addTagRequest.Name,
                    DisplayName = addTagRequest.DisplayName
                };
                await _repo.AddAsync(tag);
                return RedirectToAction("GetAll", "AdminTag");
            
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid TagId)
        {
            var tag = await _repo.GetAsync(TagId);
            if(tag is not null)
            {
                var editTag = new UpdateTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTag);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateTagRequest updateTagRequest)
        {
            if (ModelState.IsValid)
            {
                var tag = new Tag
                {
                    Id = updateTagRequest.Id,
                    Name = updateTagRequest.Name,
                    DisplayName = updateTagRequest.DisplayName
                };

                var updatedTag = await _repo.UpdateAsync(tag);
                if (updatedTag is not null)
                {
                    return RedirectToAction("GetAll");
                }
            }
                return View(null);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid TagId)
        {
            var tag = await _repo.GetAsync(TagId);
            if (tag == null)
            {
                return View(null);
            }
            return View(tag);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteTag(Guid TagId)
        {
                await _repo.DeleteAsync(TagId);
                return RedirectToAction("GetAll");
        }
    }
}


