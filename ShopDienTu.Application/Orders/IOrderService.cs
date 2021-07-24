using ShopDienTu.ViewModels.Common;
using ShopDienTu.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopDienTu.Application.Orders
{
    public interface IOrderService
    {
        Task<ApiResult<bool>> CheckOut(CheckoutRequest request);
    }
}
