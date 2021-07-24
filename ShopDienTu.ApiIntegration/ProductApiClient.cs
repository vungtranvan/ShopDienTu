using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ShopDienTu.Utilities.Constants;
using ShopDienTu.ViewModels.Catalog.Product;
using ShopDienTu.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopDienTu.ApiIntegration
{
    public class ProductApiClient : BaseApiClient, IProductApiClient
    {
        public ProductApiClient(IHttpClientFactory httpClientFactory,
                  IHttpContextAccessor httpContextAccessor,
                   IConfiguration configuration)
           : base(httpClientFactory, httpContextAccessor, configuration)
        {
        }

        public async Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request)
        {
            var result = await PutAsync<ApiResult<bool>>($"/api/product/{id}/categories", request);
            return result;
        }
        public async Task<ApiResult<bool>> CreateLanguageOther(CreateLanguageOtherProductRequest request)
        {
            return await PostAsync<ApiResult<bool>>($"/api/product/{request.ProductId}/{request.LanguageId}/languageother", request);
        }

        public async Task<bool> CreateProduct(ProductCreateRequest request)
        {
            var client = CreateClient();
            var requestContent = new MultipartFormDataContent();

            if (request.ThumbnailImage != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ThumbnailImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbnailImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "thumbnailImage", request.ThumbnailImage.FileName);
            }

            requestContent.Add(new StringContent(request.Price.ToString()), "price");
            requestContent.Add(new StringContent(request.OriginalPrice.ToString()), "originalPrice");
            requestContent.Add(new StringContent(request.Stock.ToString()), "stock");
            requestContent.Add(new StringContent(request.Name.ToString()), "name");
            requestContent.Add(new StringContent(request.Description.ToString()), "description");

            requestContent.Add(new StringContent(request.Details.ToString()), "details");
            requestContent.Add(new StringContent(request.SeoDescription.ToString()), "seoDescription");
            requestContent.Add(new StringContent(request.SeoTitle.ToString()), "seoTitle");
            requestContent.Add(new StringContent(request.SeoAlias.ToString()), "seoAlias");
            requestContent.Add(new StringContent(request.LanguageId.ToString()), "languageId");

            var response = await client.PostAsync($"/api/product/", requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<ApiResult<bool>> DeleteProduct(int productId)
        {
            var result = await DeleteAsync<ApiResult<bool>>($"/api/product/{productId}");
            return result;
        }

        public async Task<bool> AddViewCount(int productId)
        {
            var result = await PathAsync<bool>($"/api/product/{productId}/viewCount", "");
            return result;
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var result = await PathAsync<bool>($"/api/product/{productId}/{newPrice}", "");
            return result;
        }

        public async Task<bool> UpdateStock(int productId, int addedQuantity)
        {
            var result = await PathAsync<bool>($"/api/product/{productId}/{addedQuantity}", "");
            return result;
        }

        public async Task<PagedResult<ProductVm>> GetAllPaging(GetProductPagingRequest request)
        {
            var data = await GetAsync<PagedResult<ProductVm>>(
               $"/api/product/paging?pageIndex={request.PageIndex}" +
               $"&pageSize={request.PageSize}" +
               $"&keyword={request.Keyword}&languageId={request.LanguageId}");
            return data;
        }

        public async Task<PagedResult<ProductVm>> GetPagingByCategoryId(GetProductPagingRequest request)
        {
            var data = await GetAsync<PagedResult<ProductVm>>(
               $"/api/product/paging-categoryid?pageIndex={request.PageIndex}" +
               $"&pageSize={request.PageSize}" + 
               $"&keyword={request.Keyword}&languageId={request.LanguageId}&categoryId={request.CategoryId}");
            return data;
        }

        public async Task<ProductVm> GetById(int id, string languageId)
        {
            var data = await GetAsync<ProductVm>($"/api/product/{id}/{languageId}");

            return data;
        }

        public async Task<List<ProductVm>> GetFeaturedProducts(string languageId, int take)
        {
            var data = await GetAsync<List<ProductVm>>($"/api/product/featured/{languageId}/{take}");

            return data;
        }

        public async Task<List<ProductVm>> GetLatestProducts(string languageId, int take)
        {
            var data = await GetAsync<List<ProductVm>>($"/api/product/latest/{languageId}/{take}");

            return data;
        }

        public async Task<bool> UpdateProduct(ProductUpdateRequest request)
        {
            var client = CreateClient();

            var requestContent = new MultipartFormDataContent();

            if (request.ThumbnailImage != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ThumbnailImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbnailImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "thumbnailImage", request.ThumbnailImage.FileName);
            }


            requestContent.Add(new StringContent(request.Name.ToString()), "name");
            requestContent.Add(new StringContent(request.Description.ToString()), "description");

            requestContent.Add(new StringContent(request.Details.ToString()), "details");
            requestContent.Add(new StringContent(request.SeoDescription.ToString()), "seoDescription");
            requestContent.Add(new StringContent(request.SeoTitle.ToString()), "seoTitle");
            requestContent.Add(new StringContent(request.SeoAlias.ToString()), "seoAlias");
            requestContent.Add(new StringContent(request.LanguageId.ToString()), "languageId");

            var response = await client.PutAsync($"/api/product/{request.Id}", requestContent);
            return response.IsSuccessStatusCode;
        }
    }
}
