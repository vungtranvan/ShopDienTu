using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopDienTu.Application.Catalog.Products;
using ShopDienTu.ViewModels.Catalog.Product;
using ShopDienTu.ViewModels.Catalog.ProductImages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopDienTu.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{productId}/{languageId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int productId, string languageId)
        {
            var product = await _productService.GetById(productId, languageId);
            return Ok(product);
        }

        [HttpGet("paging")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetProductPagingRequest request)
        {
            var pro = await _productService.GetAllPaging(request);
            return Ok(pro);
        }

        [HttpGet("paging-categoryid")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPagingByCategoryId([FromQuery] GetProductPagingRequest request)
        {
            var pro = await _productService.GetAllByCategoryId(request);
            return Ok(pro);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _productService.Create(request);
            if (productId == 0)
            {
                return BadRequest();
            }
            var product = await _productService.GetById(productId, request.LanguageId);
            return Ok(product);
        }

        [HttpPut("{productId}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromRoute] int productId, [FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.Id = productId;
            var result = await _productService.Update(request);
            if (result == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var result = await _productService.Delete(productId);
            if (!result.IsSuccessed)
                return BadRequest();
            return Ok(result);
        }

        [HttpPatch("{productId}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice)
        {
            var result = await _productService.UpdatePrice(productId, newPrice);
            if (result)
                return Ok(result);
            return BadRequest();
        }

        [HttpPatch("{productId}/{addedQuantity}")]
        public async Task<IActionResult> UpdateStock(int productId, int addedQuantity)
        {
            var result = await _productService.UpdateStock(productId, addedQuantity);
            if (result)
                return Ok(result);
            return BadRequest();
        }

        [HttpPatch("{productId}/viewCount")]
        [AllowAnonymous]
        public async Task<IActionResult> AddViewCount(int productId)
        {
            var result = await _productService.AddViewcount(productId);
            if (result)
                return Ok(result);
            return BadRequest();
        }

        // ============ Image ============

        [HttpPost("{productId}/images")]
        [Authorize]
        public async Task<IActionResult> CreateImage(int productId, [FromForm] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _productService.AddImage(productId, request);
            if (imageId == 0)
                return BadRequest();
            var image = _productService.GetImageById(imageId);
            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
        }

        [HttpPut("{productId}/images/{imageId}")]
        [Authorize]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.UpdateImage(imageId, request);
            if (result == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{productId}/images/{imageId}")]
        [Authorize]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.RemoveImage(imageId);
            if (result == 0)
                return BadRequest();
            return Ok();
        }

        [HttpGet("{productId}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int productId, int imageId)
        {
            var image = await _productService.GetImageById(imageId);
            if (image == null)
                return BadRequest("Cannot find product");
            return Ok(image);
        }
        [HttpPut("{id}/categories")]
        public async Task<IActionResult> CategoryAssign(int id, [FromBody] CategoryAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productService.CategoryAssign(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("featured/{languageId}/{take}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFeaturedProducts(int take, string languageId)
        {
            var products = await _productService.GetFeaturedProducts(languageId, take);
            return Ok(products);
        }

        [HttpGet("latest/{languageId}/{take}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLatestProducts(int take, string languageId)
        {
            var products = await _productService.GetLatestProducts(languageId, take);
            return Ok(products);
        }

        [HttpPost("{id}/{languageId}/languageother")]
        public async Task<IActionResult> CreateLanguageOther(CreateLanguageOtherProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.CreateLanguageOther(request);
            if (!result.IsSuccessed)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
