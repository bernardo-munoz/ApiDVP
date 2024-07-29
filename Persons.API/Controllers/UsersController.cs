using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Response;
using Service.EventHandler.Commands;
using Service.Queries.DTO;
using Service.Queries.Repositoty.Interface.IPersons;
using Service.Queries.Repositoty.Interface.IUsers;

namespace Persons.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IReadUsers _readUsersService;
        private readonly IMediator _mediator;

        public UsersController(IReadUsers readUsersService, IMediator mediator)
        {
            _readUsersService = readUsersService;
            _mediator = mediator;
        }

        [HttpGet]
        [Route(nameof(UsersController.GetAllPersons))]
        public RequestResult GetAllPersons()
        {
            return _readUsersService.GetAllUsers();
        }

        [HttpGet]
        [Route(nameof(UsersController.GetPersonByNumberDocument))]
        public RequestResult GetPersonByNumberDocument(string user)
        {
            return _readUsersService.GetUsersByUser(user);
        }

        [HttpPost]
        [Route(nameof(UsersController.CreateUsers))]
        public async Task<RequestResult> CreateUsers([FromBody] UsersCreateCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost]
        [Route(nameof(UsersController.Login))]
        public RequestResult Login(LoginDTO LoginDTO)
        {
            return _readUsersService.Login(LoginDTO);
        }
    }
}
