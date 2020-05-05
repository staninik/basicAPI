using BasicAPI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BasicAPI.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            RegisterServices(services);
        }

        #region Private Methods
        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>()
                    .AddScoped<IWebsiteService, WebsiteService>()
                    .AddScoped<IUserService, UserService>();

            services.AddSingleton<IPasswordService, PasswordService>();
        }
        #endregion
    }
}