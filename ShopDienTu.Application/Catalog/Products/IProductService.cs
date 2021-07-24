using ShopDienTu.ViewModels.Catalog.Product;
using ShopDienTu.ViewModels.Catalog.ProductImages;
using ShopDienTu.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopDienTu.Application.Catalog.Products
{
    public interface IProductService
    {
        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductUpdateRequest request);
        Task<ApiResult<bool>> Delete(int productId);
        Task<ProductVm> GetById(int productId, string languageId);
        Task<PagedResult<ProductVm>> GetAllPaging(GetProductPagingRequest request);
        Task<bool> UpdatePrice(int productId, decimal newPrice);
        Task<bool> UpdateStock(int productId, int addedQuantity);
        Task<bool> AddViewcount(int productId);
        Task<int> AddImage(int productId, ProductImageCreateRequest request);
        Task<int> RemoveImage(int imageId);
        Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request);
        Task<ProductImageViewModel> GetImageById(int imageId);
        Task<List<ProductImageViewModel>> GetListImage(int productId);
        Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request);
        Task<List<ProductVm>> GetFeaturedProducts(string languageId, int take);
        Task<List<ProductVm>> GetLatestProducts(string languageId, int take);
        Task<PagedResult<ProductVm>> GetAllByCategoryId(GetProductPagingRequest request);
        Task<ApiResult<bool>> CreateLanguageOther(CreateLanguageOtherProductRequest request);
    }
}
