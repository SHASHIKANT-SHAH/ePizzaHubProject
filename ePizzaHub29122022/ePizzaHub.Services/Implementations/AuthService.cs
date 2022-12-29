

using ePizzaHub.Core;
using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Respositories.Interfaces;
using ePizzaHub.Services.Interfaces;

namespace ePizzaHub.Services.Implementations
{


    public class AuthService : IAuthService
    {
        IUserRepository _userRepository;
        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public bool CreateUser(User user, string Role)
        {
           return _userRepository.CreateUser(user, Role);
        }

        public UserModel ValidateUser(string Email, string password)
        {
             return _userRepository.ValidateUser(Email, password);
        }
    }
}
