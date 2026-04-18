using blog_api.Models.Domain;

namespace blog_api.Repositories.Inteface
{
    public interface IImageRepository
    {
        Task<Blogimage> Upload(IFormFile file, Blogimage blogimage);

        Task<IEnumerable<Blogimage>> GetAllImage();
    }
}
