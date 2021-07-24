using Microsoft.AspNetCore.Mvc;
using ShopDienTu.ApiIntegration;
using ShopDienTu.ViewModels.Catalog.Product;
using ShopDienTu.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopDienTu.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;

        public ICategoryApiClient _categoryApiClient;

        public ProductController(IProductApiClient productApiClient, ICategoryApiClient categoryApiClient)
        {
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
        }
        public async Task<IActionResult> Detail(int id, string culture, string ceoalias)
        {
            await _productApiClient.AddViewCount(id);
            var product = await _productApiClient.GetById(id, culture);
            if (product == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(new ProductDetailViewModel()
            {
                Product = product
            });
        }

        public async Task<IActionResult> Category(int id, string culture, string ceoalias, string keyword = "", int page = 1)
        {
            var category = await _categoryApiClient.GetById(culture, id);
            if (category == null)
            {
                return RedirectToAction("Error", "Home");
            }
            var product = await _productApiClient.GetPagingByCategoryId(new GetProductPagingRequest()
            {
                Keyword = keyword,
                CategoryId = id,
                PageIndex = page,
                LanguageId = culture,
                PageSize = 10
            });

            return View(new ProductCategoryViewModel()
            {
                Category = category,
                Products = product
            });
        }
    }
}
