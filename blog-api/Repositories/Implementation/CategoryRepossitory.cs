using blog_api.Data;
using blog_api.Models.Domain;
using blog_api.Repositories.Inteface;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
           return await dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetById(Guid id)
        {
           return await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
