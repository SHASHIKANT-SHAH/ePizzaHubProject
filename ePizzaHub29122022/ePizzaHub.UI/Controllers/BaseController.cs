using ePizzaHub.Models;
using ePizzaHub.UI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using System.Security.Claims;
using System.Text.Json;

namespace ePizzaHub.UI.Controllers
{
    public class BaseController : Controller
    {
        [RazorInject]
        public IUserAccessor _userAccessor { get; set; }
        public UserModel CurrentUser
        {
            get
            {
                if (User != null)
                    return _userAccessor.GetUser();
                else
                    return null;
                //if (User.Claims.Count() > 0)
                //{
                //    string userData = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value;
                //    var user = JsonSerializer.Deserialize<UserModel>(userData);
                //    return user;
                //}
                //return null;
            }
        }
    }
}
