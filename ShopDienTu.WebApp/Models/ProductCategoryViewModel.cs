using ShopDienTu.ViewModels.Catalog.Categories;
using ShopDienTu.ViewModels.Catalog.Product;
using ShopDienTu.ViewModels.Common;

namespace ShopDienTu.WebApp.Models
{
    public class ProductCategoryViewModel
    {
        public CategoryVm Category { get; set; }
        public PagedResult<ProductVm> Products { get; set; }
    }
}
