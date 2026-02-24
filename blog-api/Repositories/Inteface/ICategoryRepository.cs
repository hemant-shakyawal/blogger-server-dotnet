using blog_api.Models.Domain;

namespace blog_api.Repositories.Inteface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);

        Task<IEnumerable<Category>> GetAllAsync();

        Task<Category?> GetById(Guid id);
        
        Task<Category?>UpdateAsync(Category category);
    }
}
