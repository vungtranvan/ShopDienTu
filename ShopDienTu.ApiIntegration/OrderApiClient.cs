using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ShopDienTu.ViewModels.Common;
using ShopDienTu.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopDienTu.ApiIntegration
{
    public class OrderApiClient : BaseApiClient, IOrderApiClient
    {
        public OrderApiClient(IHttpClientFactory httpClientFactory,
                  IHttpContextAccessor httpContextAccessor,
                   IConfiguration configuration)
           : base(httpClientFactory, httpContextAccessor, configuration)
        {
        }

        public async Task<ApiResult<bool>> CheckOut(CheckoutRequest request)
        {
            return await PostAsync<ApiResult<bool>>("/api/order", request);
        }
    }
}
