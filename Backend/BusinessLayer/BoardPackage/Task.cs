using IntroSE.Kanban.Backend.DataAccessLayer.DOBs;
using IntroSE.Kanban.Backend.DataAccessLayer.DOBs.BoardPackage;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.BoardPackage
{
    /// <summary>
    /// representing the Task object
    /// </summary>
    class Task
    {
        private const string format = "G";
        private const string culture= "es-ES";

        internal TaskDOB dTask;
        private string boardCreator { get; set; }
        private string boardName { get; set; }
        private int _id;
        public int id { get => _id; set => _id = value; }
        private DateTime creationTime { get; }
        private DateTime dueDate { get; set; }
        private string title{ get; set; }
        private string description { get; set; }
        private int _currColumn;
        internal int CurrColumn { get => _currColumn; 
            set {
                dTask.currColumn = value; 
                _currColumn = value;
            } }

        private string _emailAssignee;
        internal string emailAssignee { get=>_emailAssignee; 
            set {
                dTask.assignee = value;
                _emailAssignee = value;
            } }


        ///<summary>Task constructor</summary>
        ///<param name="id">task id</param>
        ///<param name="creationTime">Creation time of the task</param>
        ///<param name="title">Task's title</param>
        ///<param name="description">Description of the task</param>
        ///<param name="dueDate">Task's due date</param>
        public Task(int id,string boardCreator,string boardName, DateTime creationTime, string title, string description, DateTime dueDate, string emailAssignee)
        {
            dTask = new TaskDOB(id, boardCreator,boardName, 0, dueDate.ToString(format, CultureInfo.CreateSpecificCulture(culture)) 
                ,creationTime.ToString(format, CultureInfo.CreateSpecificCulture(culture)),title,description,emailAssignee);
            CurrColumn = 0;
            this.boardCreator = boardCreator;
            this.boardName = boardName;
            this.creationTime = creationTime;
            setDuedate(dueDate);
            setTitle(title);
            this.description = description;
            this.id = id;
            this.emailAssignee = emailAssignee;
            dTask.Insert();
        }

        public Task(TaskDOB task)
        {
            task.persisted = true;
            dTask = task;
            id = dTask.id;
            CurrColumn = dTask.currColumn;
            boardCreator = dTask.boardCreatorName;
            boardName = dTask.boardName;
            creationTime = DateTime.ParseExact(dTask.creationDate, format, CultureInfo.CreateSpecificCulture(culture));
            dueDate = DateTime.ParseExact(dTask.dueDate, format, CultureInfo.CreateSpecificCulture(culture));
            title = dTask.title;
            description = dTask.description;
            emailAssignee = dTask.assignee;
        }

        ///<summary>CreationTime getter</summary>
        ///<returns cref="DateTime">Creation time of the task</returns>
        public DateTime getCreationTime()
        {
            return creationTime;
        }

        ///<summary>Due Date Getter</summary>
        ///<returns cref="DateTime">Task's due date</returns>
        public DateTime getDueDate()
        {
            return dueDate;
        }

        ///<summary>Title Getter</summary>
        ///<returns cref="string">Task's title</returns>
        public string getTitle()
        {
            return title;
        }

        ///<summary>Description Getter</summary>
        ///<returns cref="string">Description of the task</returns>
        public string getDescription()
        {
            return description;
        }

        ///<summary>Checking if it's possible to change task's fields</summary>
        ///<returns cref="DateTime">Bool value for the question above</returns>
        private bool canChange()
        {
            return CurrColumn!=2;
        }

        ///<summary>Due date setter</summary>
        ///<param name="newDate">new task due date</param>
        public void setDuedate(DateTime newDate)
        {
            dTask.dueDate = newDate.ToString();
            this.dueDate = newDate;
        }

        ///<summary>Title setter</summary>
        ///<param name="newTitle">new task Title</param>
        public void setTitle(string newTitle)
        {
            dTask.title = newTitle;
            this.title = newTitle;
        }

        ///<summary>Title Description setter</summary>
        ///<param name="newDes">new task Description</param>
        public void setDescription(string newDes)
        {
            dTask.description = newDes;
            description = newDes;
        }  
    }
}
