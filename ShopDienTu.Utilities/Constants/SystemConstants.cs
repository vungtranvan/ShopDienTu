using System;
using System.Collections.Generic;
using System.Text;

namespace ShopDienTu.Utilities.Constants
{
   public class SystemConstants
    {
        public const string MainConnectionString = "ShopDienTuDb";
        public const string CartSession = "CartSession";
        public class ProductConstants
        {
            public const string NA = "N-A";
        }
        public class AppSettings
        {
            public const string DefaultLanguageId = "DefaultLanguageId";
            public const string Token = "Token";
            public const string BaseAddress = "BaseAddress";
            public const string LinkBaseAddress = "https://localhost:5001";
        }
        public class ProductSettings
        {
            public const int NumberOfProductFeatured = 4;
            public const int NumberOfProductLatest = 6;
        }
    }
}
