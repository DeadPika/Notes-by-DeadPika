﻿using MediatR;
using Notes.Domain.Models;
using Notes.Persistence.Interfaces;

namespace Notes.Persistence.Repositories.Notes.Commands.CreateNote
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
                CreationDate = DateTime.UtcNow,
                EditDate = null
            };
            await _dbContext.Notes.AddAsync(note, cancelletionToken);
            await _dbContext.SaveChangesAsync(cancelletionToken);
            return note.Id;
        }
    }
}
