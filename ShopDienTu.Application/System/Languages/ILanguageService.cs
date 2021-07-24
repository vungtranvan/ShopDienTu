using ShopDienTu.ViewModels.Common;
using ShopDienTu.ViewModels.System.Languages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopDienTu.Application.System.Languages
{
    public interface ILanguageService
    {
        Task<ApiResult<List<LanguageVm>>> GetAll();
    }
}
