using blog_api.Data;
using blog_api.Models.Domain;
using blog_api.Models.DTO;
using blog_api.Repositories.Inteface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace blog_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)// constrtor  call the repository
        {
            this.categoryRepository = categoryRepository;
        }
        //
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)// call the CreateCategoryRequestDto  from DTO
        {
            // Map DataTransferObject (DTO) to Domain Model

            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };

          await categoryRepository.CreateAsync(category);

            // Domain Model to DTO
            var response = new CategoryDto
            { 
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,

            };


            return Ok(response);

        }
    }
}
