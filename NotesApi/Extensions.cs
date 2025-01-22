using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace NotesApi
{
    public static class Extensions
    {
        public static AuthenticationBuilder AddJwt(this AuthenticationBuilder builder, IConfiguration config)
        {
            builder.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => 
            {
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["AspCoreCookie"];
                        return Task.CompletedTask;
                    }
                };
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(config.GetSection("SecurityKey").Value))
                };
            }
            );
            return builder;
        }
    }
}
