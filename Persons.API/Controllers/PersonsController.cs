using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Response;
using Service.EventHandler.Commands;
using Service.Queries.Repositoty.Interface.IPersons;

namespace Persons.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IReadPersons _readPersonsService;
        private readonly IMediator _mediator;

        public PersonsController(IReadPersons readPersonsService, IMediator mediator)
        {
            _readPersonsService = readPersonsService;
            _mediator = mediator;
        }

        [HttpGet]
        [Route(nameof(PersonsController.GetAllPersons))]
        public RequestResult GetAllPersons()
        {
            return _readPersonsService.GetAllPersons();
        }

        [HttpGet]
        [Route(nameof(PersonsController.GetPersonByNumberDocument))]
        public RequestResult GetPersonByNumberDocument(int numberDocument)
        {
            return _readPersonsService.GetPersonByNumberDocument(numberDocument);
        }

        [HttpPost]
        [Route(nameof(PersonsController.CreatePersons))]
        public async Task<RequestResult> CreatePersons([FromBody] PersonCreateCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
