using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using Notes.Domain;
using Notes.Application.Interfaces;

namespace Notes.Application.Notes.Commands.CreateNote
{
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
    {
        private readonly INotesDbContext _dbContext;
        public CreateNoteCommandHandler(INotesDbContext context) => _dbContext = context;
        public async Task<Guid> Handle(CreateNoteCommand request, 
            CancellationToken cancelletionToken)
        {
            var note = new Note
            {
                UserId = request.UserId,
                Title = request.Title,
                Details = request.Details,
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                EditDate = null
            };
            await _dbContext.Notes.AddAsync(note, cancelletionToken);
            await _dbContext.SaveChangesAsync(cancelletionToken);
            return note.Id;
        }
    }
}
