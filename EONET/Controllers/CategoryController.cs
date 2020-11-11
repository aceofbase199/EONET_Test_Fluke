using EONET.BL.Abstraction;
using EONET.BL.Models;
using EONET.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EONET.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("/categories")]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            var models = await _categoryService.GetCategoriesAsync();

            if (models == null)
                return Json(new { status = "error", message = "Couldn't load categories" });

            return Ok(new Response<List<CategoryModel>>(models));
        }

    }
}