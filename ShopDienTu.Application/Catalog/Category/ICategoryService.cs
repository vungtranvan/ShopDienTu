using ShopDienTu.ViewModels.Catalog.Categories;
using ShopDienTu.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopDienTu.Application.Catalog.Category
{
    public interface ICategoryService
    {
        Task<List<CategoryVm>> GetAll(string languageId);
        Task<PagedResult<CategoryVm>> GetAllPaging(GetManageCategoryPagingRequest request);
        Task<CategoryVm> GetById(string languageId, int id);
        Task<ApiResult<bool>> Create(CategoryVm request);
        Task<ApiResult<bool>> Update(CategoryVm request);
        Task<ApiResult<bool>> Delete(int categoryId);
        Task<ApiResult<bool>> CreateLanguageOther(CreateLanguageOtherRequest request);
    }
}
