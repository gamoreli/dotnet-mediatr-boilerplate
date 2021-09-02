using MediatR;

namespace MediatorBoilerplate.Domain.Core.Pipeline.Validation
{
    public interface IMessageValidationBehavior<out TResponse> : IRequest<TResponse>
    {
    }
}