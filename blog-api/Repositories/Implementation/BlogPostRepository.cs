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

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var exitingBlogPost = await dBContext.BlogPosts.FirstOrDefaultAsync(c => c.Id == id);
            if (exitingBlogPost != null) {
                dBContext.BlogPosts.Remove(exitingBlogPost);
                await dBContext.SaveChangesAsync();
                return exitingBlogPost;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await dBContext.BlogPosts.Include(x=>x.Categories).ToListAsync();
           
        }

        public async Task<BlogPost?> GetById(Guid id)
        {
            return await dBContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var exitingBlogPost=await dBContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (exitingBlogPost == null)
            {
                return null;
            }
            // Update blogpost

            dBContext.Entry(exitingBlogPost).CurrentValues.SetValues(blogPost);

            // Update categoreis

            exitingBlogPost.Categories=blogPost.Categories;

            await dBContext.SaveChangesAsync();

            return blogPost;
        }
    }
}
