using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer.Objects;

namespace Frontend.Model
{
    public class TaskM
    {
        public int Id;
        public DateTime CreationTime;
        public string Title;
        public string Description;
        public DateTime DueDate;
        public string emailAssignee;
        private string _bgColor;
        public string bgColor { get {
                TimeSpan total = (CreationTime- DueDate) ;
                TimeSpan current = DateTime.Now-CreationTime;
                double pre = (current.TotalHours / total.TotalHours)*100;
                switch (pre)
                {
                    case >75.0:{ _bgColor = "orenge"; break; }
                }
                return _bgColor;



            } set { _bgColor = value; } }
        public TaskM(int id, DateTime creationTime, string title, string description, DateTime DueDate, string emailAssignee)
        {
            this.Id = id;
            this.CreationTime = creationTime;
            this.Title = title;
            this.Description = description;
            this.DueDate = DueDate;
            this.emailAssignee = emailAssignee;
        }
        public TaskM(TaskS task)
        {
            this.Id = task.Id;
            this.CreationTime = task.CreationTime;
            this.Title = task.Title;
            this.Description = task.Description;
            this.DueDate = task.DueDate;
            this.emailAssignee = task.emailAssignee;
        }
    }
}
