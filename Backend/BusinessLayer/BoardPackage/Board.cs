using IntroSE.Kanban.Backend.DataAccessLayer.DOBs;
using IntroSE.Kanban.Backend.DataAccessLayer.DOBs.BoardPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Tests")]

namespace IntroSE.Kanban.Backend.BusinessLayer.BoardPackage
{


    /// <summary>
    /// representing the Board object
    /// </summary>
    internal class Board
    {
        internal BoardDOB dBoard;
        //internal int id { get;private set; }
        private readonly int minCol = 2;
        private string name { get; set; }
        private string userEmail { get; set; }
        private Dictionary<int, Column> columns { get; set; }
        public Dictionary<int, Column> Columns
        {
            get { return columns; }
        }
        private int _taskIndex;


        internal int taskIndex
        {
            get => _taskIndex;
            set
            {
                dBoard.taskIndex = value;
                _taskIndex = value;
            }
        }

        /// <summary>Constructor of a Board</summary>
        ///<param name="id">The id of the board</param>
        ///<param name="name">The name of the board</param>
        ///<param name="userEmail">The userEmail of the board</param>
        public Board(/*int id,*/ string name, string userEmail)
        {
            dBoard = new BoardDOB(/*id,*/userEmail,name,this.taskIndex);
            taskIndex = 1;
            //this.id = id;
            this.name = name;
            this.userEmail = userEmail;
            columns = new Dictionary<int, Column>();
            columns.Add(0, new Column(userEmail, name, "backlog",0));
            columns.Add(1, new Column(userEmail, name, "in progress",1));
            columns.Add(2, new Column(userEmail, name, "done",2));
            dBoard.Insert();
        }


        public Board(BoardDOB dalBoard, List<ColumnDOB> columns)
        {
            dBoard = dalBoard;
            dBoard.persisted = true;
            taskIndex = dalBoard.taskIndex;
            //this.id = dBoard.boardId;
            this.name = dBoard.name;
            this.userEmail = dBoard.userEmail;
            int index = 0;
            this.columns = new Dictionary<int, Column>();
            foreach (var col in columns)
            {
                this.Columns.Add(index, new Column(col));
                index++;
            }

        }


        /// <summary>Add task to backlog column</summary>
        ///<param name="task">the task needed to add</param>
        ///<exception cref="System.Exception">Thrown when this column is full by his limit</exception>
        ///<exception cref="System.ArgumentNullException">Thrown when this argument is null</exception>
        ///<exception cref="System.ArgumentException">from the dictionary</exception>
        ///<returns>the added task</returns>
        public Task addTask(Task task)
        {
            task.CurrColumn = 0;
            taskIndex++;
            Task t = columns[0].addTask(task);
            return t;
        }
        /*
        public bool IsNotFull()
        {
            return backlog.IsNotFull();
        }*/
        public void addTaskForLoad(Task task) //TODO fix
        {
            getColumn(task.CurrColumn).addTask(task);
            /*
            switch (task.CurrColumn)
            {
                case 0:
                    backlog.addTask(task);
                    break;
                case 1:
                    inProgress.addTask(task);
                    break;
                case 2:
                    done.addTask(task);
                    break;

            }*/

        }


        /// <summary>Getter of Name of board</summary>
        /// <returns>board's name</returns>
        public string getName()
        {
            return name;
        }


        public string getUserEmail()
        {
            return userEmail;
        }


        /// <summary>Getter of specific Column of board</summary>
        /// <returns>board's column</returns>
        ///<param name="column">The column's name</param>
        public Column getColumn(string column)
        {
            Column c = null;
            foreach (var col in columns.Values)
            {
                if (col.getColumnName().Equals(column))
                    c = col;
            }
            if (c == null)
                throw new Exception("column doesn't exist");
            return c;
        }

        /// <summary>
        /// getter for a column at this Board, by its columnOrdinal
        /// </summary>
        /// <param name="columnOrdinal">column ordinal num.</param>
        /// <returns>A Column object, representing the desired column</returns>
        public Column getColumn(int columnOrdinal)
        {
            if (!columns.ContainsKey(columnOrdinal))
                throw new Exception("cannot get column");
            return columns[columnOrdinal];
        }

