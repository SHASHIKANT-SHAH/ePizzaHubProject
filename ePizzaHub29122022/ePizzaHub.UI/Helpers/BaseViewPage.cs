using ePizzaHub.Models;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using System.Security.Claims;
using System.Text.Json;

namespace ePizzaHub.UI.Helpers
{
    public abstract class BaseViewPage<TModel> : RazorPage<TModel>
    {
        //[RazorInject]
        //public IUserAccessor _userAccessor { get; set; }
        public UserModel CurrentUser
        {
            get
            {
                //if (User != null)
                //    return _userAccessor.GetUser();
                //else
                //    return null;
                if (User.Claims.Count() > 0)
                {
                    string userData = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value;
                    var user = JsonSerializer.Deserialize<UserModel>(userData);
                    return user;
                }
                return null;
            }
        }
    }
}
