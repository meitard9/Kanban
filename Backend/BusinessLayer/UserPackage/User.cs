using IntroSE.Kanban.Backend.DataAccessLayer.DOBs;
using IntroSE.Kanban.Backend.DataAccessLayer.DOBs.UserPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.UserPackage
{



    class User
    {
        private const int PASS_MAX_LENGHT = 20;
        private const int PASS_MIN_LENGHT = 4;
        private UserDOB dUser;
        private string email { get; }
        private string pass;
        internal string password
        {
            get { return pass; }
            set
            {
                VerifyPassword(value);
                dUser.Password = value;
                pass= value;
            }
        }
        public bool isLoggedIn { get; private set; }



        ///<summary>User constructor</summary>
        ///<param name="email">User's email</param>
        ///<param name="password">User's password</param>
        public User(string email, string password)
        {
            dUser = new UserDOB(email, password);
            if (!checkEmail(email)) throw new Exception("Email is invalid");
            VerifyPassword(password);
            this.email = email;
            this.password = password;
            isLoggedIn = false;
            dUser.Insert();
        }
        public User(UserDOB dUser)
        {
            dUser.persisted = true;
            this.dUser = dUser;
            email = dUser.email;
            pass = dUser.Password;
            isLoggedIn = false;
        }


        ///<summary>Logging the user in</summary>
        public void login()
        {
            this.isLoggedIn = true;
        }

        ///<summary>Logging the user out</summary>
        public void logOut()
        {
            this.isLoggedIn = false;
        }
        ///<summary>Check if the password is verify</summary>
        private void VerifyPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || !(password.Length >= PASS_MIN_LENGHT && password.Length <= PASS_MAX_LENGHT)
                || !(password.Any(char.IsUpper)) || !(password.Any(char.IsLower)) || !(password.Any(char.IsDigit)))
            {
                throw new Exception("This password does not meet the requirements: \n1) length of " + PASS_MIN_LENGHT + " to " + PASS_MAX_LENGHT +
                    " characters \n2)must include at least one uppercase letter, one small character and a number");
            }
        }

        /// <summary>
        /// Checking if the email address is valid
        /// </summary>
        /// <param name="emailaddress">email address</param>
        /// <returns>Boolean value to the question above</returns>
        private bool checkEmail(string emailaddress)
        {
            bool IsValid = true;
            if (string.IsNullOrWhiteSpace(emailaddress))
                IsValid = false;
            try
            {
                return Regex.IsMatch(emailaddress,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                IsValid = false;
            }
            if (!IsValid)
                throw new Exception("The email address you've enterred is invalid");
            return IsValid;
        }




    }
}
