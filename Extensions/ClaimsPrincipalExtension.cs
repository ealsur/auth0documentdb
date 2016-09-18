using System.Linq;
using System.Security.Claims;

namespace auth0documentdb.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        //Obtains the Auth0 User id
        public static string CurrentId(this ClaimsPrincipal user)
        {
            return user.Claims.First(x=>x.Type == ClaimTypes.NameIdentifier).Value.Replace("auth0|",string.Empty);
        }
    }
}
