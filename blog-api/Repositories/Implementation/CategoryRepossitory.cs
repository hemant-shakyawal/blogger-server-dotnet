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

        public async Task<Category?> UpdateAsync(Category category)
        {
            var exitingCategory= await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);

            if(exitingCategory != null)
            {
                dbContext.Entry(exitingCategory).CurrentValues.SetValues(category);
                await dbContext.SaveChangesAsync();
                return category;
                    

            }
            return null;
        }
        public async Task<Category?> DeleteAsync(Guid id)
        {
            var exitingCategory = await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (exitingCategory is  null)
            {
                return null;
            }
            dbContext.Categories.Remove(exitingCategory);
           await dbContext.SaveChangesAsync();
            return exitingCategory;
        }
    }
}
