using BasicAPI.Constants;
using BasicAPI.Data;
using BasicAPI.Entities;
using BasicAPI.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace BasicAPI.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static void UpdateDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<DataContext>();
            context.Database.Migrate();
        }

        public static void SeedDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices
                            .GetRequiredService<IServiceScopeFactory>()
                            .CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<DataContext>();

            if (!context.Categories.Any())
            {
                List<Category> categories = GetCategories();
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }
        }

        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }

        #region Private Methods
        private static List<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category { Code = CategoryCode.Technology, Name = "Technology" },
                new Category { Code = CategoryCode.Sport, Name = "Sport" },
                new Category { Code = CategoryCode.Education, Name = "Education" },
                new Category { Code = CategoryCode.News, Name = "News" },
                new Category { Code = CategoryCode.Entertainment, Name = "Entertainment" },
            };
        }
        #endregion
    }
}