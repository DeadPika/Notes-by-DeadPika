using Microsoft.EntityFrameworkCore;
using Notes.Domain.Enums;
using Notes.Domain.Models;

namespace Notes.Application.Interfaces
{
    public interface IUsersRepository
    {
        Task Add(User user);
        Task<User> GetByEmail(string email);
        Task<HashSet<Permission>> GetUserPermissions(Guid id);
    }
}
