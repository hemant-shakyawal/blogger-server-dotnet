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
       public BlogPostController(IBlogPostRepository blogPostRepository)
        {
          this.blogPostRepository=blogPostRepository;
        }

       

        //Post:https://localhost:7202/api/blogpost

        [HttpPost]
        public async Task<IActionResult>CreateBlogPost([FromBody] CreateBlogPostRequestDto request)
        {

            // convert DTo to domain
            var blogPost = new BlogPost {
                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublishDate = request.PublishDate,
                ShortDescription = request.ShortDescription,
                Title = request.Title,
                UrlHandle = request.UrlHandle,


            };

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


                });
            }
            return Ok(response);

        }

    }
}
