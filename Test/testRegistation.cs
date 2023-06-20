using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class testRegistation
    {
        private readonly Service s;

        public testRegistation(Service s)
        {
            this.s = s;
        }
        public void RunTests()
        {

            Response res2 = s.Register("guy@gmail.com", "Aa1235");
            if (res2.ErrorOccured)
            {
                Console.WriteLine(res2.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Created user successfully");
            }

            Response res3 = s.Register("meitar@gmail.com", "Aa1235");
            if (res3.ErrorOccured)
            {
                Console.WriteLine(res3.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Created user successfully");
            }


            /*

            Response res4 = s.Register(null, "Aa1235");
            if (res4.ErrorOccured)
            {
                Console.WriteLine(res4.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Created user successfully");
            }
            Response res5 = s.Register("pass@gmail.com", "1235aA");
            if (res5.ErrorOccured)
            {
                Console.WriteLine(res5.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Created user successfully");
            }
            Response res6 = s.Register("pass@gmail.com", "1235aA");
            if (res6.ErrorOccured)
            {
                Console.WriteLine(res6.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Created user successfully");
            }
            Response res7 = s.Register("pa@gmail.com", "1235aA1235aA1235aA1235aA");
            if (res7.ErrorOccured)
            {
                Console.WriteLine(res7.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Created user successfully");
            }
            */
        }
    }
}
