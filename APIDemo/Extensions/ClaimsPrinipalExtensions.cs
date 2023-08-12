using System.Security.Claims;

namespace APIDemo.Extensions
{
    public static class ClaimsPrinipalExtensions
    {
        public static string RetrieveEmailFromPrincipal(this ClaimsPrincipal user)
            => user.FindFirstValue(ClaimTypes.Email);
    }
}
