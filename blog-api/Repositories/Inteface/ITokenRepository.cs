using Microsoft.AspNetCore.Identity;

namespace blog_api.Repositories.Inteface
{
    public interface ITokenRepository
    {

        string CreateJwtToken(IdentityUser user, List<string> roles);

    }
}
