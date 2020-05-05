using BasicAPI.Constants;
using BasicAPI.Data;
using BasicAPI.Entities;
using BasicAPI.Exceptions;
using BasicAPI.Models;
using BasicAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BasicAPI.UnitTests
{
    [TestFixture]
    public class WebsiteService_Tests
    {
        private IWebsiteService websiteService;
        private Guid websiteToDeleteId;
        private DbContextOptions<DataContext> options;
        private const string WebsiteName = "Football";
        private readonly WebsiteAddModel websiteAddModel = new WebsiteAddModel
        {
            Name = WebsiteName,
            CategoryCode = CategoryCode.Sport,
            URL = "https://football.com",
            HomepageSnapshot = @"////////////////////////////////////////////////////////////////////////////gf////////////////////////f/z///4+fH///ngef//+eB5///74H3////wf/////D//////////////////n/H///+H4f///4fh////w8P////n5/////AP////////////////////////////////////////////////////////////////////8=",
            Login = new UserModel
            {
                Email = "test@test.bg",
                Password = "123456",
            }
        };

        [SetUp]
        public void SetUp()
        {
            options = new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;

            using var context = new DataContext(options);
            var mc = new MemoryCache(new MemoryCacheOptions());
            var categoryService = new CategoryService(context, mc);
            context.Categories.Add(new Category { Code = CategoryCode.Sport, Name = "Sport" });
            context.SaveChanges();
            websiteService = new WebsiteService(context, categoryService, new UserService(new PasswordService()));

            websiteToDeleteId = Task.Run(() => websiteService.AddAsync(websiteAddModel)).GetAwaiter().GetResult();
        }

        [TearDown]
        public void TearDown()
        {
            using var context = new DataContext(options);
            context.Database.EnsureDeleted();
            context.SaveChanges();
        }

        [Test]
        public async Task DeleteAsync_SetsTrue()
        {
            using var context = new DataContext(options);
            var mc = new MemoryCache(new MemoryCacheOptions());
            var categoryService = new CategoryService(context, mc);
            websiteService = new WebsiteService(context, categoryService, new UserService(new PasswordService()));
            await websiteService.DeleteAsync(websiteToDeleteId);
            bool isDeleted = (await context.Websites.IgnoreQueryFilters().Select(x => new { x.Id, x.IsDeleted }).FirstOrDefaultAsync(x => x.Id == websiteToDeleteId)).IsDeleted;

            Assert.IsTrue(isDeleted, "IsDeleted should be set to true.");
        }

        [Test]
        public void DeleteAsync_ThrowsNotFoundException()
        {
            using var context = new DataContext(options);
            var mc = new MemoryCache(new MemoryCacheOptions());
            var categoryService = new CategoryService(context, mc);
            websiteService = new WebsiteService(context, categoryService, new UserService(new PasswordService()));

            Assert.CatchAsync(typeof(NotFoundException), async () => await websiteService.DeleteAsync(Guid.Parse("4e7a04d2-b0a1-4dbd-9b4e-8b326c272565")), "Should return NotFoundException.");
        }

        [Test]
        public void DeleteAsync_ThrowsArgumentNullException()
        {
            using var context = new DataContext(options);
            var mc = new MemoryCache(new MemoryCacheOptions());
            var categoryService = new CategoryService(context, mc);
            websiteService = new WebsiteService(context, categoryService, new UserService(new PasswordService()));

            Assert.CatchAsync(typeof(ArgumentNullException), async () => await websiteService.DeleteAsync(Guid.Empty), "Should return ArgumentNullException.");
        }

        [Test]
        public void AddAsync_IsWebsiteNameUnique_ReturnsNotUniqueException()
        {
            using var context = new DataContext(options);
            var mc = new MemoryCache(new MemoryCacheOptions());
            var categoryService = new CategoryService(context, mc);
            websiteService = new WebsiteService(context, categoryService, new UserService(new PasswordService()));

            Assert.CatchAsync(typeof(NotUniqueException), async () => await websiteService.AddAsync(websiteAddModel), "Should return ArgumentNullException.");
        }

        [Test]
        public async Task AddAsync_CreatesWebsite()
        {
            using var context = new DataContext(options);
            var mc = new MemoryCache(new MemoryCacheOptions());
            var categoryService = new CategoryService(context, mc);
            websiteService = new WebsiteService(context, categoryService, new UserService(new PasswordService()));

            websiteAddModel.Name = "A dd Async";
            Guid id = await websiteService.AddAsync(websiteAddModel);
            Assert.AreNotEqual(id, Guid.Empty, "Add async should return a GUID.");
        }
    }
}