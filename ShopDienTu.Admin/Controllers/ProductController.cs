using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopDienTu.ApiIntegration;
using ShopDienTu.Utilities.Constants;
using ShopDienTu.ViewModels.Catalog.Product;
using ShopDienTu.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopDienTu.Admin.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly ILanguageApiClient _languageApiClient;
        private readonly ICategoryApiClient _categoryApiClient;
        public ProductController(IProductApiClient productApiClient, ILanguageApiClient languageApiClient, ICategoryApiClient categoryApiClient)
        {
            _productApiClient = productApiClient;
            _languageApiClient = languageApiClient;
            _categoryApiClient = categoryApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 3)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var request = new GetProductPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageId = languageId
            };
            var data = await _productApiClient.GetAllPaging(request);
            ViewBag.Keyword = keyword;

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Languages = new SelectList((await _languageApiClient.GetAll()).ResultObj, "Id", "Name", 0);
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Languages = new SelectList((await _languageApiClient.GetAll()).ResultObj, "Id", "Name", 0);
                return View();
            }


            var result = await _productApiClient.CreateProduct(request);
            if (result)
            {
                TempData["result"] = "Thêm mới sản phẩm thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm sản phẩm thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> CreateLanguageOther(int id, string languageId)
        {
            var lstLanguage = (await _languageApiClient.GetAll()).ResultObj;
            int selectDrop = 0;
            foreach (var item in lstLanguage)
            {
                if (item.Id == languageId)
                    selectDrop = lstLanguage.IndexOf(item);
            }
            ViewBag.Languages = new SelectList(lstLanguage, "Id", "Name", selectDrop);
            TempData["selectDrop"] = selectDrop;
            return View(new CreateLanguageOtherProductRequest() { ProductId = id, LanguageId = languageId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLanguageOther(CreateLanguageOtherProductRequest request)
        {
            var selectDrop = 0;
            if (TempData["selectDrop"] != null) selectDrop = (int)TempData["selectDrop"];
            ViewBag.Languages = new SelectList((await _languageApiClient.GetAll()).ResultObj, "Id", "Name", selectDrop);
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var product = await _productApiClient.GetById(request.ProductId, request.LanguageId);
            if (product != null)
            {
                ModelState.AddModelError("LanguageId", "Ngôn ngữ sản phẩm này đã tồn tại");
                return View(request);
            }

            var result = await _productApiClient.CreateLanguageOther(request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Thêm mới ngôn ngữ sản phẩm thành công";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Thêm ngôn ngữ sản phẩm thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, string languageId)
        {
            var product = await _productApiClient.GetById(id, languageId);
            var editVm = new ProductUpdateRequest()
            {
                Id = product.Id,
                Details = product.Details,
                Description = product.Description,
                Name = product.Name,
                SeoAlias = product.SeoAlias,
                SeoDescription = product.SeoDescription,
                SeoTitle = product.SeoTitle
            };
            var lstLanguage = (await _languageApiClient.GetAll()).ResultObj;
            int selectDrop = 0;
            foreach (var item in lstLanguage)
            {
                if (item.Id == languageId)
                    selectDrop = lstLanguage.IndexOf(item);
            }
            ViewBag.Languages = new SelectList(lstLanguage, "Id", "Name", selectDrop);
            return View(editVm);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var lstLanguage = (await _languageApiClient.GetAll()).ResultObj;
                int selectDrop = 0;
                foreach (var item in lstLanguage)
                {
                    if (item.Id == request.LanguageId)
                        selectDrop = lstLanguage.IndexOf(item);
                }
                ViewBag.Languages = new SelectList(lstLanguage, "Id", "Name", selectDrop);
                return View();
            }


            var result = await _productApiClient.UpdateProduct(request);
            if (result)
            {
                TempData["result"] = "Cập nhật sản phẩm thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Cập nhật phẩm thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int productId)
        {
            var result = await _productApiClient.DeleteProduct(productId);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Xóa sản phẩm thành công";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> CategoryAssign(int id)
        {
            var roleAssignRequest = await GetCategoryAssignRequest(id);
            return View(roleAssignRequest);
        }

        [HttpPost]
        public async Task<IActionResult> CategoryAssign(CategoryAssignRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _productApiClient.CategoryAssign(request.Id, request);

            if (result.IsSuccessed)
            {
                TempData["result"] = "Cập nhật danh mục thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            var roleAssignRequest = await GetCategoryAssignRequest(request.Id);

            return View(roleAssignRequest);
        }

        private async Task<CategoryAssignRequest> GetCategoryAssignRequest(int id)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var productObj = await _productApiClient.GetById(id, languageId);
            var categories = await _categoryApiClient.GetAll(languageId);
            var categoryAssignRequest = new CategoryAssignRequest();
            foreach (var role in categories)
            {
                categoryAssignRequest.Categories.Add(new SelectItem()
                {
                    Id = role.Id.ToString(),
                    Name = role.Name,
                    Selected = productObj.Categories.Contains(role.Name)
                });
            }
            return categoryAssignRequest;
        }
    }
}
