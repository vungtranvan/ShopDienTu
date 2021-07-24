using ShopDienTu.Data.EF;
using ShopDienTu.Data.Entities;
using ShopDienTu.Utilities.Enum;
using ShopDienTu.ViewModels.Common;
using ShopDienTu.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopDienTu.Application.Orders
{
    public class OrderService : IOrderService
    {
        private readonly ShopDienTuDbContext _context;

        public OrderService(ShopDienTuDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CheckOut(CheckoutRequest request)
        {
            var oderDetail = new List<OrderDetail>();

            foreach (var item in request.OrderDetails)
            {
                oderDetail.Add(new OrderDetail()
                {
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Quantity = item.Quantity
                });

                var product = await _context.Products.FindAsync(item.ProductId);
                product.Stock -= item.Quantity;
            }
            var order = new Order
            {
                ShipName = request.Name,
                ShipEmail = request.Email,
                ShipAddress = request.Address,
                ShipPhoneNumber = request.Address,
                UserId = request.UserId,
                OrderDate = DateTime.Now,
                Status = OrderStatus.InProgress,
                OrderDetails = oderDetail
            };

            _context.Orders.Add(order);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<bool>();
            }
            else
            {
                return new ApiErrorResult<bool>("Thanh toán thất bại");
            }
        }
    }
}
