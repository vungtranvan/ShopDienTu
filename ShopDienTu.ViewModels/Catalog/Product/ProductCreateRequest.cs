using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopDienTu.ViewModels.Catalog.Product
{
    public class ProductCreateRequest
    {
        [Display(Name = "Giá bán")]
        [Required(ErrorMessage = "Không được để trống")]
        public decimal Price { get; set; }

        [Display(Name = "Giá gốc")]
        [Required(ErrorMessage = "Không được để trống")]
        public decimal OriginalPrice { get; set; }

        [Display(Name = "Số Lượng")]
        public int Stock { get; set; }

        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Không được để trống")]
        public string Name { set; get; }

        [Display(Name = "Mô tả")]
        [Required(ErrorMessage = "Không được để trống")]
        public string Description { set; get; }
        [Display(Name = "Chi Tiết")]
        [Required(ErrorMessage = "Không được để trống")]
        public string Details { set; get; }

        [Required(ErrorMessage = "Không được để trống")]
        public string SeoDescription { set; get; }

        [Required(ErrorMessage = "Không được để trống")]
        public string SeoTitle { set; get; }

        [Required(ErrorMessage = "Không được để trống")]
        public string SeoAlias { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        public string LanguageId { set; get; }
        public bool? IsFeatured { get; set; }

        public IFormFile ThumbnailImage { get; set; }
    }
}
