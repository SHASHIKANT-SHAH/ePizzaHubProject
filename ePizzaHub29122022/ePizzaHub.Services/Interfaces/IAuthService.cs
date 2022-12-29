
using ePizzaHub.Core.Entities;
using ePizzaHub.Models;

namespace ePizzaHub.Services.Interfaces
{
    public interface IAuthService
    {
        UserModel ValidateUser(string Email, string password);
        bool CreateUser(User user, string Role);
    }
}
