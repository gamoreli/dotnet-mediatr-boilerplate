using System.Collections.Generic;

namespace MediatorBoilerplate.Domain.Core.Base.Responses
{
    public record QueryResponsePaginated<TResponse>
        (IEnumerable<TResponse> Payload, int Page, int Total) : QueryResponse<IEnumerable<TResponse>>(Payload);
}