using BasicMessageSender.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
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
        public bool HideFromList(User loggedUser)
        {
            using (var Context = new BMSContext())
            {
                User user = Context.Users.Where(u => u.Username == loggedUser.Username).FirstOrDefault();
                if (user != null)
                    user.IsHidden = !user.IsHidden;
                Context.SaveChanges();
                return user.IsHidden;
            }
        }
        public void BlockUser(User logedUser, string blockedUser)
        {
            using(var Context = new BMSContext())
            {
                BlockedUsers bu = new BlockedUsers();
                bu.BlockerUser = Context.Users.Where(u => u.Username == logedUser.Username).FirstOrDefault();
                bu.BlockedUser = Context.Users.Where(u => u.Username == blockedUser).FirstOrDefault();
                Context.BlockedUsers.Add(bu);
                Context.SaveChanges();
            }
        }
        public void RegisterUser(string userName, string firstName, string lastName, string password, string phoneNumber, string email)
        {
            if (String.IsNullOrWhiteSpace(userName) || String.IsNullOrWhiteSpace(userName) || String.IsNullOrWhiteSpace(firstName) || String.IsNullOrWhiteSpace(lastName) || String.IsNullOrWhiteSpace(password) || String.IsNullOrWhiteSpace(email))
                throw new Exception("Mandatory fields represented with * cannot be null.");
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$");
            if (!regex.IsMatch(password))
                throw new Exception("Password is not acceptable. \n Password rules: Small and Capital letters and number, Minimum lenght:6");
            if (GetUserByUserName(userName) != null)
                throw new Exception("UserName already exist, please choose another one.");



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
                //TODO: Adding every relations to the BlockedUsers table and a variable is blocked to be able to do it in 1 query 
                //select * from Users Full Join BlockedUsers on Users.Id = BlockedUsers.BlockedUserId where BlockedUsers.BlockedUserId is null and Users.Username != '1'

                User user = GetUserByUserName(username);
                List<User> BlockedUsers = Context.BlockedUsers.Include("BlockedUser").Where(x => x.BlockerUser.Username == username).Select(x=>x.BlockedUser).ToList();
                List<User> AllUsers = Context.Users.Where(u => u.Username != username && !u.IsHidden).ToList();
                foreach(var bUser in BlockedUsers)
                {
                    AllUsers.Remove(bUser);
                }
                return AllUsers;
            }
        }
        public List<string> GetAllUsersName(string username)
        {
            using (var Context = new BMSContext())
            {
                User user = GetUserByUserName(username);

                return Context.Users.Where(u => u.Username != username && !u.IsHidden).Select(u=>u.Username).ToList();
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
