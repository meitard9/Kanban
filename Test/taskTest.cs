using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = IntroSE.Kanban.Backend.ServiceLayer.Task;

namespace Test
{
    class taskTest
    {
        private readonly Service s;


        public taskTest(Service s)
        {
            this.s = s;
        }
        public void RunTests()
        {
            /*string userEmail = "guy@gmail.com";
            string userPass = "Aa1235";
            string board1Name = "board1";
            DateTime dt = new DateTime(2022, 12, 25, 10, 30, 45);

            //create user
            s.Register(userEmail, userPass);

            //create board for this user
            s.AddBoard(userEmail, board1Name);
           
            //add task (id=1)
            s.AddTask(userEmail, board1Name, "title1","description",dt);
            s.GetColumn(userEmail, board1Name, 0);
            s.GetColumn(userEmail, board1Name, 1);
            s.GetColumn(userEmail, board1Name, 2);

            //add task (id=2)
            s.AddTask(userEmail, board1Name, "title1", "description", dt);
            s.GetColumn(userEmail, board1Name, 0);
            s.GetColumn(userEmail, board1Name, 1);
            s.GetColumn(userEmail, board1Name, 2);

            //in progress tasks
            s.InProgressTasks(userEmail);

            //advance to in progress
            s.AdvanceTask(userEmail, board1Name,0,1);
            s.GetColumn(userEmail,board1Name,0);
            s.GetColumn(userEmail, board1Name, 1);
            s.GetColumn(userEmail, board1Name, 2);

            //in progress tasks
            s.InProgressTasks(userEmail);

            //advance to done
            s.AdvanceTask(userEmail, board1Name, 1, 1);
            s.GetColumn(userEmail, board1Name, 0);
            s.GetColumn(userEmail, board1Name, 1);
            s.GetColumn(userEmail, board1Name, 2);

            //advance to after done -should return an erorr
            s.AdvanceTask(userEmail, board1Name, 2, 1);*/





        }
    }
}
