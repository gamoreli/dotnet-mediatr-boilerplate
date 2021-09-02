using System;
using System.Net;
using System.Threading.Tasks;
using MediatorBoilerplate.Domain.Core.Base.Responses;
using MediatorBoilerplate.Domain.Features.User.Queries.GetAllUsers;
using MediatorBoilerplate.Domain.Features.User.Queries.GetUser;
using MediatorBoilerplate.Domain.Features.User.Queries.Shared;
using MediatorBoilerplate.Infra.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediatorBoilerplate.Api.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class UserController : BaseController
    {
        public UserController(IMediator mediator) : base(mediator) { }

        /// <summary>
        ///     Gets all users
        /// </summary>
        /// <returns>NotFound/Ok(QueryResponsePaginated(UserProjection))</returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(QueryResponsePaginated<UserProjection>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllUsers() => await Mediator.Send(new GetAllUsersQuery()).ToActionResult();
        
        /// <summary>
        ///     Gets an user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>NotFound/Ok(QueryResponse(UserProjection))</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(QueryResponse<UserProjection>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser([FromRoute] Guid id) => await Mediator.Send(new GetUserQuery(id)).ToActionResult();
    }
}