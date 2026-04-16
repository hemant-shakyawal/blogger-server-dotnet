using blog_api.Models.Domain;
using blog_api.Models.DTO;
using blog_api.Repositories.Inteface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace blog_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICategoryRepository categoryRepository;
        public BlogPostController(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;

        }

        public ICategoryRepository CategoryRepository { get; }



        //Post:https://localhost:7202/api/blogpost

        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto request)
        {

            // convert DTo to domain
            var blogPost = new BlogPost
            {
                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublishDate = request.PublishDate,
                ShortDescription = request.ShortDescription,
                Title = request.Title,
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>()


            };

            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await categoryRepository.GetById(categoryGuid);

                if (existingCategory is not null)
                {
                    blogPost.Categories.Add(existingCategory);

                }
            }

            blogPost = await blogPostRepository.CreateAsync(blogPost);
            //convert domain to DTO 
            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                PublishDate = blogPost.PublishDate,
                ShortDescription = blogPost.ShortDescription,
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle

                }).ToList()
            };

            return Ok(response);


        }

        //Get:https://localhost:7202/api/blogposts
        [HttpGet]

        public async Task<IActionResult> GetAllBlogPosts()
        {
            var blogPosts = await blogPostRepository.GetAllAsync();

            //convert domain to DTO 
            var response = new List<BlogPostDto>();
            foreach (var blogPost in blogPosts)
            {
                response.Add(new BlogPostDto
                {
                    Id = blogPost.Id,
                    Author = blogPost.Author,
                    Content = blogPost.Content,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    IsVisible = blogPost.IsVisible,
                    PublishDate = blogPost.PublishDate,
                    ShortDescription = blogPost.ShortDescription,
                    Title = blogPost.Title,
                    UrlHandle = blogPost.UrlHandle,
                    Categories = blogPost.Categories.Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle

                    }).ToList()


                });
            }
            return Ok(response);

        }

        //Get:https://localhost:7202/api/blogpost/{Id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
        {
            var blogPost = await blogPostRepository.GetById(id);
            if (blogPost is null)
            {
                return NotFound();
            }
            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                PublishDate = blogPost.PublishDate,
                ShortDescription = blogPost.ShortDescription,
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle

                }).ToList()

            };
            return Ok(response);

        }

        //Put:https://localhost:7202/api/blogpost/{Id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpddateBlogPost([FromRoute] Guid id, UpdateBlogPostRequestDto request)
        {

            // convert DTo to domain
            var blogPost = new BlogPost
            {
               Id=id,
                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublishDate = request.PublishDate,
                ShortDescription = request.ShortDescription,
                Title = request.Title,
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>()


            };

            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await categoryRepository.GetById(categoryGuid);

                if (existingCategory is not null)
                {
                    blogPost.Categories.Add(existingCategory);

                }
            }

            // call repository to update Blogpost Domain Model


            var UpdatedBlogPost=await blogPostRepository.UpdateAsync(blogPost);

            if (UpdatedBlogPost == null)
            {
                return NotFound();
            }


            // Convert Domain model to back to DTO
            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                PublishDate = blogPost.PublishDate,
                ShortDescription = blogPost.ShortDescription,
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle

                }).ToList()

            };

            return Ok(response);

        }

        //Delete :https://localhost:7202/api/blogpost/{Id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteBlogPost([FromRoute] Guid id)
        {
            var deleteBlogPost =await blogPostRepository.DeleteAsync(id);
            if (deleteBlogPost == null) { return NotFound(); }

            // convert Domain model to DTO

            var response = new BlogPostDto
            {
                Id = deleteBlogPost.Id,
                Author = deleteBlogPost.Author,
                Content = deleteBlogPost.Content,
                FeaturedImageUrl = deleteBlogPost.FeaturedImageUrl,
                IsVisible = deleteBlogPost.IsVisible,
                PublishDate = deleteBlogPost.PublishDate,
                ShortDescription = deleteBlogPost.ShortDescription,
                Title = deleteBlogPost.Title,
                UrlHandle = deleteBlogPost.UrlHandle,

            };
            return Ok(response);


        }
    }


}
