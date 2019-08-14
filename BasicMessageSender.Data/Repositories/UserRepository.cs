using BasicMessageSender.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace BasicMessageSender.Data.Repositories
{
    public class UserRepository
    {


        public UserRepository()
        {
        }
        public User GetUserByUserName(string Username)
        {
            using (var Context = new BMSContext())
            {
                return Context.Users.Where(u => u.Username == Username).FirstOrDefault();
            }
        }
        public void HideFromList(string username)
        {
            using (var Context = new BMSContext())
            {
                User user = Context.Users.Where(u => u.Username == username).FirstOrDefault();
                if (user != null)
                    user.IsHidden = true;
            }
        }
        public void BlockUsersFromList(string blockedUser)
        {

        }
        public void RegisterUser(string userName, string firstName, string lastName, SecureString password, string phoneNumber)
        {
            if (userName is null)
                throw new Exception("UserName cannot be null.");
            if (firstName is null)
                throw new Exception("FirstName cannot be null.");

        }
        public List<User> GetAllUsers(string username)
        {
            User user = GetUserByUserName(username);

            return context.Users.Where(u => u.Username != username && !u.IsHidden).ToList();
        }
        public bool Login(string userName, string password)
        {
            using (var Context = new BMSContext())
            {
                return Context.Users.Where(u => u.Username == userName && u.Password == password).Count() > 0;
            }
        }

    }
}
