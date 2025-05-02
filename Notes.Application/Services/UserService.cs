using Notes.Application.Interfaces;
using Notes.Domain.Models;

namespace Notes.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IUsersRepository _usersRepository;
        public UserService(IUsersRepository usersRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider) =>
            (_usersRepository, _passwordHasher, _jwtProvider) = (usersRepository, passwordHasher, jwtProvider);

        public async Task Register(string userName, string password, string email)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = userName,
                HashPassword = hashedPassword,
                Email = email
            };

            await _usersRepository.Add(user);
        }
        public async Task<string> Login(string email, string password)
        {
            var user = await _usersRepository.GetByEmail(email);

            var result = _passwordHasher.Verify(password, user.HashPassword);

            if (result == false)
            {
                throw new Exception("Failed to login.");
            }
            var token = _jwtProvider.GenerateToken(user);

            return token;
        }
    }
}
