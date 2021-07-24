using ShopDienTu.Utilities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopDienTu.ViewModels.Catalog.Categories
{
    public class CategoryVm
    {
        public int? Id { get; set; }

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

        public int? ParentId { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        public Status Status { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        public bool IsShowOnHome { get; set; } = true;

        [Required(ErrorMessage = "Không được để trống")]
        public int SortOrder { get; set; }
    }
}
