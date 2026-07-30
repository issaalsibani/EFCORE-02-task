using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("Product")]
    public class ProductController : ControllerBase
    {
        private ProjectContext _projectContext;
        public ProductController(ProjectContext projectContext)
        {
            _projectContext = projectContext;
        }
        [HttpPost("AddProduct")]
        public IActionResult AddProduct(Product P)
        {

            _projectContext._products.Add(P);
            _projectContext.SaveChanges();

            return Ok();
        }

        [HttpDelete("RemoveProduct")]
        public void RemoveProduct(int Id)
        {
            Product p = _projectContext._products.FirstOrDefault(p => p.ProductId == Id);
            if (p != null)
            {

            }
            else
            {
                _projectContext._products.Remove(p);
                _projectContext.SaveChanges();

            }     

        }

        [HttpGet("GetProduct")]
        public Product GetProduct(int Id)
        {
            Product p = _projectContext._products.FirstOrDefault(p => p.ProductId == Id);
            return p;
        }

        [HttpGet("GetAllProduct")]
        public  List<Product> GetProducts()
        {
            List<Product> products = _projectContext._products.ToList();
            return products;
        }

        
    }
}
