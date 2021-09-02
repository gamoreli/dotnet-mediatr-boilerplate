using System.Threading.Tasks;
using LanguageExt;
using MediatorBoilerplate.Domain.Core.Base.Failures;
using Microsoft.AspNetCore.Mvc;

namespace MediatorBoilerplate.Infra.Extensions
{
    public static class EitherToActionResultExtensions
    {
        public static Task<IActionResult>
            ToActionResult<TFailure, TResult>(this Task<Either<TFailure, TResult>> either) => either.Map(Match);

        private static IActionResult Match<TFailure, TResult>(this Either<TFailure, TResult> either) =>
            either.Match<IActionResult>(
                Left: failure =>
                {
                    if (failure is NotFoundFailure)
                        return new NotFoundResult();

                    return new BadRequestObjectResult(failure);
                },
                Right: result => new OkObjectResult(result));
    }
}