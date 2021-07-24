using ShopDienTu.ViewModels.Catalog.Product;
using ShopDienTu.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopDienTu.ApiIntegration
{
    public interface IProductApiClient
    {
        Task<PagedResult<ProductVm>> GetAllPaging(GetProductPagingRequest request);
        Task<bool> CreateProduct(ProductCreateRequest request);
        Task<bool> UpdateProduct(ProductUpdateRequest request);
        Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request);
        Task<ProductVm> GetById(int id, string languageId);
        Task<List<ProductVm>> GetFeaturedProducts(string languageId, int take);
        Task<List<ProductVm>> GetLatestProducts(string languageId, int take);
        Task<ApiResult<bool>> DeleteProduct(int productId);
        Task<bool> AddViewCount(int productId);
        Task<bool> UpdatePrice(int productId, decimal newPrice);
        Task<bool> UpdateStock(int productId, int addedQuantity);
        Task<ApiResult<bool>> CreateLanguageOther(CreateLanguageOtherProductRequest request);
        Task<PagedResult<ProductVm>> GetPagingByCategoryId(GetProductPagingRequest request);
    }
}
