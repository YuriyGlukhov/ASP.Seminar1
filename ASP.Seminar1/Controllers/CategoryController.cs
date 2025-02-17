using ASP.Seminar1.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.Seminar1.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        [HttpGet("getCategory")]
        public ActionResult<IEnumerable<Category>> GetCategory()
        {
            try
            {
                using (var context = new ProductContext())
                {
                    var categories = context.Categories.Select(x => new Category()
                    {
                        Name = x.Name,
                        Id = x.Id,
                        Description = x.Description
                    }).ToList();
                    return categories;
                }

            }
            catch
            {
                return StatusCode(500);
            }


        }

        [HttpPost("postCategory")]
        public IActionResult PostCategory([FromQuery] string name, string description)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (context.Categories.Any(x => x.Name.ToLower() == name.ToLower()))
                    {
                        return StatusCode(409);
                    }
                    else
                    {
                        context.Categories.Add(new Category()
                        {
                            Name = name,
                            Description = description

                        });
                        context.SaveChanges();

                        return Ok();
                    }
                }

            }
            catch
            {
                return StatusCode(500);
            }
        }


        [HttpDelete("deleteCategory")]
        public IActionResult DeleteCategory(string name)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (context.Categories.Any(x => x.Name.ToLower() == name.ToLower()))
                    {
                        var category = context.Categories.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
                        context.Categories.Remove(category);
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
