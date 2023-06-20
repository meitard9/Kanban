using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using IntroSE.Kanban.Backend.ServiceLayer;
using System.Globalization;

namespace Test
{
    class emailtester
    {
        private readonly Service s;

        public emailtester(Service s)
        {
            this.s = s;
        }

        public bool RunTests(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;


            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }




        }
    }

    
}
