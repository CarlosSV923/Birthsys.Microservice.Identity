using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Birthsys.Identity.Application.Options;
using Birthsys.Identity.Domain.Abstractions;
using Birthsys.Identity.Domain.Aggregates.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Birthsys.Identity.Application.Services.Jwt
{
    public sealed class JwtProvider(
        IOptions<JwtOptions> jwtOptions
    ) : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;
        public Result<JwtGenerateTokenResult> GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id!.Value.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email!.Value),
                new(JwtRegisteredClaimNames.Name, $"{user.Name!.Value}.{user.LastName!.Value}"),
            };
            try
            {
                using var rsa = RSA.Create();
                rsa.ImportFromPem(_jwtOptions.PrivateKey.ToCharArray());
                var key = new RsaSecurityKey(rsa);
                var creds = new SigningCredentials(key, SecurityAlgorithms.RsaSha256);

                var expiration = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes);
                var token = new JwtSecurityToken(
                    issuer: _jwtOptions.Issuer,
                    audience: _jwtOptions.Audience,
                    claims: claims,
                    expires: expiration,
                    signingCredentials: creds
                );

                return new JwtGenerateTokenResult(
                    new JwtSecurityTokenHandler().WriteToken(token),
                    expiration
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Result.Failure<JwtGenerateTokenResult>(Error.Build(500, "TokenGenerationFailed", $"Failed to generate auth token"));
            }
        }
    }
}