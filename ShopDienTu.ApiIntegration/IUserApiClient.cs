using ShopDienTu.ViewModels.Common;
using ShopDienTu.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopDienTu.ApiIntegration
{
    public interface IUserApiClient
    {
        Task<ApiResult<string>> Authenticate(LoginRequest request);
        Task<ApiResult<PagedResult<UserVm>>> GetUsersPagings(GetUserPagingRequest request);

        Task<ApiResult<bool>> Register(RegisterRequest request);

        Task<ApiResult<UserVm>> GetById(Guid id);
        Task<ApiResult<UserVm>> GetByUserName(string userName);
        Task<ApiResult<bool>> Update(Guid id, UpdateUserRequest request);
        Task<ApiResult<bool>> Delete(Guid id);
        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
    }
}
