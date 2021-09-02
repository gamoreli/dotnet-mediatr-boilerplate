namespace MediatorBoilerplate.Domain.Core.Base.Responses
{
    public record QueryResponse<TResponse>(TResponse Payload);
}