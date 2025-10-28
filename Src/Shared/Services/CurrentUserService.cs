using Anazon.Models;
using Anazon.Database;
using System.Security.Claims;

namespace Anazon.Shared.Services;

public class CurrentUserService(AppDbContext dbContext, IHttpContextAccessor contextAccessor)
{
    private ClaimsPrincipal Principal => contextAccessor.HttpContext!.User;
    private User? currentUser = null;

    public int? CurrentUserId
    {
        get
        {
            var userIdClaim = Principal.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out int userId))
                return null;

            return userId;
        }
    }

    public User? CurrentUser
    {
        get
        {
            currentUser ??= dbContext.Users.FirstOrDefault(u => u.Id == CurrentUserId);

            return currentUser;
        }
    }
}
