using ShopDienTu.ViewModels.Common;
using ShopDienTu.ViewModels.System.Roles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopDienTu.ApiIntegration
{
    public interface IRoleApiClient
    {
        Task<List<RoleVm>> GetAll();
    }
}
