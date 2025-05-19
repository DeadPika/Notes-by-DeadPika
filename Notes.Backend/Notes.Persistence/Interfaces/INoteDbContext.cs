using Microsoft.EntityFrameworkCore;
using Notes.Domain.Models;
using Notes.Persistence.Entities;

namespace Notes.Persistence.Interfaces
{
    public interface INotesDbContext
    {
        DbSet<Note> Notes { get; set; }
        DbSet<UserEntity> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
