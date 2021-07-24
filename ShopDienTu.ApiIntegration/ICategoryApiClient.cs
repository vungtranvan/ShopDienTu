using ShopDienTu.ViewModels.Catalog.Categories;
using ShopDienTu.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopDienTu.ApiIntegration
{
    public interface ICategoryApiClient
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
