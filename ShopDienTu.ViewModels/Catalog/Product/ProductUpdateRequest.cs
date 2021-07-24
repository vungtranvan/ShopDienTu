using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopDienTu.ViewModels.Catalog.Product
{
    public class ProductUpdateRequest
    {
        [Display(Name = "Mã")]
        public int Id { get; set; }

        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Không được để trống")]
        public string Name { set; get; }
        [Required(ErrorMessage = "Không được để trống")]
        public string Description { set; get; }
        [Required(ErrorMessage = "Không được để trống")]
        public string Details { set; get; }

        [Required(ErrorMessage = "Không được để trống")]
        public string SeoDescription { set; get; }

        [Required(ErrorMessage = "Không được để trống")]
        public string SeoTitle { set; get; }

        [Required(ErrorMessage = "Không được để trống")]
        public string SeoAlias { get; set; }

        [Display(Name = "Ngôn Ngữ")]
        public string LanguageId { set; get; }
        public bool? IsFeatured { get; set; }
        public IFormFile ThumbnailImage { get; set; }
    }
}
