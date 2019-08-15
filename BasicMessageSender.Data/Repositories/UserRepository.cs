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
        public void BlockUser(User logedUser, string blockedUser)
        {
            using(var Context = new BMSContext())
            {
                BlockedUsers bu = new BlockedUsers();
                bu.BlockedUser = Context.Users.Where(u => u.Username == blockedUser).FirstOrDefault();
                logedUser.BlockedUsers.Add(bu);
                Context.SaveChangesAsync();
            }
        }
        public void RegisterUser(string userName, string firstName, string lastName, string password, string phoneNumber, string email)
        {
            if (userName is null)
                throw new Exception("UserName cannot be null.");
            if (firstName is null)
                throw new Exception("FirstName cannot be null.");
            using (var Context = new BMSContext())
            {
                User newUser = new User();
                newUser.Username = userName;
                newUser.FirstName = firstName;
                newUser.Surname = lastName;
                newUser.Password = password;
                newUser.PhoneNumber = phoneNumber;
                newUser.Email = email;
                Context.Users.Add(newUser);

                Context.SaveChanges();
            }
        }
        public List<User> GetAllUsers(string username)
        {
            using (var Context = new BMSContext())
            {
                User user = GetUserByUserName(username);

                return Context.Users.Where(u => u.Username != username && !u.IsHidden).ToList();
            }
        }
        public User Login(string userName, string password)
        {
            using (var Context = new BMSContext())
            {
                return Context.Users.Where(u => u.Username == userName && u.Password == password).FirstOrDefault();
            }
        }

    }
}
