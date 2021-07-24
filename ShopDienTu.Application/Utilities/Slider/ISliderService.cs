using ShopDienTu.ViewModels.System.Roles;
using ShopDienTu.ViewModels.Utilities.Slider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopDienTu.Application.Utilities.Slider
{
    public interface ISliderService
    {
        Task<List<SliderVm>> GetAll();
    }
}
