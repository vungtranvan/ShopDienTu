using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ShopDienTu.ViewModels.Utilities.Slider;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopDienTu.ApiIntegration
{
    public class SliderApiClient : BaseApiClient, ISliderApiClient
    {
        public SliderApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpClientFactory, httpContextAccessor, configuration)
        { }

        public async Task<List<SliderVm>> GetAll()
        {
            return await GetAsync<List<SliderVm>>("/api/slider");
        }
    }
}
