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
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDto request)// call the CreateCategoryRequestDto  from DTO
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
        //Get:https://localhost:7202/api/Categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await categoryRepository.GetAllAsync();

            //Map Domain model to DTO

            var response = new List<CategoryDto>();

            foreach (var category in categories)
            {
                response.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle,

                });  
            }
            return Ok(response);
        }

        //Get:https://localhost:7202/api/Categories/{Id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var existingCategory = await categoryRepository.GetById(id);
            if(existingCategory is null)
            {
                return NotFound();
            }
            var response =new CategoryDto { 
                Id = existingCategory.Id,
            Name = existingCategory.Name,
            UrlHandle=existingCategory.UrlHandle
            };
            return Ok(response);
        }
    }


}
