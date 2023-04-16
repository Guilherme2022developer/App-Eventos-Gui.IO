using System.Collections.Generic;
using System.Security.Claims;
using System;

namespace Eventos.IO.Domain.INTERFACES;

public interface IUser
{
    string Name { get; }
    Guid GetUserId();
    bool IsAuthenticated();
    IEnumerable<Claim> GetClaimsIdentity();
}