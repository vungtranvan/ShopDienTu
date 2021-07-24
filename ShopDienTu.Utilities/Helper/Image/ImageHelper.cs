using ShopDienTu.Utilities.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopDienTu.Utilities.Helper.Image
{
    public class ImageHelper
    {
        public static string PathImage(string nameImage)
        {
            StringBuilder sb = new StringBuilder(SystemConstants.AppSettings.LinkBaseAddress);
            sb.Append("/user-content/");
            if (!string.IsNullOrEmpty(nameImage))
                return sb.Append(nameImage).ToString();
            return sb.Append("no-image.png").ToString();
        }
    }
}
