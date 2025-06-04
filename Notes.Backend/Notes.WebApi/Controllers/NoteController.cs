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
using Asp.Versioning;

namespace Notes.WebApi.Controllers
{
    //[ApiVersion("1.0")]
    //[ApiVersion("2.0")]
    [ApiVersionNeutral]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class NoteController : BaseController
    {
        private readonly IMapper _mapper;
        public NoteController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Get the list of notes.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /note
        /// </remarks>
        /// <returns>Returns NoteListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [PermissionAuthorize(Permission.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<NoteListVm>> GetAll()
        {
            var query = new GetNoteListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Get the note by id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /note/2366E859-3DCB-45B3-8EB0-4348B133A77E
        /// </remarks>
        /// <params name="id">Note id (guid)</params>
        /// <returns>Returns NoteDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("{id}")]
        [PermissionAuthorize(Permission.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        /// <summary>
        /// Creates the note.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /note
        /// {
        ///     title: "note title",
        ///     details: "note details"
        /// }
        /// </remarks>
        /// <param name="createNoteDto">CreateNoteDto object</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [PermissionAuthorize(Permission.Create)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDto createNoteDto)
        {
            try
            {
                var command = _mapper.Map<CreateNoteCommand>(createNoteDto);
                command.UserId = UserId;
                var noteId = await Mediator.Send(command);
                return Ok(noteId); // Исправлено: возвращаем noteId
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating note: {ex.Message} - Inner: {ex.InnerException?.Message}");
                return StatusCode(500, $"An error occurred: {ex.Message} - Inner: {ex.InnerException?.Message}");
            }
        }

        /// <summary>
        /// Update the note.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /note
        /// {
        ///     title: "update note title",
        /// }
        /// </remarks>
        /// <param name="updateNoteDto">UpdateNoteDto object</param>
        /// <returns>Returns NonContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [PermissionAuthorize(Permission.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromBody] UpdateNoteDto updateNoteDto)
        {
            var command = _mapper.Map<UpdateNoteCommand>(updateNoteDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Delete the note by id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /note/833BB096-0CA4-43B0-8C84-B914AD1F0BEE
        /// </remarks>
        /// <param name="id">Id of the note (guid)</param>
        /// <returns>Returns NonContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpDelete("{id}")]
        [PermissionAuthorize(Permission.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
