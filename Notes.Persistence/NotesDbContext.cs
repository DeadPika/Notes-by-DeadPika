using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Notes.Domain.Models;
using Notes.Persistence.Configuration;
using Notes.Persistence.Entities;
using Notes.Persistence.Interfaces;

namespace Notes.Persistence
{
    public class NotesDbContext
        : DbContext, INotesDbContext
    {
        private readonly AuthorizationOptions? _authOptions;
        public DbSet<Note> Notes { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public NotesDbContext(DbContextOptions<NotesDbContext> options, 
            IOptions<AuthorizationOptions> authOptions) : base(options) { _authOptions = authOptions.Value; }

        // Перегрузка для тестов.
        public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new NoteConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RolePermissionConfiguration(_authOptions));
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new PermissionConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
