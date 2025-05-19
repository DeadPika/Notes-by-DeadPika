namespace Notes.Application.Interfaces
{
    public interface IUserService
    {
        Task<string> Login(string email, string password);
        Task Register(string userName, string password, string email);
    }
}