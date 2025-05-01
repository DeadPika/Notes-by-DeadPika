using Notes.Persistence.Repositories.Notes.Queries.GetNoteList;

namespace Notes.Persistence.Repositories.Notes.Queries.GetNoteList
{
    public class NoteListVm
    {
        public IList<NoteLookupDto>? Notes { get; set; }
    }
}
