using blog_api.Models.Domain;
using blog_api.Models.DTO;
using blog_api.Repositories.Inteface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace blog_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        //Get:  https://localhost:7202/api/images
        [HttpGet]
        public async Task<IActionResult> GetAllImage() { 
        // Call image repository to get all image

            var images=await imageRepository.GetAllImage();

            //convert domain model to DTO
            var response = new List<BlogImageDto>();
            foreach (var image in images) {

                response.Add(new BlogImageDto
                {

                    Id = image.Id,
                    Title = image.Title,
                    FileExtention = image.FileExtention,
                    FileName = image.FileName,
                    Url = image.Url,
                    DateCreated = image.DateCreated

                });
            }
            return Ok(response);


        }

        // POST: https://localhost:7202/api/images
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] BlogImageUploadRequestDto request)
        {
            // 1. Validate the file using the property from the 'request' DTO
            ValidateFileUpload(request.File);

            if (ModelState.IsValid)
            {
                // 2. Map DTO to Domain Model
                var blogImage = new Blogimage // Ensure naming matches your Domain model (BlogImage vs Blogimage)
                {
                    FileExtention = Path.GetExtension(request.File.FileName).ToLower(),
                    FileName = request.FileName,
                    Title = request.Title,
                    DateCreated = DateTime.Now,
                };

                // 3. Use repository to upload file and save record
                // Passing the actual file and the domain object
                blogImage = await imageRepository.Upload(request.File, blogImage);

                // 4. Convert Domain Model back to DTO for the response
                var response = new BlogImageDto
                {
                    Id = blogImage.Id,
                    Title = blogImage.Title,
                    FileExtention = blogImage.FileExtention,
                    FileName = blogImage.FileName,
                    Url = blogImage.Url,
                    DateCreated = blogImage.DateCreated
                };

                return Ok(response);
            }

            return BadRequest(ModelState);
        }

        // Helper method for validation
        private void ValidateFileUpload(IFormFile file)
        {
            if (file == null)
            {
                ModelState.AddModelError("file", "Please select a file to upload.");
                return;
            }

            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".gif", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Unsupported file format");
            }

            if (file.Length > 10485760) // 10 MB
            {
                ModelState.AddModelError("file", "File size cannot be more than 10 MB");
            }
        }
    }
}