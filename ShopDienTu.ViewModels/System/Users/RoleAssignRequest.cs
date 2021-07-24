using ShopDienTu.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopDienTu.ViewModels.System.Users
{
    public class RoleAssignRequest
    {
        public Guid Id { get; set; }
        public IList<SelectItem> Roles { get; set; } = new List<SelectItem>();
    }
}
