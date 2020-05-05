using BasicAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicAPI.Services
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryModel>> GetCategoriesAsync();

        Task<Guid> GetCategoryIdByCodeAsync(string code);
    }
}