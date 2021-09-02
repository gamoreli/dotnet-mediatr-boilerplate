using MediatR;

namespace MediatorBoilerplate.Domain.Core.Pipeline.Logging
{
    public interface ILoggingBehavior<out TResponse> : IRequest<TResponse>
    { }
}