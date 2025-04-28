using FluentValidation;

namespace Notes.Infrastructure.Repositories.Notes.Queries.GetNoteList
{
    public class GetNoteListQueryValidator : AbstractValidator<GetNoteListQuery>
    {
        public GetNoteListQueryValidator()
        {
            RuleFor(getNoteListQuery => getNoteListQuery.UserId).NotEqual(Guid.Empty).WithMessage("Id пользователя не может быть пустым!");
        }
    }
}
