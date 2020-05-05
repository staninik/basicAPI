using BasicAPI.Constants;
using BasicAPI.Data;
using BasicAPI.Entities;
using BasicAPI.Exceptions;
using BasicAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BasicAPI.Services
{
    public class WebsiteService : IWebsiteService
    {
        private readonly DataContext context;
        private readonly ICategoryService categoryService;
        private readonly IUserService userService;

        public WebsiteService(DataContext context, ICategoryService categoryService, IUserService userService)
        {
            this.context = context;
            this.categoryService = categoryService;
            this.userService = userService;
        }

        public async Task<WebsiteModel> GetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException();
            }

            return await context.Websites.Include(w => w.Category)
                .Include(w => w.Login).Where(w => w.Id == id).Select(w => new WebsiteModel(w)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<WebsiteModel>> GetAsync(QueryDescriptor filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException();
            }

            var query = context.Websites.Select(w => new WebsiteModel
            {
                Id = w.Id,
                Name = w.Name,
                URL = w.URL,
                HomepageSnapshot = w.HomepageSnapshot,
                UserId = w.UserId,
                UserEmail = w.Login.Email,
                CategoryCode = w.Category.Code,
                Category = w.Category.Name,
            });

            if (filter.SortDescriptor != null)
            {
                query = ApplySorting(query, filter.SortDescriptor);
            }

            return await query.Skip(filter.Skip).Take(filter.Take).ToListAsync();
        }

        public async Task<Guid> AddAsync(WebsiteAddModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            await ValidateNameUniquenessAsync(model.Name);

            Guid categoryId = await GetCategoryIdAsync(model.CategoryCode);

            User login = userService.CreateUser(model.Login);

            Website website = new Website
            {
                Name = model.Name,
                URL = model.URL,
                CategoryId = categoryId,
                HomepageSnapshot = Convert.FromBase64String(model.HomepageSnapshot),
                Login = login
            };

            context.Websites.Add(website);
            await context.SaveChangesAsync();

            return website.Id;
        }

        public async Task<WebsiteUpdateModel> UpdateAsync(Guid id, WebsiteUpdateModel model)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException();
            }

            var website = await context.Websites.Include(w => w.Category)
                                                .Include(w => w.Login)
                                                .FirstOrDefaultAsync(w => w.Id.Equals(id));

            if (website is null)
            {
                throw new NotFoundException("Website not found.");
            }

            if (!string.IsNullOrWhiteSpace(website.Name) && !website.Name.ToLower().Equals(model.Name.ToLower()))
            {
                await ValidateNameUniquenessAsync(model.Name);
                website.Name = model.Name;
            }

            if (!string.IsNullOrWhiteSpace(website.CategoryCode) && !website.CategoryCode.ToUpper().Equals(model.CategoryCode.ToUpper()))
            {
                website.CategoryId = await GetCategoryIdAsync(model.CategoryCode);
            }

            if (!string.IsNullOrWhiteSpace(model.URL))
            {
                website.URL = model.URL;
            }

            if (!string.IsNullOrWhiteSpace(model.HomepageSnapshot))
            {
                website.HomepageSnapshot = Convert.FromBase64String(model.HomepageSnapshot);
            }

            await context.SaveChangesAsync();

            return model;
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException();
            }

            var website = await context.Websites.FindAsync(id);

            if (website is null)
            {
                throw new NotFoundException("Website not found.");
            }

            context.Websites.Remove(website);
            await context.SaveChangesAsync();
        }

        #region Private Methods
        private async Task ValidateNameUniquenessAsync(string name)
        {
            if (await context.Websites.AnyAsync(w => w.Name.ToLower().Equals(name.ToLower())))
            {
                throw new NotUniqueException("Website name is not uniue. Please provide unique name.");
            }
        }

        private async Task<Guid> GetCategoryIdAsync(string categoryCode)
        {
            var categoryId = await categoryService.GetCategoryIdByCodeAsync(categoryCode);

            if (categoryId == Guid.Empty)
            {
                throw new NotFoundException($"Category with code '{categoryCode}' does not exists");
            }

            return categoryId;
        }

        private IQueryable<WebsiteModel> ApplySorting(IQueryable<WebsiteModel> query, SortDescriptor sortDescriptor)
        {
            Expression<Func<WebsiteModel, object>> sortExpression = null;

            sortExpression = sortDescriptor.Field switch
            {
                nameof(WebsiteModel.Name) => (w => w.Name),
                nameof(WebsiteModel.URL) => (w => w.URL),
                nameof(WebsiteModel.Category) => (w => w.Category),
                nameof(WebsiteModel.UserEmail) => (w => w.UserEmail),
                _ => throw new NotSupportedException($"Value '{sortDescriptor.Field}' is not supported for sorting."),
            };

            bool isAsc = sortDescriptor.Dir.ToLower().Equals(SortDirection.Ascending);
            return isAsc ? query.OrderBy(sortExpression) : query.OrderByDescending(sortExpression);
        }
        #endregion
    }
}