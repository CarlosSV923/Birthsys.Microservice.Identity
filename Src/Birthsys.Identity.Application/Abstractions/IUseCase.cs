using Birthsys.Identity.Domain.Abstractions;
using MediatR;

namespace Birthsys.Identity.Application.Abstractions
{
    public interface IUseCase : IRequest<Result>, IUseCaseBase
    {
    }

    public interface IUseCase<TResponse> : IRequest<Result<TResponse>>, IUseCaseBase
    {
    }

    public interface IUseCaseBase
    {
        
    }
}