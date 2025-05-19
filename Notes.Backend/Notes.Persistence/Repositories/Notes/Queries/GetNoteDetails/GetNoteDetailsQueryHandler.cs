using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Domain.Models;
using Notes.Persistence.Interfaces;

namespace Notes.Persistence.Repositories.Notes.Queries.GetNoteDetails
{
    public class GetNoteDetailsQueryHandler
        : IRequestHandler<GetNoteDetailsQuery, NoteDetailsVm>
    {
        private readonly INotesDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetNoteDetailsQueryHandler(INotesDbContext context, IMapper mapper) => (_dbContext, _mapper) = (context, mapper);
        public async Task<NoteDetailsVm> Handle(GetNoteDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Notes.
                FirstOrDefaultAsync(note =>
                note.Id == request.Id && 
                note.UserId == request.UserId, cancellationToken);

            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            return _mapper.Map<NoteDetailsVm>(entity);
        }
    }
}
