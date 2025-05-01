using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;
using Notes.Domain.Models;

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
            var userEntity = new User
            {
                Id = user.Id,
                Name = user.Name,
                HashPassword = user.HashPassword,
                Email = user.Email,
            };
            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            var userEntity = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception();
            return userEntity;
        }
    }
}
