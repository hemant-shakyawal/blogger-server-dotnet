using blog_api.Data;
using blog_api.Models.Domain;
using blog_api.Repositories.Inteface;
using Microsoft.EntityFrameworkCore;

namespace blog_api.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDBContext dBContext;

        // constrctor 
        public BlogPostRepository(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public async Task<BlogPost> CreateAsync(BlogPost blogpost)
        {
            await dBContext.BlogPosts.AddAsync(blogpost);
            await dBContext.SaveChangesAsync();
            return blogpost;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await dBContext.BlogPosts.Include(x=>x.Categories).ToListAsync();
           
        }

        public async Task<BlogPost?> GetById(Guid id)
        {
            return await dBContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
