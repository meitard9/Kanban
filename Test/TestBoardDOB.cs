using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{

    class TestBoardDOB
    {
        private readonly Service s;
        private string email = "meitar@gmail.com";
        private string boardname = "myBoard";
        public TestBoardDOB(Service s)
        {
            this.s = s;

        }
        public void RunTests()
        {
            //s.LoadData();
            s.AddBoard(email, "aA1234");
        }

    }
}
