using blog_api.Data;
using blog_api.Models.Domain;
using blog_api.Repositories.Inteface;
using Microsoft.EntityFrameworkCore;

namespace blog_api.Repositories.Implementation
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ApplicationDBContext applicationDBContext;

        public ImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor,ApplicationDBContext applicationDBContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.applicationDBContext = applicationDBContext;
        }

        public async Task<IEnumerable<Blogimage>> GetAllImage()
        {
            return await applicationDBContext.Blogimages.ToListAsync();
        }

        public async Task<Blogimage> Upload(IFormFile file, Blogimage blogimage)
        {
            //1-Upload  the Image to Api/Images
            var loaclPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{blogimage.FileName}{blogimage.FileExtention}");

            using var stream = new FileStream(loaclPath, FileMode.Create);

            await file.CopyToAsync(stream);



            //2-Upload the database 

            // https://bloger.com/images/somefilename.jpg

            var httpRequest = httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}/images/{blogimage.FileName}{blogimage.FileExtention}";
            blogimage.Url = urlPath;
            await applicationDBContext.Blogimages.AddAsync(blogimage);
            await applicationDBContext.SaveChangesAsync();
            return blogimage;


        }
    }
}
