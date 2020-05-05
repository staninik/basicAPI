using BasicAPI.Entities;
using System;

namespace BasicAPI.Models
{
    public class WebsiteModel
    {
        public WebsiteModel()
        {

        }

        public WebsiteModel(Website website)
        {
            Id = website.Id;
            Name = website.Name;
            URL = website.URL;
            HomepageSnapshot = website.HomepageSnapshot;
            UserId = website.UserId;
            UserEmail = website.Login.Email;
            CategoryCode = website.Category.Code;
            Category = website.Category.Name;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string URL { get; set; }

        public byte[] HomepageSnapshot { get; set; }

        public Guid UserId { get; set; }

        public string UserEmail { get; set; }

        public string CategoryCode { get; set; }

        public string Category { get; set; }
    }
}