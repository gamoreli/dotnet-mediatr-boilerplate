namespace MediatorBoilerplate.Domain.Core.Base.Failures
{
    public record BadRequestFailure(string Message) : Failure(Message);
}