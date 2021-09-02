using System.Threading.Tasks;
using MediatorBoilerplate.Domain.Features.User.Commands.CreateUser;
using MediatorBoilerplate.Infra.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediatorBoilerplate.Api.Controllers.User
{
    public partial class UserController
    {
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser()
        {
            return await Mediator.Send(new CreateUserMessage("gamoreli@hotmail.com", "Gabriel Augusto Moreli"))
                .ToActionResult();
        }
    }
}