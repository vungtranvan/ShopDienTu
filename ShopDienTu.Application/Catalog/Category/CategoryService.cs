using ShopDienTu.Data.EF;
using ShopDienTu.ViewModels.Catalog.Categories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopDienTu.ViewModels.Common;
using ShopDienTu.Data.Entities;
using ShopDienTu.Utilities.Exceptions;

namespace ShopDienTu.Application.Catalog.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ShopDienTuDbContext _context;

        public CategoryService(ShopDienTuDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> Create(CategoryVm request)
        {
            var translations = new List<CategoryTranslation>()
            {
                new CategoryTranslation()
                {
                    Name = request.Name,
                    SeoDescription = request.SeoDescription,
                    SeoAlias = request.SeoAlias,
                    SeoTitle = request.SeoTitle,
                    LanguageId = request.LanguageId
                }
             };

            var category = new ShopDienTu.Data.Entities.Category()
            {
                SortOrder = request.SortOrder,
                IsShowOnHome = request.IsShowOnHome,
                ParentId = request.ParentId,
                Status = request.Status,
                CategoryTranslations = translations
            };
            _context.Categories.Add(category);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<bool>();
            }
            else
            {
                return new ApiErrorResult<bool>("Thêm thất bại");
            }
        }

        public async Task<ApiResult<bool>> CreateLanguageOther(CreateLanguageOtherRequest request)
        {
            var category = await _context.Categories.FindAsync(request.Id);
            var categoryTranslations = await _context.CategoryTranslations.FirstOrDefaultAsync(x => x.CategoryId == request.Id
           && x.LanguageId == request.LanguageId);

            if (categoryTranslations != null)
            {
                return new ApiErrorResult<bool>("Ngôn ngữ này đã tồn tại");
            }

            var translations = new CategoryTranslation()
            {
                CategoryId = category.Id,
                Name = request.Name,
                SeoDescription = request.SeoDescription,
                SeoAlias = request.SeoAlias,
                SeoTitle = request.SeoTitle,
                LanguageId = request.LanguageId
            };

            _context.CategoryTranslations.Add(translations);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<bool>();
            }
            else
            {
                return new ApiErrorResult<bool>("Thêm thất bại");
            }
        }
        public async Task<ApiResult<bool>> Update(CategoryVm request)
        {
            var category = await _context.Categories.FindAsync(request.Id);
            var categoryTranslations = await _context.CategoryTranslations.FirstOrDefaultAsync(x => x.CategoryId == request.Id
           && x.LanguageId == request.LanguageId);

            if (category == null || categoryTranslations == null) throw new EShopException($"Cannot find a category with id: {request.Id}");

            category.IsShowOnHome = request.IsShowOnHome;
            category.ParentId = request.ParentId;
            category.SortOrder = request.SortOrder;
            category.Status = request.Status;
            categoryTranslations.Name = request.Name;
            categoryTranslations.SeoAlias = request.SeoAlias;
            categoryTranslations.SeoDescription = request.SeoDescription;
            categoryTranslations.SeoTitle = request.SeoTitle;

            var result = await _context.SaveChangesAsync();
            if (result > 0 || category.Id > 0)
            {
                return new ApiSuccessResult<bool>();
            }
            else
            {
                return new ApiErrorResult<bool>("Cập nhật thất bại");
            }
        }

        public async Task<ApiResult<bool>> Delete(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null) throw new EShopException($"Cannot find category {categoryId}");

            _context.Categories.Remove(category);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ApiSuccessResult<bool>();
            }
            else
            {
                return new ApiErrorResult<bool>("Xóa không thành công");
            }
        }

        public async Task<List<CategoryVm>> GetAll(string languageId)
        {
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.Id equals ct.CategoryId
                        where ct.LanguageId == languageId && c.IsShowOnHome == true
                        select new { c, ct };
            return await query.OrderBy(x => x.c.SortOrder).Select(x => new CategoryVm()
            {
                Id = x.c.Id,
                Name = x.ct.Name,
                ParentId = x.c.ParentId,
                IsShowOnHome = x.c.IsShowOnHome,
                LanguageId = x.ct.LanguageId,
                SeoAlias = x.ct.SeoAlias,
                SeoDescription = x.ct.SeoDescription,
                SeoTitle = x.ct.SeoTitle,
                SortOrder = x.c.SortOrder,
                Status = x.c.Status
            }).ToListAsync();
        }

        public async Task<CategoryVm> GetById(string languageId, int id)
        {
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.Id equals ct.CategoryId
                        where ct.LanguageId == languageId && c.Id == id
                        select new { c, ct };
            return await query.Select(x => new CategoryVm()
            {
                Id = x.c.Id,
                Name = x.ct.Name,
                ParentId = x.c.ParentId,
                IsShowOnHome = x.c.IsShowOnHome,
                LanguageId = x.ct.LanguageId,
                SeoAlias = x.ct.SeoAlias,
                SeoDescription = x.ct.SeoDescription,
                SeoTitle = x.ct.SeoTitle,
                SortOrder = x.c.SortOrder,
                Status = x.c.Status
            }).FirstOrDefaultAsync();
        }

        public async Task<PagedResult<CategoryVm>> GetAllPaging(GetManageCategoryPagingRequest request)
        {
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.Id equals ct.CategoryId
                        where ct.LanguageId == request.LanguageId
                        select new { c, ct };

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.ct.Name.Contains(request.Keyword)
                || x.ct.LanguageId.Contains(request.Keyword)
                || x.c.Id.ToString().Contains(request.Keyword));
            }

            var totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).Select(x => new CategoryVm()
                {
                    Id = x.c.Id,
                    Name = x.ct.Name,
                    ParentId = x.c.ParentId,
                    LanguageId = x.ct.LanguageId,
                    SeoAlias = x.ct.SeoAlias,
                    SeoDescription = x.ct.SeoDescription,
                    SeoTitle = x.ct.SeoTitle,
                    IsShowOnHome = x.c.IsShowOnHome,
                    SortOrder = x.c.SortOrder,
                    Status = x.c.Status
                }).ToListAsync();

            var pagedRusult = new PagedResult<CategoryVm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };

            return pagedRusult;
        }
    }
}
