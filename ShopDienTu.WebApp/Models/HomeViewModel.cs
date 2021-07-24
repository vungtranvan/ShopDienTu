using ShopDienTu.ViewModels.Catalog.Product;
using ShopDienTu.ViewModels.Utilities.Slider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopDienTu.WebApp.Models
{
    public class HomeViewModel
    {
        public List<SliderVm> Slides { get; set; }

        public List<ProductVm> FeaturedProducts { get; set; }

        public List<ProductVm> LatestProducts { get; set; }
    }
}
