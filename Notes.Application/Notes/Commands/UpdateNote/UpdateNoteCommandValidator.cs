using FluentValidation;

namespace Notes.Application.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
    {
        public UpdateNoteCommandValidator()
        {
            RuleFor(updateNoteCommand => updateNoteCommand.UserId).NotEqual(Guid.Empty).WithMessage("Id пользователя не может быть пустым!");
            RuleFor(updateNoteCommand => updateNoteCommand.Id).NotEqual(Guid.Empty).WithMessage("Id заметки не может быть пустым!");
            RuleFor(createNoteCommand => createNoteCommand.Title)
                .NotEmpty().MaximumLength(250).WithMessage("Название не может быть пустым или больше 250 символов!");
        }
    }
}
