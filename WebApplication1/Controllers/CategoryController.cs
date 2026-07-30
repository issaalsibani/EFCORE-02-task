using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("Category")]
    public class CategoryController : ControllerBase

    {
        private ProjectContext _projectContext;
        public CategoryController(ProjectContext projectContext)
        {
            _projectContext = projectContext;
        }

        [HttpPost("AddCategory")]
        public void AddCategory(Category C)
        {

            _projectContext._categories.Add(C);
            _projectContext.SaveChanges();


        }

        [HttpDelete("RemoveCategory")]
        public void RemoveCategory(int Id)
        {
            Category C = _projectContext._categories.FirstOrDefault(C => C.CategoryId == Id);
            if (C != null)
            {

            }
            else
            {
                _projectContext._categories.Remove(p);
                _projectContext.SaveChanges();

            }

        }

        [HttpGet("GetCategory")]
        public Category GetCategory(int Id)
        {
            Category p = _projectContext._categories.FirstOrDefault(p => p.CategoryId == Id);
            return p;
        }

        [HttpGet("GetAllCategory")]
        public List<Category> GetCategory()
        {
            List<Category> categories = _projectContext._categories.ToList();
            return categories;
        }
    }
}
