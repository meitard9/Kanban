using IntroSE.Kanban.Backend.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Reflection;
using log4net.Config;
using System.IO;
using IntroSE.Kanban.Backend.BusinessLayer.UserPackage;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    class UserService
    {
        private readonly UserController userController;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public UserService()
        {
            userController = new UserController();
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            log.Info("starting up! UserService");
        }

        /// <summary>        
        /// checks the login status of a user. true if the user is logged in and false otherwise
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <returns>bool. The bool should be true for a looged in user and false otherwise
        public bool isLoggedIn(string email)
        {
            if (!userController.getUsers().ContainsKey(email))
            {
                log.Error("isLoggedIn - " + "user is not in the system");
                return false;
            }
            return userController.getUsers()[email].isLoggedIn;
        }


        ///<summary>This method registers a new user to the system.</summary>
        ///<param name="email">the user e-mail address, used as the username for logging the system.</param>
        ///<param name="password">the user password.</param>
        ///<returns cref="Response">The response of the action</returns>
        public Response Register(string email, string password)
        {
            try
            {

                userController.register(email, password);
                log.Info("Register- SUCCESS "+email +", "+password);
                return new Response();
            }
            catch (Exception e)
            {
                log.Error("Register- " + e.Message);
                return Response<Exception>.FromError(e.Message);
            }
            
        }


        /// <summary>
        /// Log in an existing user
        /// </summary>
        /// <param name="email">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns>A response object with a value set to the user, instead the response should contain a error message in case of an error</returns>
        public Response<User> Login(string email, string password)
        {
            try
            {
                userController.logIn(email, password);
                log.Info(email + " has logged into the system");
                User newUser = new User(email);
                return Response<User>.FromValue(newUser);
            }
            catch (Exception e)
            {
                log.Error("LOGIN- FAIL" + e.Message);
                return Response<User>.FromError( e.Message);
            }
        }


        /// <summary>        
        /// Log out an logged in user. 
        /// </summary>
        /// <param name="email">The email of the user to log out</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response Logout(string email)
        {
            try
            {
                userController.logout(email);
                log.Info(email + " has logged out the system");
                return new Response();
            }
            catch (Exception e)
            {
                log.Error("LOGOUT- FAIL " + e.Message);
                return new Response(e.Message);
            }
        }

        public Response LoadData()
        {
            try
            {
                userController.LoadAllUsers();
                log.Info("LoadData->SECCESS" );
            }
            catch (Exception e)
            {
                log.Error("LoadData->FAIL- " + e.Message);
                return new Response(e.Message);
            }
            return new Response();
        }

        public Response DeleteAll()
        {
            try
            {
                userController.DeleteAll();
                log.Info("UserServis->DeleteAll->SECCESS");
                return new Response();
            }
            catch (Exception e)
            {
                log.Error("UserServis->DeleteAll->FAIL- " + e.Message);
                return new Response(e.Message);
            }
        }











    }
}
