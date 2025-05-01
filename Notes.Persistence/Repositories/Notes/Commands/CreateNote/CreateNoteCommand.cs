using MediatR;

namespace Notes.Persistence.Repositories.Notes.Commands.CreateNote
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
    }
}
