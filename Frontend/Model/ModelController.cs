using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.ServiceLayer.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Model
{
    public class ModelController
    {
        private static Service service;
        private ModelController() {
            service = new Service();
        }
        private static ModelController _modelController;

        public static ModelController GetInstance()
        {
            if (_modelController == null)
            {
                _modelController = new ModelController();
            }
            return _modelController;
        }
        public void LoadAll()
        {
            service.LoadData();
        }

        public MUser Login(string email,string pas)
        {   Response < User> r= service.Login(email, pas);
            if (r.ErrorOccured)
                throw new Exception(r.ErrorMessage);
            User user= r.Value;
            return new MUser(user.Email,pas);
        }
        public void Logout(string email)
        {
            Response r = service.Logout(email);
            if (r.ErrorOccured)
                throw new Exception(r.ErrorMessage);
        }
        public void Register(string email, string pas)
        {
            Response r=service.Register(email,pas);
            if (r.ErrorOccured)
                throw new Exception(r.ErrorMessage);
        }
        public List<BoardM> getAllBoardMs()
        {
            List<BoardM> boards = new List<BoardM>();
            Response<List<BoardS>> r = service.getAllBoards();
            if (r.ErrorOccured)
                throw new Exception(r.ErrorMessage);
            foreach (BoardS b in r.Value)
            {
                boards.Add(new BoardM(b));
            }
            return boards;
        }
        public void RemoveBoard(string userEmail,BoardM boardM)
        {
            Response r=service.RemoveBoard(userEmail,boardM.CreatorEmail, boardM.Name);
            if (r.ErrorOccured)
                throw new Exception(r.ErrorMessage);
        }
        public List<TaskM> getInProgressTasks(string userEmail)
        {
            List<TaskM> tasks = new List<TaskM>();
            Response<IList<TaskS>> r = service.InProgressTasks(userEmail);
            if (r.ErrorOccured)
                throw new Exception(r.ErrorMessage);
            if (r.Value != null)
            {
                foreach (TaskS taskS in r.Value)
                {
                    tasks.Add(new TaskM(taskS));
                }
            }
            return tasks;
        }
        public void JoinBoard(string userEmail, BoardM boardM)
        {
            Response r=service.JoinBoard(userEmail, boardM.CreatorEmail, boardM.Name);
            if (r.ErrorOccured)
                throw new Exception(r.ErrorMessage);
        }
        public void AddBoard(string userEmail,string name)
        {
            Response r = service.AddBoard(userEmail, name);
            if (r.ErrorOccured)
                throw new Exception(r.ErrorMessage);
        }










    }
}
