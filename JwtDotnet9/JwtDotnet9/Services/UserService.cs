using JwtDotnet9.Models;

namespace JwtDotnet9.Services;

public interface IUserService
{
    User? GetUser(string username, string password);
}

public class UserService : IUserService
{
    private List<User> _users = new()
    {
        new()
        {
            Username = "admin",
            Password = "admin123",
            Roles = ["Admin"]
        },
        new()
        {
            Username = "user",
            Password = "user123",
            Roles = ["User"]
        }
   };

    public User? GetUser(string username, string password)
    {
        return _users.FirstOrDefault(u => u.Username == username && u.Password == password);
    }
}