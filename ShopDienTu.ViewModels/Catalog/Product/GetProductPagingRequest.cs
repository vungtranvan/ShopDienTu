using ShopDienTu.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopDienTu.ViewModels.Catalog.Product
{
    public class GetProductPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }

        public string LanguageId { get; set; }

        public int? CategoryId { get; set; }
    }
}
