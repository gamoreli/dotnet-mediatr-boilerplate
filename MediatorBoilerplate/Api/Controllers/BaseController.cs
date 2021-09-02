using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediatorBoilerplate.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        public BaseController(IMediator mediator) => Mediator = mediator;

        protected IMediator Mediator { get; }
    }
}