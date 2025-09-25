using Birthsys.Identity.Domain.Abstractions;
using MediatR;

namespace Birthsys.Identity.Application.Abstractions
{
    public interface IUseCase : IRequest<Result>
    {
    }

    public interface IUseCase<TResponse> : IRequest<Result<TResponse>>
    {
    }
}