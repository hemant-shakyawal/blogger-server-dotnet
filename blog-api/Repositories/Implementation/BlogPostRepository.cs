using blog_api.Data;
using blog_api.Models.Domain;
using blog_api.Repositories.Inteface;

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
    }
}
