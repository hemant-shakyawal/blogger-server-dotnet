using blog_api.Data;
using blog_api.Models.Domain;
using blog_api.Repositories.Inteface;

namespace blog_api.Repositories.Implementation
{
    public class CategoryRepossitory : ICategoryRepository
    {
        private readonly ApplicationDBContext dbContext;

        public CategoryRepossitory(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public  async  Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            return category;
        }
    }
}
