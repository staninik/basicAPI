using BasicAPI.Entities;

namespace BasicAPI.Models
{
    public class CategoryModel
    {
        public CategoryModel()
        {

        }

        public CategoryModel(Category category)
        {
            Code = category.Code;
            Name = category.Name;
        }

        public string Code { get; set; }

        public string Name { get; set; }
    }
}