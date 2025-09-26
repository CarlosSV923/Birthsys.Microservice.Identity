using System.Security.Cryptography;
using Birthsys.Identity.Application.Options;
using Birthsys.Identity.Domain.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Birthsys.Identity.Api.ConfigOptions
{
    public class JwtConfigOptions(
        IOptions<JwtOptions> jwtOptions
    ) : IConfigureNamedOptions<JwtBearerOptions>
    {
        public void Configure(JwtBearerOptions options)
        {
            var config = jwtOptions.Value;
            using var rsa = RSA.Create();
            rsa.ImportFromPem(config.PublicKey.ToCharArray());

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config.Issuer,
                ValidAudience = config.Audience,
                IssuerSigningKey = new RsaSecurityKey(rsa),
                ClockSkew = TimeSpan.FromSeconds(30)
            };

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        context.Response.Headers.Append("Token-Expired", "true");
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";
                        context.Response.WriteAsync(Error.Build(401, "Unauthorized", "Token has expired").ToJsonString());
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";
                        context.Response.WriteAsync(Error.Build(401, "Unauthorized", context.Exception.Message).ToJsonString());
                    }
                    return Task.CompletedTask;
                }
            };
        }

        public void Configure(string? name, JwtBearerOptions options)
        {
            Configure(options);
        }
    }
}