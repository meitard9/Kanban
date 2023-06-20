using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class TestUserDOB
    {
        private readonly Service s;
        private string email = "meitar@gmail.com";
        private string email2 = "mei222@gmail.com";
        private string pass = "aA1234";
        private string boardName = "board1";
        private string title = "title";
        private string des = "description";
        private DateTime dt = new DateTime(2022, 12, 25, 10, 30, 45);
        private DateTime newDt = new DateTime(2022, 01, 01, 01, 01, 01);
        private DateTime FailDt = new DateTime(2020, 01, 01, 01, 01, 01);


        public TestUserDOB(Service s)
        {
            this.s = s;
        }
        public void RunTests()
        {
            //s.DeleteData();
            s.Register(email,pass);
            s.AddBoard(email,boardName);
            s.AddTask(email, email, boardName, title, des,dt);
            s.RemoveBoard(email, email, boardName);

            //s.LoadData();
            //s.LoadData();
            //s.InProgressTasks(email);

            //create user

            // s.Register("meitargmail.com", "aA1234");
            //s.Register("yarin@gmail.com", "aA1234");
            // s.Register("eden@gmail.com", "aA1234");

            s.Login(email, pass);

            //create board for this user
            s.AddBoard(email, boardName);

            //add task (id=1)
            s.AddTask(email, email, boardName, "title1", "description", dt);
            s.GetColumn(email, email, boardName, 0);
            s.GetColumn(email, email, boardName, 1);
            s.GetColumn(email, email, boardName, 2);

            //add task (id=2)
            s.AddTask(email, email, boardName, "title1", "description", dt);
            s.GetColumn(email, email, boardName, 0);
            s.GetColumn(email, email, boardName, 1);
            s.GetColumn(email, email, boardName, 2);

            //in progress tasks
            s.InProgressTasks(email);

            //advance to in progress
            s.AdvanceTask(email, email, boardName, 0, 1);
            s.GetColumn(email, email, boardName, 0);
            s.GetColumn(email, email, boardName, 1);
            s.GetColumn(email, email, boardName, 2);

            //in progress tasks
            s.InProgressTasks(email);

            //advance to done
            s.AdvanceTask(email, email, boardName, 1, 1);
            s.GetColumn(email, email, boardName, 0);
            s.GetColumn(email, email, boardName, 1);
            s.GetColumn(email, email, boardName, 2);

            //advance to after done -should return an erorr
            s.AdvanceTask(email, email, boardName, 2, 1);

           //s.LoadData();

           s.RemoveBoard(email, email, boardName);


           s.LoadData();
           s.Login(email, pass);
           s.Logout(email);


        }
        public void RunAllSeviceTests1()
        {
            s.DeleteData();
            //s.LoadData();
            s.Register("meitar@gmail.com", "aA1234");
            s.Register("yarin@gmail.com", "aA1234");
            s.Login("meitar@gmail.com", "aA1234");
            s.Register("reem@gmail.com", "aA1234");
            s.Login("reem@gmail.com", "aA1234");
            s.AddBoard("meitar@gmail.com", "board");
            s.Login("yarin@gmail.com", "aA1234");
            s.AddBoard("yarin@gmail.com", "board");
            s.JoinBoard("yarin@gmail.com", "meitar@gmail.com", "board");
            //s.RemoveBoard("meitar@gmail.com", "meitar@gmail.com", "board");
            DateTime dt= new DateTime(2022, 7, 15, 3, 15, 0);
            s.AddTask("yarin@gmail.com", "meitar@gmail.com", "board", "yarinTask", "This take was made for yarin", dt);
            s.AddTask("yarin@gmail.com", "meitar@gmail.com", "board", "yarinTask", "This take was made for yarin3", dt);
            s.Login("meitar@gmail.com", "aA1234");
            s.AddTask("meitar@gmail.com", "meitar@gmail.com", "board", "yarinTask", "This take was made for m1", dt);
            s.AddTask("meitar@gmail.com", "meitar@gmail.com", "board", "yarinTask", "This take was made for m2", dt);
            s.AddTask("meitar@gmail.com", "yarin@gmail.com", "board", "yarinTask", "This take was made for y3", dt);
            s.AddTask("meitar@gmail.com", "yarin@gmail.com", "board", "yarinTask", "This take was made for y4", dt);
            s.AddTask("meitar@gmail.com", "yarin@gmail.com", "board", "yarinTask", "This take was made for y5", FailDt);
            Response res = s.GetColumn("yarin@gmail.com", "meitar@gmail.com", "board", 0);
            res=s.GetColumnName("yarin@gmail.com", "meitar@gmail.com", "board", 0);
            res = s.GetColumnLimit("yarin@gmail.com", "meitar@gmail.com", "board", 0);
            s.LimitColumn("yarin@gmail.com", "meitar@gmail.com", "board",0, 4);
            res = s.GetColumnLimit("yarin@gmail.com", "meitar@gmail.com", "board", 0);
            DateTime dt2 = new DateTime(2023, 10, 15, 3, 15, 0);
            s.UpdateTaskTitle("yarin@gmail.com", "meitar@gmail.com", "board", 0, 1, "newTitle");
            s.UpdateTaskDescription("yarin@gmail.com", "meitar@gmail.com", "board", 0, 1, "newDescription");
            s.UpdateTaskDueDate("yarin@gmail.com", "meitar@gmail.com", "board", 0, 1, dt2);
            s.AssignTask("yarin@gmail.com", "meitar@gmail.com", "board", 0, 1, "meitar@gmail.com");
            //s.Logout("yarin@gmail.com");
            s.Login("meitar@gmail.com", "aA1234");
            s.AdvanceTask("meitar@gmail.com", "meitar@gmail.com", "board", 0, 1);
            res = s.InProgressTasks("meitar@gmail.com");
            res = s.GetBoardNames("yarin@gmail.com");
            //s.RemoveBoard("meitar@gmail.com", "meitar@gmail.com", "board");
            s.AddColumn("yarin@gmail.com", "meitar@gmail.com", "board", 1, "MyNewColumn");
            s.MoveColumn("yarin@gmail.com", "meitar@gmail.com", "board", 1,1);
            s.MoveColumn("yarin@gmail.com", "meitar@gmail.com", "board", 2, 1);
            s.MoveColumn("yarin@gmail.com", "meitar@gmail.com", "board", 1, 1);
            s.RenameColumn("yarin@gmail.com", "meitar@gmail.com", "board", 3, "fuckinA");
            //s.RemoveColumn("yarin@gmail.com", "meitar@gmail.com", "board", 2);

            //s.Logout("meitar@gmail.com");
        }

        public void RunAllSeviceTests2()
        {
            s.LoadData();
            //s.Register("reem@gmail.com", "aA1234");
             
            s.Login("yarin@gmail.com", "aA1234");
            s.Login("meitar@gmail.com", "aA1234");
            //s.AddTask("yarin@gmail.com", "yarin@gmail.com", "board", "yarinTask", "advanceTaskAttempt", dt);
            //s.AddTask("yarin@gmail.com", "yarin@gmail.com", "board", "yarinTask", "advanceTaskAttempt2", dt);
            //s.AdvanceTask("yarin@gmail.com", "yarin@gmail.com", "board", 0, 3);
            //s.AdvanceTask("yarin@gmail.com", "yarin@gmail.com", "board", 0, 4);
            //Response res = s.AddTask("yarin@gmail.com", "yarin@gmail.com", "board", "yarinTask", "advanceTaskAttempt2", dt);
            Response res=s.GetColumn("yarin@gmail.com", "meitar@gmail.com","board",2);
            s.AssignTask("yarin@gmail.com", "meitar@gmail.com", "board", 1,1, "meitar@gmail.com");
            res = s.GetColumnName("yarin@gmail.com", "meitar@gmail.com", "board", 2);
            //res = s.InProgressTasks("yarin@gmail.com");
            //s.AddBoard("meitar@gmail.com", "aA1234");

            //s.JoinBoard()
            //s.Logout("meitar@gmail.com");
        }
        public void RunAllSeviceTests3()
        {
            s.LoadData();
            s.Login("meitar@gmail.com", "aA1234");
            s.Login("yarin@gmail.com", "aA1234");
            s.AddTask("meitar@gmail.com", "yarin@gmail.com", "board", "yarinTask", "This take was made for y3", dt);
            s.AddTask("meitar@gmail.com", "yarin@gmail.com", "board", "yarinTask", "This take was made for y4", dt);
            s.AddBoard(email,boardName+"2");
            s.AddTask(email, email, boardName + "2", "newT", des, dt);
            s.AddTask(email, email, boardName + "2", "newT", des, dt);

        }

    }
}



