using blog_api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace blog_api.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Blogimage> Blogimages { get; set; }



    }
}
