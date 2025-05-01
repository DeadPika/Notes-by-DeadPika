using FluentValidation;

namespace Notes.Persistence.Repositories.Notes.Commands.DeleteNote
{
    public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
    {
        public DeleteNoteCommandValidator()
        {
            RuleFor(deleteNoteCommand => deleteNoteCommand.Id).NotEqual(Guid.Empty).WithMessage("Id заметки не может быть пустым!");
            RuleFor(deleteNoteCommand => deleteNoteCommand.UserId).NotEqual(Guid.Empty).WithMessage("Id пользователя не может быть пустым!");
        }
    }
}
