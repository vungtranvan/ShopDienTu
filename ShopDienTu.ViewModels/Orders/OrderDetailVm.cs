using System;
using System.Collections.Generic;
using System.Text;

namespace ShopDienTu.ViewModels.Orders
{
    public class OrderDetailVm
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public decimal Price { set; get; }
    }
}
