using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Model
{
    public class MUser
    {
        public string UserEmail { get;private set; }
        public string Password { get;private set; }

        public MUser(string email, string pass)
        {
            this.UserEmail = email;
            this.Password = pass;
        }

}
}
