using IntroSE.Kanban.Backend.ServiceLayer;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Service s = new Service();
            /*new testRegistation(s).RunTests();
            new emailtester(s).RunTests("cacascbvb@fvfvsdc.com");*/
            //new taskTest(s).RunTests();
            //new TestUserDOB(s).RunTests();
            //new TestUserDOB(s).RunAllSeviceTests1();
            new TestUserDOB(s).RunAllSeviceTests1();
            //new TestBoardDOB(s).RunTests();

            }
    }
}
