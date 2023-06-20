using System;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace Tester
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Service service = new Service();
            service.LoadData();


        }
    }
}
