using ShopDienTu.ViewModels.Common;
using ShopDienTu.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopDienTu.ApiIntegration
{
    public interface IOrderApiClient
    {
        Task<ApiResult<bool>> CheckOut(CheckoutRequest request);
    }
}
