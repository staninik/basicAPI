using BasicAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await categoryService.GetCategoriesAsync();
            return Ok(result);
        }
    }
}