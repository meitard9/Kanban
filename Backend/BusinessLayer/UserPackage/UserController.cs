using System;
using System.Collections.Generic;
using IntroSE.Kanban.Backend.DataAccessLayer.DOBs.UserPackage;

namespace IntroSE.Kanban.Backend.BusinessLayer.UserPackage
{
    class UserController
    {
        private Dictionary<string, User> users;
        private UserDalController udc;

        /// <summary>
        /// UserController Constructor
        /// </summary>
        public UserController()
        {
            this.users = new Dictionary<string, User>();
            udc = new UserDalController();
        }



        /// <summary>
        /// Users Getter
        /// </summary>
        /// <returns>Users dictionary</returns>
        public Dictionary<string, User> getUsers()
        {
            return this.users;
        }

        /// <summary>
        /// Register a new user to the system
        /// </summary>
        /// <param name="email">User's email</param>
        /// <param name="password">User's password</param>
        internal void register(string email, string password)
        {
            if (!isUserExists(email))
                throw new Exception("the user already exists");
            User u = new User(email, password);
            users.Add(email, u);
        }

        /// <summary>
        /// Checking if the user exists
        /// </summary>
        /// <param name="email">User's email</param>
        /// <returns>Boolean value to the question above</returns>
        public bool isUserExists(string email)
        {
            foreach (var userEmail in users.Keys)
            {

                if (userEmail.ToLower().Equals(email.ToLower()))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Logging the user in to the system
        /// </summary>
        /// <param name="email">User's email</param>
        /// <param name="password">User's password</param>
        /// <returns>User object</returns>
        internal User logIn(string email, string password)
        {
            if (!this.users.ContainsKey(email))
            {
                throw new Exception("This user name does not exists");
            }

            if (!this.users[email].password.Equals(password))
            {
                throw new Exception("Incorrect password");
            }
            this.users[email].login();
            return users[email];

        }

        /// <summary>
        /// Logging the user out of the system
        /// </summary>
        /// <param name="email">user's email</param>
        public void logout(string email)
        {
            if (!this.users.ContainsKey(email))
            {
                throw new Exception("This user name does not exists");
            }
            if (!users[email].isLoggedIn)
                throw new Exception("Thus user is already logged out");
            users[email].logOut();
        }

        /// <summary>
        /// Creating new user
        /// </summary>
        /// <param name="email">User's email</param>
        /// <param name="password">User's password</param>
        public void CreateUser(string email, string password)
        {
            users.Add(email, new User(email, password));
        }

        public void LoadAllUsers() {
            List<UserDOB> usersFromDal = udc.LoadUsers();
            foreach(UserDOB dUser in usersFromDal)
            {
                if(!users.ContainsKey(dUser.email))
                    users.Add(dUser.email, new User(dUser));
            }
        
        }
        public void DeleteAll()
        {
            udc.DeleteAll();
        }
    }
}

