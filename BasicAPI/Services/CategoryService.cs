using BasicAPI.Constants;
using BasicAPI.Data;
using BasicAPI.Entities;
using BasicAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext context;
        private readonly IMemoryCache cache;

        public CategoryService(DataContext context, IMemoryCache memoryCache)
        {
            this.context = context;
            cache = memoryCache;
        }

        public async Task<Guid> GetCategoryIdByCodeAsync(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentNullException();
            }

            return (await GetCategoryEntitiesAsync()).Where(c => c.Code == code).Select(c => c.Id).FirstOrDefault();
        }

        public async Task<IEnumerable<CategoryModel>> GetCategoriesAsync()
        {
            return (await GetCategoryEntitiesAsync()).Select(c => new CategoryModel(c));
        }

        #region Private Methods
        private async Task<IEnumerable<Category>> GetCategoryEntitiesAsync()
        {
            var cacheEntry = await cache.GetOrCreateAsync(CacheKey.Category, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24);
                return await context.Categories.ToListAsync();
            });

            return cacheEntry;
        }
        #endregion
    }
}