using IntroSE.Kanban.Backend.DataAccessLayer.DOBs;
using IntroSE.Kanban.Backend.DataAccessLayer.DOBs.BoardPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.BoardPackage
{
    /// <summary>
    /// representing the Column object
    /// </summary>
    class Column : IColumn
    {
        internal ColumnDOB dColumn;
        private string boardCreatorEmail;
        private string boardName;
        private string columnName { get; set; }
        internal string name
        {
            get { return columnName; }
            set
            {
                dColumn.type = value;
                columnName = value;
            }
        }
        private int _limit;
        private int id;
        internal int Id
        {
            get { return id; }
            set
            {

                dColumn.Id = value;
                id = value;
            }
        }


        internal int limit { get { return _limit; } set {
                if (value == -1)
                {
                    dColumn.limit = value;
                    _limit = value;
                    return;
                }
                if (tasks.Keys.Count > value)
                {
                    throw new Exception("the new limit is smaller than the number of the existing task");
                }
                dColumn.limit = value;
                _limit = value;} }

        private Dictionary<int,Task> tasks { get; set; }


        /// <summary>Constructor of a Column</summary>
        ///<param name="columnTaype">The columnTaype of the column</param>
        public Column(string BoardCreator, string BoardName, string columnTaype, int id)
        {   
            dColumn = new ColumnDOB(BoardCreator, BoardName, columnTaype,-1, id);
            this.boardCreatorEmail = BoardCreator;
            this.boardName = BoardName;
            this.limit = -1;//no limit by defult
            this.columnName = columnTaype;
            tasks = new Dictionary<int, Task>();
            this.id = id;
            dColumn.Insert();
        }

        public Column(ColumnDOB dColumn)
        {
            dColumn.persisted = true;
            this.dColumn = dColumn;
            tasks = new Dictionary<int, Task>();
            this.boardCreatorEmail = dColumn.BoardCreatorName;
            this.boardName = dColumn.BoardName;
            this.limit = dColumn.limit;
            this.columnName = dColumn.type;
            this.id = dColumn.Id;
        }

        /// <summary>Constructor of a Column</summary>
        ///<param name="columnTaype">The columnTaype of the column</param>
        public Column(string boardCreatror, string boardName, int limit,string columnTaype,int id)
        {
            dColumn = new ColumnDOB(boardCreatror, boardName, columnTaype, limit, id);
            this.columnName = columnTaype;
            tasks =new Dictionary<int, Task>();
            this.limit = limit;
            this.id = id;
            dColumn.Insert();
        }

        /// <summary>Getter of ColumnTaype</summary>
        /// <returns>Column's columnTaype</returns>
        public string getColumnName()
        {
            return columnName;
        }

        /// <summary>Getter of Dictionary of tasks</summary>
        /// <returns>Column's Tasks</returns>
        public Dictionary<int, Task> getTasks()
        {
            return tasks;
        }

        /// <summary>Getter of column's Limit</summary>
        /// <param name="id">The id of the task</param>
        /// <returns>The task with this id</returns>
        public Task getTask(int id)
        {
            return tasks[id];
        }

        /// <summary>deleting given task from this column</summary>
        /// <param name="task">The task that should be deleted</param>
        public void deleteTask(Task task)
        {
            tasks.Remove(task.id);
        }

        /// <summary>deleting all tasks from this column</summary>
        public void deleteTasks()
        {
            tasks = new Dictionary<int, Task>();
        }

        /// <summary>adding given task to this column</summary>
        /// <param name="task">The task that should be added</param>
        /// <exception cref="System.Exception">Thrown when this column is full by his limit</exception>
        /// <returns>The task that added</returns>
        public Task addTask(Task task)
        {
            if (limit != -1 && tasks.Count >= limit)
                throw new Exception("cannot add task due to limit");

            tasks.Add(task.id, task);
            return task;
        }
        /// <summary>Check if tasks can be added to a column</summary>
        /// <returns>True if can add</returns>
        public bool IsNotFull()
        {
            return limit == -1 || tasks.Count < limit;

        }
        public void sortByDueDate()
        {
            //Dictionary<int, Task> tasks = getTasks();
            this.tasks.OrderBy(key => key.Value.getDueDate());
            //return tasks;

        }

    }
}
