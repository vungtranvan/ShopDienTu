using ShopDienTu.Data.EF;
using ShopDienTu.ViewModels.Utilities.Slider;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopDienTu.Utilities.Enum;

namespace ShopDienTu.Application.Utilities.Slider
{
    public class SliderService : ISliderService
    {
        private readonly ShopDienTuDbContext _context;

        public SliderService(ShopDienTuDbContext context)
        {
            _context = context;
        }


        public async Task<List<SliderVm>> GetAll()
        {
            var slides = await _context.Sliders.Where(x => x.Status.Equals(Status.Active)).OrderBy(x => x.SortOrder).Select(x => new SliderVm()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Url = x.Url,
                Image = x.Image
            }).ToListAsync();

            return slides;
        }
    }
}
