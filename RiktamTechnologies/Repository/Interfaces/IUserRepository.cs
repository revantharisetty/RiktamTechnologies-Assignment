using RiktamTechnologies.Models;

namespace RiktamTechnologies.Repository.Interfaces
{
    public interface IUserRepository
    {

        Task<List<User>> GetUsers();

        Task<User> GetUser(int? userId);
        Task<bool> AuthenticateUser(string userName, string password);

        Task<int> AddUser(User user);

        Task<int> DeleteUser(int? userId);

        Task UpdateUser(User user);
    }
}
