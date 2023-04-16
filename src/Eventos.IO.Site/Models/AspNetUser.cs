using System.Security.Claims;
using Eventos.IO.Domain.INTERFACES;

namespace Eventos.IO.Site.Models;

public class AspNetUser : IUser
{
    private readonly IHttpContextAccessor _accessor;

    public AspNetUser(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public string Name => _accessor.HttpContext.User.Identity.Name;

    public IEnumerable<Claim> GetClaimsIdentity()
    {
        return _accessor.HttpContext.User.Claims;
    }

    public Guid GetUserId()
    {
        return IsAuthenticated() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.NewGuid();
    }

    public bool IsAuthenticated()
    {
        return _accessor.HttpContext.User.Identity.IsAuthenticated;
    }
}