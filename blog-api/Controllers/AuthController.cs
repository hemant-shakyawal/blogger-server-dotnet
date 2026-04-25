using System.Security.Claims;
using blog_api.Models.DTO;
using blog_api.Repositories.Inteface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace blog_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository )
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        //Post:{apibaseurl}/api/auth/login
        [HttpPost]
        [Route("login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            //check mail
            var identityUser = await userManager.FindByEmailAsync(request.Email);

            if (identityUser is not null) {

                var checkPasswordResult = await userManager.CheckPasswordAsync(identityUser, request.Password);
                if (checkPasswordResult) {

                    var roles=await userManager.GetRolesAsync(identityUser);

                    //Create a token and response

                   var jwtToken= tokenRepository.CreateJwtToken(identityUser, roles.ToList());

                    var response = new LoginResponseDto()
                    {

                        Email = request.Email,
                        Roles = roles.ToList(),
                       
                    };

                    Response.Cookies.Append("access_token", jwtToken, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Lax,
                        Expires = DateTime.UtcNow.AddMinutes(15)

                    });

                    return Ok(response);
                
                }
                
            


            }
            ModelState.AddModelError("", "Email or Password incorrect");
            return ValidationProblem(ModelState);

        }

        //Post:{apibaseurl}/api/auth/register
        [HttpPost]
        [Route("register")]

        public async Task<IActionResult> Register([FromBody] RegistorRequestDto request)
        {
            // create the identity User Object
            var user = new IdentityUser
            {

                UserName = request.Email?.Trim(),
                Email = request.Email?.Trim(),

            };


            //create user
            var identityResult = await userManager.CreateAsync(user, request.Password);
            if (identityResult.Succeeded)
            {

                //Add Role to user (Reader)

                identityResult = await userManager.AddToRoleAsync(user, "Reader");

                if (identityResult.Succeeded)
                {
                    return Ok();
                }
                else
                {

                    if (identityResult.Errors.Any())
                    {

                        foreach (var error in identityResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);

                        }

                    }

                }

            }
            else
            {

                if (identityResult.Errors.Any())
                {

                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);

                    }

                }

            }



            return ValidationProblem(ModelState);
            
        }




        [Authorize]
        [HttpGet]
        [Route("me")]
        //Get:{apibaseurl}/api/auth/me
        public IActionResult UserDetails()
        {

            if(User.Identity==null || !User.Identity.IsAuthenticated)
            {
                return Unauthorized();


            }

            var response = new LoginResponseDto
            {

                Email = User.FindFirst(ClaimTypes.Email)?.Value,
                Roles = User.FindAll(ClaimTypes.Role).Select(x => x.Value).ToList()

            };
            return Ok(response);
            
        }



        [HttpPost]
        [Route("logout")]
        public IActionResult Logout()
        {

            // override the previous cookies
            Response.Cookies.Append("access_token", "", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddDays(-1)

            });
            return Ok();

        }

    }



    }

