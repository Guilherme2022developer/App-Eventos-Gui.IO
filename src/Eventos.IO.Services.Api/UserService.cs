using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Eventos.IO.Application.VIEWMODELS;
using Microsoft.IdentityModel.Tokens;

namespace Eventos.IO.Services.Api;

public static class UserService
{
    public static string GenereteToken (RegistrerUserViewmodel user)
    {
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Settings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Email.ToString()),
                new Claim(ClaimTypes.Role, user.role.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);

    }
}