using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;
using Notes.Domain.Enums;
using Notes.Domain.Models;
using Notes.Persistence.Entities;

namespace Notes.Persistence.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly NotesDbContext _context;
        private readonly IMapper _mapper;
        public UsersRepository(NotesDbContext notesDbContext, IMapper mapper) => 
            (_context, _mapper) = (notesDbContext, mapper);
        public async Task Add(User user)
        {
            var roleEntity = await _context.Roles
                .SingleOrDefaultAsync(r => r.Id == (int)Role.User)
                ?? throw new InvalidOperationException();

            var userEntity = new UserEntity
            {
                Id = user.Id,
                UserName = user.Name,
                HashPassword = user.HashPassword,
                Email = user.Email,
                Roles = [roleEntity]
            };

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            var userEntity = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception();
            var user = _mapper.Map<User>(userEntity);
            return user;
        }

        public async Task<HashSet<Permission>> GetUserPermissions(Guid id)
        {
            var roles = await _context.Users
                .AsNoTracking()
                .Include(u => u.Roles)
                .ThenInclude(r => r.Permissions)
                .Where(u => u.Id == id)
                .Select(U => U.Roles)
                .ToArrayAsync();

            return roles
                .SelectMany(r => r)
                .SelectMany(r => r.Permissions)
                .Select(p => (Permission)p.Id)
                .ToHashSet();
        }
    }
}
