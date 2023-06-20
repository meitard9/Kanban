using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer.Objects
{
    public struct BoardS
    {
        public readonly string name;
        public readonly string userEmail;

        internal BoardS(string name, string userEmail)
        {
            this.name = name;
            this.userEmail = userEmail;
        }
    }
}
