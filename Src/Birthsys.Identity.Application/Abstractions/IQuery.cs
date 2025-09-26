using Birthsys.Identity.Domain.Abstractions;
using MediatR;

namespace Birthsys.Identity.Application.Abstractions
{
    public interface IQuery : IRequest<Result>
    {
    }
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}