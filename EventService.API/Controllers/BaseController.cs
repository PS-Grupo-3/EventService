using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace EventService.API.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected string? CurrentUserId =>
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        protected string? CurrentUserRole =>
            User.FindFirst(ClaimTypes.Role)?.Value;

        protected bool IsAdmin =>
            string.Equals(CurrentUserRole, "Admin", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(CurrentUserRole, "SuperAdmin", StringComparison.OrdinalIgnoreCase);

        protected bool IsSuperAdmin =>
            string.Equals(CurrentUserRole, "SuperAdmin", StringComparison.OrdinalIgnoreCase);

        protected bool IsCurrent =>
            string.Equals(CurrentUserRole, "Current", StringComparison.OrdinalIgnoreCase);
    }
}