        /// <summary>
        /// getter for columns at this Board
        /// </summary>
        /// <returns>A Dictionary<int, Column> object, representing this Board columns, mapped by their ordinary number</returns>
        public Dictionary<int, Column> getColumns()
        {
            return columns;
        }

        /// <summary>Getter of specific Column's limit</summary>
        /// <returns>column's limit</returns>
        ///<param name="columnName">The column's name</param>
        public int GetColumnLimit(string columnName)
        {
            return getColumn(columnName).limit;
        }

        /// <summary>Getter of specific Column's name</summary>
        /// <returns>column's name</returns>
        ///<param name="columnOrdinal">The column's id</param>
        public string GetColumnName(int columnOrdinal)
        {
            return getColumn(columnOrdinal).getColumnName();
        }

        /// <summary>
        /// Setter limit in specific column
        /// </summary>
        /// <param name="columnOrdinal">column id</param>
        /// <param name="limit">new limit</param>
        ///<exception cref="System.Exception">if the new limit smaller then the existing tasks</exception>
        public void setLimit(int columnOrdinal, int limit)
        {
            try
            {
                columns[columnOrdinal].limit = limit;
                /*
                if (column.Equals("backlog"))
                {
                    backlog.limit = limit;
                }
                if (column.Equals("in progress"))
                {
                    inProgress.limit = limit;
                }
                     if (column.Equals("done"))
                {
                    done.limit = limit;
                }*/
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// advance specific task
        /// </summary>
        /// <param name="column">column name</param>
        /// <param name="task">the task that needed to advancing</param>
        /// <exception cref="System.Exception">if the next column is full by his limit</exception>
        public void advanceTask(int columnOrdinal, Task task)
        {
            try
            {
                /*
                if (column.Equals("backlog"))
                {
                    if(inProgress.limit != -1 && inProgress.getTasks().Count >= inProgress.limit)
                    {
                        throw new Exception("cannot advance task due to limit problem");
                    }
                    backlog.deleteTask(task);
                    task.CurrColumn = 1;
                    inProgress.addTask(task);
                }
                if (column.Equals("in progress"))
                {
                    if (done.limit != -1 && done.getTasks().Count >= done.limit)
                    {
                        throw new Exception("cannot advance task due to limit");
                    }
                    inProgress.deleteTask(task);
                    task.CurrColumn = 2;
                    done.addTask(task);
                }
                */
                getColumn(columnOrdinal).deleteTask(task);
                task.CurrColumn = columnOrdinal + 1;
                getColumn(columnOrdinal+1).addTask(task);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// all of in progress task from specific board
        /// </summary>
        /// <returns>List<Task> that contains all of in progress task from specific board</returns>
        public List<Task> InProgressTasks()
        {
            return getColumn("in progress").getTasks().Values.ToList();
        }
        public List<Task> InProgressTasksByAssignee(string userEmail)
        {
            List<Task> inProgTask = new List<Task>();
            foreach (var col in Columns) 
            { 
                if(col.Key>0 && col.Key < this.getMaxIdOfColumns())
                { 
                    foreach (var t in getColumn(col.Key).getTasks().Values.ToList())
                    {
                        if (t.emailAssignee == userEmail)
                        {
                            inProgTask.Add(t);
                        }
                    }
                }
            }
            return inProgTask;
        }
        /// <summary>
        /// delete tasks' board when the board deleted by his creator
        /// </summary>  
        public void deleteTasks()
        {
            string[] c = { "backlog", "in progress", "done" };
            List<string> columns = new List<string>(c);
            foreach (var n in columns)
            {
                Column name = this.getColumn(n);
                name.deleteTasks();
            }
        }
        /// <summary>
        /// set all emails' Assignes of tasks' board to null when the board deleted by his creator
        /// </summary> 
        public void resetTaskAssigne()
        {
            string[] c = { "backlog", "in progress", "done" };
            List<string> columns = new List<string>(c);
            foreach (var n in columns)
            {
                Column name = this.getColumn(n);
                foreach (var t in name.getTasks().Values)
                {
                    t.emailAssignee = null;
                }
            }
            

        }

        /// <summary>
        /// removes a column from this Board, by its columnOrdinal
        /// </summary>
        /// <param name="columnOrdinal">column ordinal num.</param>
        public void removeColumn(int columnOrdinal)
        {
            Dictionary<int, Column> newColumns = new Dictionary<int, Column>();
            if (!columns.ContainsKey(columnOrdinal))
                throw new Exception("cannot get column");
            if (columns.Count <= minCol)
                throw new Exception("a board must have at least 2 columns");

            
            if (columnOrdinal == 0) //Removing the leftmost column and transferring it's tasks to it's right neighbour
            {
                if (!columns.ContainsKey(columnOrdinal + 1))
                    throw new Exception("cannot get next column");
                if (columns[columnOrdinal + 1].getTasks().Count + columns[columnOrdinal].getTasks().Count >=
                    columns[columnOrdinal + 1].limit && columns[columnOrdinal + 1].limit!=-1)
                    throw new Exception("can't advance task due to limit");
                foreach (var t in columns[columnOrdinal].getTasks().Values)
                {
                    columns[columnOrdinal + 1].addTask(t);

                }
            }

            else //Removing any column which is not the leftmost one and transferring it's tasks to it's left neighbour
            {
                if (columns[columnOrdinal - 1].getTasks().Count + columns[columnOrdinal].getTasks().Count >=
                    columns[columnOrdinal - 1].limit && columns[columnOrdinal + 1].limit != -1)
                    throw new Exception("can't advance task due to limit");

                foreach (var t in columns[columnOrdinal].getTasks().Values)
                {
                    columns[columnOrdinal - 1].addTask(t);
                }
            }

            this.columns[columnOrdinal].dColumn.Delete(); //Removing the column from the DB

            foreach (var col in columns) //Creating new column collection without the removed column
            {
                if (col.Key < columnOrdinal)
                    newColumns.Add(col.Key, col.Value);
                else if (col.Key > columnOrdinal)
                {
                    newColumns.Add(col.Key - 1, col.Value);
                }
            }
            columns = newColumns;




            foreach (var col in columns)  //updating the tasks currect column field
            {
                foreach (var task in col.Value.getTasks().Values)
                {
                    task.CurrColumn = col.Key;
                    //task.dTask.Update(TaskDOB.CurrColumnName, col.Key);
                }
                col.Value.Id = col.Key;
                //col.Value.dColumn.Update(ColumnDOB.ColumnId, col.Value.dColumn.Id);
            }

        }

        /// <summary>
        /// adds a column at this Board
        /// </summary>
        /// <param name="columnOrdinal">column ordinal num.</param>
        /// <param name="columnName">column name</param>
        /// <returns>A Column object, representing the added column</returns>
        public Column addColumn(int columnOrdinal, string columnName)
        {
            foreach (var col in columns.Values)
            {
                if (col.getColumnName().Equals(columnName))
                    throw new Exception("column name already exists in board");
            }

            Dictionary<int, Column> newColumns = new Dictionary<int, Column>();
            if (columnOrdinal >= columns.Count + 1)
                throw new Exception("Incorrect ordinal, the column ordinal inserted is higher by 2 or more than the rightmost column");
            if (columnOrdinal == columns.Count)
            {
                columns.Add(columnOrdinal, new Column(this.userEmail,this.name, columnName,columnOrdinal));
                return columns[columnOrdinal];
            }
            else { 
            
                int index = 1;
                foreach (var col in columns)
                {
                    if (col.Key < columnOrdinal)
                        newColumns.Add(col.Key, col.Value);
                    else if (col.Key == columnOrdinal)
                    {
                        newColumns.Add(columnOrdinal, null);
                        newColumns.Add(columnOrdinal + index, col.Value);
                        index++;
                    }
                    else
                    {
                        newColumns.Add(columnOrdinal + index, col.Value);
                        index++;
                    }
                }
                columns = newColumns;
            }
            foreach (var col in columns)
            {
                if (col.Value != null)
                {
                    col.Value.Id = col.Key;
                    //toDalObject().updateColeId(userEmail, col.Value.getName(), col.Key);
                    foreach (var task in col.Value.getTasks())
                    {
                        task.Value.CurrColumn=col.Key;
                    }
                }
            }
            columns[columnOrdinal] = new Column(this.userEmail, this.name, columnName, columnOrdinal);
            return columns[columnOrdinal];
        }

        /// <summary>
        /// moves a column to the right and swaps its righthand column, at this Board
        /// </summary>
        /// <param name="columnOrdinal">column ordinal num. to be shifted to the right</param>
        /// <param name="creator"> the creator of this board</param>
        /// <returns>A Column object, representing the shifted to the right's column</returns>
        public Column MoveColumnRight(int columnOrdinal)
        {

            Column c1 = columns[columnOrdinal];
            Column c2 = columns[columnOrdinal + 1];
            columns.Remove(columnOrdinal);
            columns.Remove(columnOrdinal + 1);
            columns.Add(columnOrdinal + 1, c1);
            foreach (var task in columns[columnOrdinal + 1].getTasks())
            {
                task.Value.CurrColumn = columnOrdinal + 1; 
            }
            columns.Add(columnOrdinal, c2);
            foreach (var task in columns[columnOrdinal].getTasks())
            {
                task.Value.CurrColumn = columnOrdinal;
            }

            c1.Id = -1;
            c2.Id = columnOrdinal;
            c1.Id = columnOrdinal + 1;

            return columns[columnOrdinal + 1];
        }

        /// <summary>
        /// moves a column to the left and swaps its lefthand column, at this Board
        /// </summary>
        /// <param name="columnOrdinal">column ordinal num. to be shifted to the left</param>
        /// <param name="creator"> the creator of this board</param>
        /// <returns>A Column object, representing the shifted to the left's column</returns>
        public Column MoveColumnLeft(int columnOrdinal)
        {
            Column c1 = columns[columnOrdinal];
            Column c2 = columns[columnOrdinal - 1];
            columns.Remove(columnOrdinal);
            columns.Remove(columnOrdinal - 1);
            columns.Add(columnOrdinal - 1, c1);
            foreach (var task in columns[columnOrdinal - 1].getTasks().Values)
            {
                task.CurrColumn = columnOrdinal - 1; 
            }
            columns.Add(columnOrdinal, c2);
            foreach (var task in columns[columnOrdinal].getTasks().Values)
            {
                task.CurrColumn = columnOrdinal ;
            }

            c1.Id = -1;
            c2.Id = columnOrdinal;
            c1.Id = columnOrdinal - 1;
            return columns[columnOrdinal - 1];
        }

        /// <summary>
        /// change column name
        /// </summary>
        /// <param name="email">email of the owner of the board/param>
        /// <param name="columnOrdinal">column ordinal of the column we wish to change name</param>
        /// <param name="newName">the new column name</param>
        public void setName(int columnOrdinal, string newName)
        {
            foreach (var col in columns.Values)
            {
                if (col.getColumnName().Equals(newName))
                    throw new Exception("column name already exists in board");
            }
            if (columnOrdinal < 0 || columnOrdinal >= columns.Count)
                throw new Exception("illegal column index");
            columns[columnOrdinal].name = newName;
        }

        public void sortBoard()
        {
            //Dictionary<int, Column> newColumns = columns;
            foreach (var col in columns.Values)
            {
                col.sortByDueDate();
            }
            //return new Board(userEmail, newColumns);
        }

        internal bool deleteData()
        {
            return dBoard.Delete();
        }
        

        public int getNumOfColumns()
        {
            return columns.Count;
        }

        public int getMaxIdOfColumns()
        {
            return getNumOfColumns()- 1;
        }
    }
}