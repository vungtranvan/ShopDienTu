using ShopDienTu.ViewModels.Common;

namespace ShopDienTu.ViewModels.Catalog.Categories
{
    public class GetManageCategoryPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }

        public string LanguageId { get; set; }

    }
}
