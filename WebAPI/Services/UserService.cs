using DbEntities.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Handlers;

namespace WebAPI.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {
            new User { Id = 0, Name = "default" }
        };

        public async Task<User> Authenticate(string username, string password)
        {
            //var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password));
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.Name == username));

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so return user details without password
            return user.WithoutPassword();
        }

        //public async Task<User> GetUser(string username)
        //{
        //    return _users.SingleOrDefault(user => user.Username == username);
        //}

        public async Task<IEnumerable<User>> GetAll()
        {
            return await Task.Run(() => _users.WithoutPasswords());
        }
    }
}
