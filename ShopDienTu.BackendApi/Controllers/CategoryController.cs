using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopDienTu.Application.Catalog.Category;
using ShopDienTu.ViewModels.Catalog.Categories;
using System.Threading.Tasks;

namespace ShopDienTu.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(
            ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(string languageId)
        {
            var products = await _categoryService.GetAll(languageId);
            return Ok(products);
        }

        [HttpGet("paging")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetManageCategoryPagingRequest request)
        {
            var category = await _categoryService.GetAllPaging(request);
            return Ok(category);
        }

        [HttpGet("{id}/{languageId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(string languageId, int id)
        {
            var category = await _categoryService.GetById(languageId, id);
            return Ok(category);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CategoryVm request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _categoryService.Create(request);
            if (!result.IsSuccessed)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPost("{id}/{languageId}/languageother")]
        public async Task<IActionResult> CreateLanguageOther(CreateLanguageOtherRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _categoryService.CreateLanguageOther(request);
            if (!result.IsSuccessed)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> Update([FromRoute] int categoryId, CategoryVm request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.Id = categoryId;
            var result = await _categoryService.Update(request);
            if (!result.IsSuccessed)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> Delete(int categoryId)
        {
            var result = await _categoryService.Delete(categoryId);
            if (!result.IsSuccessed)
                return BadRequest();
            return Ok(result);
        }
    }
}
