using ShopDienTu.ViewModels.Utilities.Slider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopDienTu.ApiIntegration
{
    public interface ISliderApiClient
    {
        Task<List<SliderVm>> GetAll();
    }
}
