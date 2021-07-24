using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopDienTu.ViewModels.Catalog.Categories
{
   public class CreateLanguageOtherRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        public string Name { get; set; }

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
