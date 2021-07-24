using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopDienTu.ViewModels.Catalog.Product
{
    public class CreateLanguageOtherProductRequest
    {
        public int ProductId { set; get; }
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
        [Required(ErrorMessage = "Không được để trống")]
        public string LanguageId { set; get; }
    }
}
