using BasicAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicAPI.Services
{
    public interface IWebsiteService
    {
        Task<WebsiteModel> GetAsync(Guid id);

        Task<IEnumerable<WebsiteModel>> GetAsync(QueryDescriptor filter);

        Task<Guid> AddAsync(WebsiteAddModel model);

        Task<WebsiteUpdateModel> UpdateAsync(Guid id, WebsiteUpdateModel model);

        public Task DeleteAsync(Guid id);
    }
}