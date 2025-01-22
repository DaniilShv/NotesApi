using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Buffers.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;

namespace NotesApi.Services.Jwt;

public static class JwtBuilder
{
    public static string GenerateJwtToken(string nickname, string id, IConfiguration config)
    {
        List<Claim> claims = new()
        {
            new Claim("nickname", nickname),
            new Claim("id", id)
        };
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(360),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("SecurityKey").Value)),
                SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public static void AppendJwtCookie(string token, HttpResponse response)
    {
        var cookieOptions = new CookieOptions();
        cookieOptions.Expires = DateTime.UtcNow.AddMinutes(360);
        response.Cookies.Append("AspCoreCookie", token, cookieOptions);
    }
}
