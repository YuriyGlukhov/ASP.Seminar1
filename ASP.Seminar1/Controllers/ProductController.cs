using ASP.Seminar1.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.Seminar1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet("getProduct")]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            try
            {
                using (var context = new ProductContext())
                {
                    var products = context.Products.Select(x => new Product() 
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Description = x.Description,
                        Category = x.Category,
                        Cost = x.Cost,
                        CategoryId = x.CategoryId,
                    }).ToList();
                    return products;
                }

            }
            catch 
            {
                return StatusCode(500);
            } 
        }

        [HttpPost("postProduct")]
        public IActionResult PostProduct(string name, string description, int price, int groupId)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (!context.Products.Any(x => x.Name.ToLower() == name.ToLower()))
                    {
                        context.Products.Add(new Product()
                        {
                            Name = name,
                            Description = description,
                            Cost = price,
                            CategoryId = groupId
                        });
                        context.SaveChanges();

                        return Ok();
                    }
                    else
                    {

                       return StatusCode(409);
                    }
                }

            }
            catch
            {
                return StatusCode(500);
            }
        }


        [HttpDelete("deleteProduct")]
        public IActionResult DeleteProduct(string name)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (context.Products.Any(x => x.Name.ToLower() == name.ToLower()))
                    {
                        var product = context.Products.FirstOrDefault(x =>x.Name.ToLower() == name.ToLower());
                        context.Products.Remove(product);
                        context.SaveChanges(); 
                        
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(409);
                    }
                }

            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
