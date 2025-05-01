using FluentValidation;

namespace Notes.Persistence.Repositories.Notes.Commands.CreateNote
{
    public class CreateNoteCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateNoteCommandValidator()
        {
            RuleFor(createNoteCommand => createNoteCommand.Title)
                .NotEmpty().MaximumLength(250).WithMessage("Название не может быть пустым или больше 250 символов!");
            RuleFor(createNoteCommand => createNoteCommand.UserId)
                .NotEqual(Guid.Empty).WithMessage("Id пользователя не может быть пустым!");
        }
    }
}
