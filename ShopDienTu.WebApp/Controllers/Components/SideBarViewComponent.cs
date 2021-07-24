using Microsoft.AspNetCore.Mvc;
using ShopDienTu.ApiIntegration;
using System.Globalization;
using System.Threading.Tasks;

namespace ShopDienTu.WebApp.Controllers.Components
{
    public class SideBarViewComponent : ViewComponent
    {
        private readonly ICategoryApiClient _categoryApiClient;
        public SideBarViewComponent(ICategoryApiClient categoryApiClient)
        {
            _categoryApiClient = categoryApiClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var culture = CultureInfo.CurrentCulture.Name;
            var items = await _categoryApiClient.GetAll(culture);
            return View(items);
        }
    }
}
