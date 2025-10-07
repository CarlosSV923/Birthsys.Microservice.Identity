using Birthsys.Identity.Application.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Birthsys.Identity.Infrastructure.Accessors
{
    public sealed class TokenAccessor(IHttpContextAccessor httpContextAccessor) : ITokenAccessor
    {
        public string GetToken()
        {
            var authHeader = httpContextAccessor.HttpContext?
            .Request
            .Headers
            .Authorization.FirstOrDefault();

            return authHeader?.Replace("Bearer ", string.Empty) ?? string.Empty;
        }
    }
}