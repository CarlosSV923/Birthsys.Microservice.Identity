using Birthsys.Identity.Domain.Abstractions;
using MediatR;

namespace Birthsys.Identity.Application.Abstractions
{
    public interface ICommand : IRequest<Result>
    {
    }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    {
    }
}