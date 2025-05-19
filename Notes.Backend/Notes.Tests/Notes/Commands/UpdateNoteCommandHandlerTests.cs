using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Persistence.Repositories.Notes.Commands.UpdateNote;
using Notes.Tests.Common;

namespace Notes.Tests.Notes.Commands
{
    public class UpdateNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateNoteCommandHandler_Success()
        {
            // Assert
            var handler = new UpdateNoteCommandHandler(Context);
            var updateDetails = "new title";

            // Act
            await handler.Handle(
                new UpdateNoteCommand
                {
                    Id = NotesContextFactory.NoteIdForUpdate,
                    UserId = NotesContextFactory.UserBId,
                    Title = updateDetails
                }, 
                CancellationToken.None);

            // Assert
            Assert.NotNull(await Context.Notes.SingleOrDefaultAsync(note =>
                note.Id == NotesContextFactory.NoteIdForUpdate &&
                note.Title == updateDetails));
        }

        [Fact]
        public async Task UpdateNoteCommandHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new UpdateNoteCommandHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new UpdateNoteCommand
                    {
                        Id = Guid.NewGuid(),
                        UserId = NotesContextFactory.UserBId
                    },
                    CancellationToken.None));
        }
        [Fact]
        public async Task UpdateNoteCommandHandler_FailOnWrongUserId()
        {
            // Arrange 
            var handler = new UpdateNoteCommandHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
               await handler.Handle(
                   new UpdateNoteCommand
                   {
                       Id = NotesContextFactory.NoteIdForUpdate,
                       UserId = NotesContextFactory.UserAId
                   },
                   CancellationToken.None));
        }
    }
}
