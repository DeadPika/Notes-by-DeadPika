using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Notes.Persistence.Repositories.Notes.Queries.GetNoteDetails;
using Notes.Persistence.Repositories.Notes.Queries.GetNoteList;
using Notes.Persistence.Repositories.Notes.Commands.CreateNote;
using Notes.Persistence.Repositories.Notes.Commands.DeleteNote;
using Notes.Persistence.Repositories.Notes.Commands.UpdateNote;
using Notes.WebApi.Contracts;
using Microsoft.AspNetCore.Authorization;
using Notes.WebApi.Attributes;
using Notes.Domain.Enums;

namespace Notes.WebApi.Controllers
{
    [Authorize("AdminPolicy")]
    [Route("api/[controller]")]
    public class NoteController : BaseController
    {
        private readonly IMapper _mapper;
        public NoteController(IMapper mapper) => _mapper = mapper;

        [HttpGet]
        [PermissionAuthorize(Permission.Read)]
        public async Task<ActionResult<NoteListVm>> GetAll()
        {
            var query = new GetNoteListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpGet("{id}")]
        [PermissionAuthorize(Permission.Read)]
        public async Task<ActionResult<NoteDetailsVm>> Get(Guid id)
        {
            var query = new GetNoteDetailsQuery
            {
                UserId = UserId,
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        [PermissionAuthorize(Permission.Create)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDto createNoteDto)
        {
            var command = _mapper.Map<CreateUserCommand>(createNoteDto);
            command.UserId = UserId;
            var noteId = await Mediator.Send(command);
            return Ok(command);
        }

        [HttpPut]
        [PermissionAuthorize(Permission.Update)]
        public async Task<IActionResult> Update([FromBody] UpdateNoteDto updateNoteDto)
        {
            var command = _mapper.Map<UpdateNoteCommand>(updateNoteDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [PermissionAuthorize(Permission.Delete)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteNoteCommand
            {
                UserId = UserId,
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }

    }
}
