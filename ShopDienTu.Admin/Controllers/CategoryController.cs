using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopDienTu.ApiIntegration;
using ShopDienTu.Utilities.Constants;
using ShopDienTu.ViewModels.Catalog.Categories;
using System.Threading.Tasks;

namespace ShopDienTu.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILanguageApiClient _languageApiClient;
        private readonly ICategoryApiClient _categoryApiClient;
        public CategoryController(ILanguageApiClient languageApiClient, ICategoryApiClient categoryApiClient)
        {
            _languageApiClient = languageApiClient;
            _categoryApiClient = categoryApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 3)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var request = new GetManageCategoryPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageId = languageId
            };
            var data = await _categoryApiClient.GetAllPaging(request);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryVm request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Languages = new SelectList((await _languageApiClient.GetAll()).ResultObj, "Id", "Name", 0);
                return View();
            }

            var result = await _categoryApiClient.Create(request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Thêm mới danh mục thành công";
                return RedirectToAction("Index");
            }
            ViewBag.Languages = new SelectList((await _languageApiClient.GetAll()).ResultObj, "Id", "Name", 0);
            ModelState.AddModelError("", "Thêm danh mục thất bại");
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
            return View(new CreateLanguageOtherRequest() { Id = id, LanguageId = languageId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLanguageOther(CreateLanguageOtherRequest request)
        {
            var selectDrop = 0;
            if (TempData["selectDrop"] != null) selectDrop = (int)TempData["selectDrop"];
            ViewBag.Languages = new SelectList((await _languageApiClient.GetAll()).ResultObj, "Id", "Name", selectDrop);
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var category = await _categoryApiClient.GetById(request.LanguageId, request.Id);
            if (category != null)
            {
                ModelState.AddModelError("LanguageId", "Ngôn ngữ danh mục này đã tồn tại");
                return View(request);
            }

            var result = await _categoryApiClient.CreateLanguageOther(request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Thêm mới ngôn ngữ danh mục thành công";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Thêm ngôn ngữ danh mục thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, string languageId)
        {
            var category = await _categoryApiClient.GetById(languageId, id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryVm request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var result = await _categoryApiClient.Update(request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Cập nhật danh mục thành công";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Cập danh mục thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryApiClient.Delete(id);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Xóa danh mục thành công";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error", "Home");
        }
    }
}
