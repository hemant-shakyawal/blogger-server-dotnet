using blog_api.Models.Domain;

namespace blog_api.Repositories.Inteface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogpost);
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetById(Guid id);
    }
}
