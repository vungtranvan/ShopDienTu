using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ShopDienTu.ViewModels.Catalog.Categories;
using ShopDienTu.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopDienTu.ApiIntegration
{
    public class CategoryApiClient : BaseApiClient, ICategoryApiClient
    {
        public CategoryApiClient(IHttpClientFactory httpClientFactory,
                  IHttpContextAccessor httpContextAccessor,
                   IConfiguration configuration)
           : base(httpClientFactory, httpContextAccessor, configuration)
        {
        }

        public async Task<ApiResult<bool>> Create(CategoryVm request)
        {
            return await PostAsync<ApiResult<bool>>("/api/category", request);
        }
        public async Task<ApiResult<bool>> CreateLanguageOther(CreateLanguageOtherRequest request)
        {
            return await PostAsync<ApiResult<bool>>($"/api/category/{request.Id}/{request.LanguageId}/languageother", request);
        }
        public async Task<ApiResult<bool>> Update(CategoryVm request)
        {
            var response = await PutAsync<ApiResult<bool>>($"/api/category/{request.Id}", request);
            return response;
        }

        public async Task<ApiResult<bool>> Delete(int categoryId)
        {
            var response = await DeleteAsync<ApiResult<bool>>($"/api/category/{categoryId}");
            return response;
        }

        public async Task<List<CategoryVm>> GetAll(string languageId)
        {
            var response = await GetAsync<List<CategoryVm>>("/api/category?languageId=" + languageId);
            return response;
        }

        public async Task<PagedResult<CategoryVm>> GetAllPaging(GetManageCategoryPagingRequest request)
        {
            var response = await GetAsync<PagedResult<CategoryVm>>(
              $"/api/category/paging?pageIndex={request.PageIndex}" +
              $"&pageSize={request.PageSize}" +
              $"&keyword={request.Keyword}&languageId={request.LanguageId}");
            return response;
        }

        public async Task<CategoryVm> GetById(string languageId, int id)
        {
            var response = await GetAsync<CategoryVm>($"/api/category/{id}/{languageId}");
            return response;
        }
    }
}
