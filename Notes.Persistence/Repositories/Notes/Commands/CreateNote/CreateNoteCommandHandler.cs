using MediatR;
using Notes.Application.Interfaces;
using Notes.Domain.Models;

namespace Notes.Persistence.Repositories.Notes.Commands.CreateNote
{
    public class CreateNoteCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly INotesDbContext _dbContext;
        public CreateNoteCommandHandler(INotesDbContext context) => _dbContext = context;
        public async Task<Guid> Handle(CreateUserCommand request,
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
