using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CRMApi.Middleware;

public class UserMiddleware
{
    private readonly RequestDelegate _next;

    public UserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Cookies.TryGetValue("upcare-health-token", out var token))
        {
            Console.WriteLine($"Token: {token}");

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SHH")!)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            var result = await tokenHandler.ValidateTokenAsync(token, validationParams);
            if (result.IsValid)
            {
                var principal = new ClaimsPrincipal(result.ClaimsIdentity);

                context.User = principal;
            }

        }

        await _next(context);
    }
}
