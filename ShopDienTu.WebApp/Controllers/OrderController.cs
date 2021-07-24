using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopDienTu.ApiIntegration;
using ShopDienTu.Utilities.Constants;
using ShopDienTu.ViewModels.Orders;
using ShopDienTu.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopDienTu.WebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IOrderApiClient _orderApiClient;
        public OrderController(IUserApiClient userApiClient, IOrderApiClient orderApiClient)
        {
            _userApiClient = userApiClient;
            _orderApiClient = orderApiClient;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Checkout()
        {
            return View(GetCheckoutViewModel());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Checkout(CheckoutViewModel request)
        {
            var model = GetCheckoutViewModel();
            if (model.CartItems.Count <= 0)
            {
                return RedirectToAction("Index", "Home");
            }
            var orderDetails = new List<OrderDetailVm>();
            foreach (var item in model.CartItems)
            {
                orderDetails.Add(new OrderDetailVm()
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                });
            }
            var user = await _userApiClient.GetByUserName(request.CheckoutModel.UserName.Trim());
            var checkoutRequest = new CheckoutRequest()
            {
                UserId = user.ResultObj.Id,
                Address = request.CheckoutModel.Address,
                Name = request.CheckoutModel.Name,
                Email = request.CheckoutModel.Email,
                PhoneNumber = request.CheckoutModel.PhoneNumber,
                OrderDetails = orderDetails
            };

            var result = await _orderApiClient.CheckOut(checkoutRequest);
            if (result.IsSuccessed)
            {
                HttpContext.Session.Remove(SystemConstants.CartSession);
                TempData["SuccessMsg"] = "Order puschased successful";
            }
            TempData["ErrorMsg"] = "Order error";
            return View(model);
        }

        private CheckoutViewModel GetCheckoutViewModel()
        {
            var session = HttpContext.Session.GetString(SystemConstants.CartSession);
            List<CartItemViewModel> currentCart = new List<CartItemViewModel>();
            if (session != null)
                currentCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);
            var checkoutVm = new CheckoutViewModel()
            {
                CartItems = currentCart,
                CheckoutModel = new CheckoutRequest()
            };
            return checkoutVm;
        }
    }
}
