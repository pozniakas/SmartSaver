using DbEntities.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace WebAPI.Handlers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static User WithoutPassword(this User user)
        {
            //user.Password = null;
            return user;
        }

        public static int? GetId(this ClaimsPrincipal user)
        {
            return int.TryParse(user.Claims.FirstOrDefault().Value, out var id) ? id : null;
        }

        public static bool Is(this ClaimsPrincipal httpUser, User user)
        {
            int? userId = int.TryParse(httpUser.Claims.FirstOrDefault().Value, out var id) ? id : null;
            return user == null ? true : userId == user.Id;
        }
    }
}
