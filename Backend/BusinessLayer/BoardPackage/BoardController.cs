using IntroSE.Kanban.Backend.DataAccessLayer;
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
    /// representing the Board object
    /// </summary>
    class BoardController
    {
        private Dictionary<string, Dictionary<string, List<Board>>> boards;
        //private int id;
        private BoardDalController boardDalController;


        /// <summary>
        /// Constructor of a Board
        /// </summary>
        public BoardController()
        {
            //mapping Boards by an emails
            boards = new Dictionary<string, Dictionary<string, List<Board>>>();
            boardDalController = new BoardDalController();
            //id = boardDalController.GetMaxBoardId() + 1;
        }


        /// <summary>
        /// Getter of the Dictionary
        /// </summary>
        /// <returns>return Dictionary(value=Dictionary of boards by board's name, key=email)</returns>

        public Dictionary<string, Dictionary<string, List<Board>>> getBoards()

        {
            return boards;
        }

        /// <summary>

        /// limit a specific column
        /// </summary>
        /// <param name="userEmail">email of the user that the board belongs to him</param>
        /// <param name="boardName">the name of the board</param>
        /// <param name="column">column name(backlog,in progress,done)</param>
        /// <param name="limit">new limit</param>
        /// <exception cref="System.Exception">Thrown when board or email is not found</exception>
        public void LimitColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int limit)

        {
            try
            {

                if (!boards.ContainsKey(userEmail))
                {
                    throw new Exception("user email not found");
                }
                if (!boards.ContainsKey(creatorEmail))
                {
                    throw new Exception("creator email not found - means that the requested board does not exist either");
                }
                if (!boards[userEmail].ContainsKey(boardName))
                {
                    throw new Exception("board not found");
                }
                if (!boards[creatorEmail].ContainsKey(boardName))
                {
                    throw new Exception("board name not found in creator user - means that the requested board does not exist either");
                }
                if (limit < -1)
                {
                    throw new Exception("limit must be at least -1");
                }


                Board cbord = findBoardByCreator(creatorEmail, boardName);
                if (cbord == null)
                {
                    throw new Exception("The creator user does not create such a board - means that the requested board does not exist either ");
                }
                //string columnName = null;
                if (columnOrdinal < 0 || columnOrdinal > cbord.getMaxIdOfColumns())
                {
                    throw new Exception("invalid column number");
                }
                /*
                if (columnOrdinal == 0)
                {
                    columnName = "backlog";
                }
                if (columnOrdinal == 1)
                {
                    columnName = "in progress";
                }
                if (columnOrdinal == 2)
                {
                    columnName = "done";
                }*/

                if (limit < cbord.getColumn(columnOrdinal).getTasks().Count && limit!=-1)
                {
                    throw new Exception("The requested new limit is smaller than the number of tasks in the column");
                }


                cbord.setLimit(columnOrdinal, limit);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>

        /// Get the limit of a specific column
        /// </summary>
        /// <param name="userEmail">The email of the user</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnName">The name of the column</param>
        /// <returns>The limit of this column</returns>
        public int GetColumnLimit(string userEmail, string creatorEmail, string boardName, int columnOrdinal)

        {

            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("email not found");
            }
            if (!boards.ContainsKey(creatorEmail))
            {
                throw new Exception("creator email not found - means that the requested board does not exist either");
            }
            if (!boards[userEmail].ContainsKey(boardName))
            {
                throw new Exception("board not found");
            }
            if (!boards[creatorEmail].ContainsKey(boardName))
            {
                throw new Exception("board name not found in creator user - means that the requested board does not exist either");
            }
            try
            {
                Board cbord = findBoardByCreator(creatorEmail, boardName);
                if (cbord == null)
                {
                    throw new Exception("The creator user does not create such a board - means that the requested board does not exist either ");
                }
                
                if (columnOrdinal < 0 || columnOrdinal > cbord.getMaxIdOfColumns())
                {
                    throw new Exception("invalid column number");
                }

                string columnName = cbord.getColumn(columnOrdinal).getColumnName();

                /*
                if (columnOrdinal == 0)
                {
                    columnName = "backlog";
                }
                if (columnOrdinal == 1)
                {
                    columnName = "in progress";
                }
                if (columnOrdinal == 2)
                {
                    columnName = "done";
                }*/

                int limit = cbord.GetColumnLimit(columnName);
                return limit;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// Get the name of the column
        /// </summary>
        /// <param name="userEmail">The email of the user</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnName">The name of the column</param>
        /// <returns>The name of this column</returns>
        /// <exception cref="System.Exception">Thrown when board or email is not found</exception>
        public string GetColumnName(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("email not found");
            }
            if (!boards[userEmail].ContainsKey(boardName))
            {
                throw new Exception("board not found");
            }
            if (!boards.ContainsKey(creatorEmail))
            {
                throw new Exception("creator email not found - means that the requested board does not exist either");
            }
            if (!boards[creatorEmail].ContainsKey(boardName))
            {
                throw new Exception("board name not found in creator user - means that the requested board does not exist either");
            }
            try
            {
                Board cbord = findBoardByCreator(creatorEmail, boardName);
                if (cbord == null)
                {
                    throw new Exception("The creator user does not create such a board - means that the requested board does not exist either ");
                }
                //string columnName = null;
                if (columnOrdinal < 0 || columnOrdinal > cbord.getMaxIdOfColumns())
                {
                    throw new Exception("invalid column number");
                }
                /*
                if (columnOrdinal == 0)
                {
                    columnName = "backlog";
                }
                if (columnOrdinal == 1)
                {
                    columnName = "in progress";
                }
                if (columnOrdinal == 2)
                {
                    columnName = "done";
                }*/

                string colname = cbord.GetColumnName(columnOrdinal);
                return colname;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        /// <summary>

        /// Add task to specific board of user
        /// </summary>
        /// <param name="userEmail">The email of the user</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="title">The title of the task</param>
        /// <param name="description">The description of the task</param>
        /// <param name="dueDate">The due date of the task</param>
        /// <returns>The added task</returns>
        /// <exception cref="System.Exception">Thrown when board/email is not found</exception>
        /// <exception cref="System.Exception">Thrown if the title is null or her size is not 0<size<50</exception>
        /// <exception cref="System.Exception">Thrown when the description is longer then 300</exception>
        /// <exception cref="System.Exception">Thrown when dueDate<=current date</exception>
        public Task AddTask(string userEmail, string creatorEmail, string boardName, string title, string description, DateTime dueDate)

        {
            try
            {
                if (!boards.ContainsKey(userEmail))
                {
                    throw new Exception("email not found");
                }
                if (!boards[userEmail].ContainsKey(boardName))
                {
                    throw new Exception("board not found");
                }
                if (!boards.ContainsKey(creatorEmail))
                {
                    throw new Exception("creator email not found - means that the requested board does not exist either");
                }
                if (!boards[creatorEmail].ContainsKey(boardName))
                {
                    throw new Exception("board name not found in creator user - means that the requested board does not exist either");
                }
                if (title == null || title.Length == 0 || title.Length > 50)
                {
                    throw new Exception("Invalid title(max. 50 charactres, not empty)");
                }

                if (description != null)
                {
                    if (description.Length > 300)
                    {
                        throw new Exception("Invalid description(max. 300 charactres)");
                    }
                }

                if (dueDate == null || dueDate <= DateTime.Now)
                {
                    throw new Exception("Invalid due date");
                }

                Board cbord = findBoardByCreator(creatorEmail, boardName);
                if (cbord == null)
                {
                    throw new Exception("The creator user does not create such a board - means that the requested board does not exist either ");
                }
                Task task = null;
                if (cbord.Columns[0].IsNotFull())
                {
                    task = cbord.addTask(new Task(cbord.taskIndex, cbord.getUserEmail(), cbord.getName(), DateTime.Now, title, description, dueDate, userEmail));

                    BoardDOB dobBoard = new BoardDOB(cbord.getUserEmail(), cbord.getName(), cbord.taskIndex);
                    dobBoard.Update(dobBoard.TaskIndexColumnName, cbord.taskIndex);
                }
                else
                    throw new Exception("Column Limit Has Been Reached");
                return task;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>

        /// Update the duedate of a specific task
        /// </summary>
        /// <param name="userEmail">The email of the user</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="column">The name of the column</param>
        /// <param name="taskId">The id of the task</param>
        /// <param name="DueDate">The new due date</param>
        /// <exception cref="System.Exception">Thrown when board/email/task is not found</exception>
        /// <exception cref="System.Exception">Thrown when dueDate<=current date</exception>
        public void UpdateTaskDueDate(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, DateTime DueDate)

        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("email not found");
            }
            if (!boards[userEmail].ContainsKey(boardName))
            {
                throw new Exception("board not found");
            }
            if (!boards.ContainsKey(creatorEmail))
            {
                throw new Exception("creator email not found - means that the requested board does not exist either");
            }
            if (!boards[creatorEmail].ContainsKey(boardName))
            {
                throw new Exception("board name not found in creator user - means that the requested board does not exist either");
            }

            Board cbord = findBoardByCreator(creatorEmail, boardName);
            if (cbord == null)
            {
                throw new Exception("The creator user does not create such a board - means that the requested board does not exist either ");
            }
            //string columnName = null;
            if (columnOrdinal < 0 || columnOrdinal > cbord.getMaxIdOfColumns())
            {
                throw new Exception("invalid column number");
            }

            if (columnOrdinal == cbord.getMaxIdOfColumns())
            {
                throw new Exception("cant change task because its in the done column");
            }

            /*
            if (columnOrdinal == 2)
            {
                throw new Exception("cant change task because its in the done column");
            }
            if (columnOrdinal == 0)
            {
                columnName = "backlog";
            }
            if (columnOrdinal == 1)
            {
                columnName = "in progress";
            }*/

            if (!cbord.getColumn(columnOrdinal).getTasks().ContainsKey(taskId))
            {
                throw new Exception("task not found");
            }
            if (!(cbord.getColumn(columnOrdinal).getTasks()[taskId].emailAssignee == userEmail))
            {
                throw new Exception("only the assigned can change task details");
            }
            if (DueDate == null || DueDate <= DateTime.Now)
            {
                throw new Exception("due date not valid");
            }
            cbord.getColumn(columnOrdinal).getTasks()[taskId].setDuedate(DueDate);
        }

        /// <summary>

        /// Update the title of a specific task
        /// </summary>
        /// <param name="userEmail">The email of the user</param>W
        /// <param name="boardName">The name of the board</param>
        /// <param name="column">The name of the column</param>
        /// <param name="taskId">The name of the column</param>
        /// <param name="title">The new title</param>
        /// <exception cref="System.Exception">Thrown when board/email/task is not found</exception>
        /// <exception cref="System.Exception">Thrown if the title is null or her size is not 0<size<50</exception>
        public void UpdateTaskTitle(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string title)

        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("email not found");
            }
            if (!boards[userEmail].ContainsKey(boardName))
            {
                throw new Exception("board not found");
            }
            if (!boards.ContainsKey(creatorEmail))
            {
                throw new Exception("creator email not found - means that the requested board does not exist either");
            }
            if (!boards[creatorEmail].ContainsKey(boardName))
            {
                throw new Exception("board name not found in creator user - means that the requested board does not exist either");
            }

            Board cbord = findBoardByCreator(creatorEmail, boardName);
            if (cbord == null)
            {
                throw new Exception("The creator user does not create such a board - means that the requested board does not exist either ");
            }
            //string columnName = null;

            if (columnOrdinal < 0 || columnOrdinal > cbord.getMaxIdOfColumns())
            {
                throw new Exception("invalid column number");
            }

            if (columnOrdinal == cbord.getMaxIdOfColumns())
            {
                throw new Exception("cant change task because its in the done column");
            }

            /*if (columnOrdinal == 2)
            {
                throw new Exception("cant change task because its in the done column");
            }
            if (columnOrdinal == 0)
            {
                columnName = "backlog";
            }
            if (columnOrdinal == 1)
            {
                columnName = "in progress";
            }*/


            if (!cbord.getColumn(columnOrdinal).getTasks().ContainsKey(taskId))
            {
                throw new Exception("task not found");
            }
            if (!(cbord.getColumn(columnOrdinal).getTasks()[taskId].emailAssignee == userEmail))
            {
                throw new Exception("only the assigned can change task details");
            }
            if (title == null || title.Length == 0 || title.Length > 50)
            {
                throw new Exception("title max. 50 characters, not empty");
            }
            cbord.getColumn(columnOrdinal).getTasks()[taskId].setTitle(title);
        }

        /// <summary>

        /// Update the description of a specific task
        /// </summary>
        /// <param name="userEmail">The email of the user</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="column">The name of the column</param>
        /// <param name="taskId">The id of the task</param>
        /// <param name="description">The new description</param>
        /// <exception cref="System.Exception">Thrown when board/email/task is not found</exception>
        /// <exception cref="System.Exception">Thrown when the description is longer then 300</exception>
        public void UpdateTaskDescription(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string description)

        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("email not found");
            }
            if (!boards[userEmail].ContainsKey(boardName))
            {
                throw new Exception("board not found");
            }
            if (!boards.ContainsKey(creatorEmail))
            {
                throw new Exception("creator email not found - means that the requested board does not exist either");
            }
            if (!boards[creatorEmail].ContainsKey(boardName))
            {
                throw new Exception("board name not found in creator user - means that the requested board does not exist either");
            }

            Board cbord = findBoardByCreator(creatorEmail, boardName);
            if (cbord == null)
            {
                throw new Exception("The creator user does not create such a board - means that the requested board does not exist either ");
            }

            if (columnOrdinal < 0 || columnOrdinal > cbord.getMaxIdOfColumns())
            {
                throw new Exception("invalid column number");
            }

            if (columnOrdinal == cbord.getMaxIdOfColumns())
            {
                throw new Exception("cant change task because its in the done column");
            }

            /*
            string columnName = null;
            if (columnOrdinal == 0)
            {
                columnName = "backlog";
            }
            if (columnOrdinal == 1)
            {
                columnName = "in progress";
            }*/

            if (!cbord.getColumn(columnOrdinal).getTasks().ContainsKey(taskId))
            {
                throw new Exception("task not found");
            }
            if (!(cbord.getColumn(columnOrdinal).getTasks()[taskId].emailAssignee == userEmail))
            {
                throw new Exception("only the assigned can change task details");
            }
            if (description != null && description.Length > 300)
            {
                throw new Exception("description max. 300 characters");
            }
            cbord.getColumn(columnOrdinal).getTasks()[taskId].setDescription(description);
        }

        /// <summary>
        /// Advance a task, backlog-->in progress, in progress-->done and task in done column can not be advanced.
        /// </summary>
        /// <param name="userEmail">The email of the user</param>

        /// <param name="boardName">The name of the board</param>
        /// <param name="column">The name of the column</param>
        /// <param name="taskId">The id of the task</param>
        /// <exception cref="System.Exception">Thrown when board/email/task is not found</exception>
        /// <exception cref="System.Exception">Thrown when tring to advance task in "done" column</exception>

        public void AdvanceTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId)

        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("email not found");
            }
            if (!boards[userEmail].ContainsKey(boardName))
            {
                throw new Exception("board not found");
            }
            if (!boards.ContainsKey(creatorEmail))
            {
                throw new Exception("creator email not found - means that the requested board does not exist either");
            }
            if (!boards[creatorEmail].ContainsKey(boardName))
            {
                throw new Exception("board name not found in creator user - means that the requested board does not exist either");
            }

            Board cbord = findBoardByCreator(creatorEmail, boardName);
            if (cbord == null)
            {
                throw new Exception("The creator user does not create such a board - means that the requested board does not exist either ");
            }
            //string columnName = null;
            if (columnOrdinal < 0 || columnOrdinal > cbord.getMaxIdOfColumns())
            {
                throw new Exception("invalid column number");
            }


            if (columnOrdinal == cbord.getMaxIdOfColumns())
            {
                throw new Exception("cannot be advanced done column tasks");
            }

            //columnName = cbord.Columns[columnOrdinal].getColumnName();
            /*
            if (columnOrdinal == 0)
            {
                columnName = "backlog";
            }
            if (columnOrdinal == 1)
            {
                columnName = "in progress";
            }*/

            if (!cbord.getColumn(columnOrdinal).getTasks().ContainsKey(taskId))
            {
                throw new Exception("task not found");
            }
            if (!(cbord.getColumn(columnOrdinal).getTasks()[taskId].emailAssignee == userEmail))
            {
                throw new Exception("only the assigned can change task details");
            }


            if (!cbord.Columns[columnOrdinal+1].IsNotFull())
            {
                throw new Exception("The next column is already reached it's limit");
            }

            /*
            if (columnOrdinal == 0)
            {
                if(!cbord.getColumn("in progress").IsNotFull())
                {
                    throw new Exception("The next column is already reached it's limit");

                }
            }
            if (columnOrdinal == 1)
            {
                if (!cbord.getColumn("done").IsNotFull())
                {

                    throw new Exception("The next column is already reached it's limit");
                }
            }*/


            try
            {
                cbord.advanceTask(columnOrdinal, cbord.getColumn(columnOrdinal).getTasks()[taskId]);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Get a list that contains all the tasks in this column
        /// </summary>
        /// <param name="userEmail">the email of the user</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnName">The name of the column</param>
        /// <returns>List of all the tasks in this column</returns>
        public List<Task> GetColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal)

        {
            List<Task> t = new List<Task>();
            try
            {
                if (!boards.ContainsKey(userEmail))
                {
                    throw new Exception("user email not found");
                }
                if (!boards.ContainsKey(creatorEmail))
                {
                    throw new Exception("creator email not found - means that the requested board does not exist either");
                }
                if (!boards[userEmail].ContainsKey(boardName))
                {
                    throw new Exception("board not found");
                }
                if (!boards[creatorEmail].ContainsKey(boardName))
                {
                    throw new Exception("board name not found in creator user - means that the requested board does not exist either");
                }
                Board cbord = findBoardByCreator(creatorEmail, boardName);
                if (cbord == null)
                {
                    throw new Exception("The creator user does not create such a board - means that the requested board does not exist either ");
                }
                string columnName = null;
                if (columnOrdinal < 0 || columnOrdinal > cbord.getMaxIdOfColumns())
                {
                    throw new Exception("invalid column number");
                }
                /*
                if (columnOrdinal == 0)
                {
                    columnName = "backlog";
                }
                if (columnOrdinal == 1)
                {
                    columnName = "in progress";
                }
                if (columnOrdinal == 2)
                {
                    columnName = "done";
                }*/

                foreach (var task in cbord.getColumn(columnOrdinal).getTasks().Values)
                {
                    //Task temp = new Task(task.getId(), task.getCreationTime(), task.getTitle(), task.getDescription(), task.getDueDate());
                    t.Add(task);
                }
                return t;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>

        /// Add new board
        /// </summary>
        /// <param name="email">the email of the user who tring to add the board</param>
        /// <param name="boardName">the name of the new board</param>
        /// <exception cref="System.Exception">Thrown when this user all ready hold a board with this name</exception>
        public void AddBoard(string email, string boardName)

        {
            try
            {
                List<Board> b = new List<Board>();
                if (boards.ContainsKey(email))
                {
                    if (boards[email].ContainsKey(boardName))
                    {
                        Board boardFound = findBoardByCreator(email, boardName);
                        if (boardFound != null)
                        {
                            if (boards[email][boardName].Contains(boardFound))
                                throw new Exception("user cannot create boards with the same name");
                        }
                        else
                            boards[email][boardName].Add(new Board(boardName, email));
                        throw new Exception("user cannot create boards with the same name");
                    }
                    else
                    {
                        b.Add(new Board(boardName, email));
                        this.boards[email].Add(boardName, b);
                        //boards[email].Add(boardName, new Board(id, boardName, email));
                    }
                }
                else
                {
                    b.Add(new Board(boardName, email));
                    Dictionary<string, List<Board>> newBoards = new Dictionary<string, List<Board>>();
                    newBoards.Add(boardName, b);
                    boards.Add(email, newBoards);
                }
                //id++;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void AddBoardForLoad(BoardDOB dBoard)
        {
            try { 
                List<Board> b = new List<Board>();
                List<ColumnDOB> columnDOBs = boardDalController.LoadColumns(dBoard.userEmail, dBoard.name);


                Board newBoard = new Board(dBoard, columnDOBs);
                newBoard.taskIndex = dBoard.taskIndex;
                foreach (TaskDOB dTask in boardDalController.LoadTasks(dBoard.userEmail, dBoard.name))
                {
                    try
                    {
                        Task t = new Task(dTask);
                        newBoard.addTaskForLoad(t);
                    }
                    catch { 
                }
                }
                b.Add(newBoard);
                if (boards.ContainsKey(dBoard.userEmail))

                {
                    if (boards[dBoard.userEmail].ContainsKey(dBoard.name))
                    {
                    
                        Board boardFound = findBoardByCreator(dBoard.userEmail, dBoard.name);

                        if (boardFound != null)
                        {
                            if (boards[dBoard.userEmail][dBoard.name].Contains(boardFound))
                                throw new Exception("user cannot create boards with the same name");
                        }
                        else
                            boards[dBoard.userEmail][dBoard.name].Add(newBoard);
                        
                    }
                    else
                    {
                        this.boards[dBoard.userEmail].Add(dBoard.name, b);
                    }
                }
                else
                {
                    Dictionary<string, List<Board>> newBoards = new Dictionary<string, List<Board>>();
                    newBoards.Add(dBoard.name, b);
                    boards.Add(dBoard.userEmail, newBoards);
                }
            }
            catch(Exception e)
            {
                throw e;
            }

        }

        public void LoadAll()
        {
            try
            {
                foreach (BoardDOB dBoard in boardDalController.LoadBoards())
                {

                    AddBoardForLoad(dBoard);

                    foreach (BoardMemberDOB member in boardDalController.LoadBoardMembers(dBoard.userEmail, dBoard.name))
                    {
                        JoinBoardWithoutPersistance(member.user, dBoard.userEmail, dBoard.name);

                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            //id = boardDalController.GetMaxBoardId()+1;
        }

        public void DeleteAll()
        {
            boardDalController.DeleteAll();
        }

        /// <summary>
        /// Adds a board created by another user to the logged-in user. 
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the new board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public void JoinBoard(string userEmail, string creatorEmail, string boardName)
        {
            try
            {

                if (!boards.ContainsKey(creatorEmail))
                {
                    throw new Exception("creator email not found - means that the requested board does not exist either");
                }
                if (!boards[creatorEmail].ContainsKey(boardName))
                {
                    throw new Exception("board name not found in creator user - means that the requested board does not exist either");
                }

                Board cboard = findBoardByCreator(creatorEmail, boardName);
                if (cboard == null)
                {
                    throw new Exception("The creator user does not create such a board - means that the requested board does not exist either");
                }


                if (boards.ContainsKey(userEmail))
                {
                    Dictionary<string, List<Board>> userboards = boards[userEmail];
                    if (!userboards.ContainsKey(boardName))
                    {
                        //the user does not have a board with the name of the board that he want to join- we creat new list for new key
                        List<Board> newBoardsName = new List<Board>();
                        newBoardsName.Add(cboard);
                        userboards.Add(boardName, newBoardsName);
                    }
                    else
                    {
                        //TODO to check that im not trying to join board i'm already joined to + to add boardcreator to boardmembers
                        List<Board> ub = userboards[boardName];
                        if (userboards[boardName].Contains(cboard))
                        {
                            throw new Exception("This user is already joined this board");
                        }
                        List<Board> newBoardsName = userboards[boardName];
                        newBoardsName.Add(cboard);
                    }
                }
                else
                {
                    List<Board> b = new List<Board>();
                    Dictionary<string, List<Board>> newBoards = new Dictionary<string, List<Board>>();
                    b.Add(cboard);
                    newBoards.Add(boardName, b);
                    boards.Add(userEmail, newBoards);
                }
                BoardMemberDOB bmd = new BoardMemberDOB(userEmail, creatorEmail, boardName);
                bmd.Insert();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void JoinBoardWithoutPersistance(string userEmail, string creatorEmail, string boardName)
        {
            try
            {

                if (!boards.ContainsKey(creatorEmail))
                {
                    throw new Exception("creator email not found - means that the requested board does not exist either");
                }
                if (!boards[creatorEmail].ContainsKey(boardName))
                {
                    throw new Exception("board name not found in creator user - means that the requested board does not exist either");
                }

                Board cboard = findBoardByCreator(creatorEmail, boardName);
                if (cboard == null)
                {
                    throw new Exception("The creator user does not create such a board - means that the requested board does not exist either");
                }

                if (boards.ContainsKey(userEmail))
                {
                    Dictionary<string, List<Board>> userboards = boards[userEmail];
                    if (!userboards.ContainsKey(boardName))
                    {
                        //the user does not have a board with the name of the board that he want to join- we creat new list for new key
                        List<Board> newBoardsName = new List<Board>();
                        newBoardsName.Add(cboard);
                        userboards.Add(boardName, newBoardsName);
                    }
                    else
                    {
                        List<Board> newBoardsName = userboards[boardName];
                        newBoardsName.Add(cboard);
                    }
                }

                else
                {
                    List<Board> b = new List<Board>();
                    Dictionary<string, List<Board>> newBoards = new Dictionary<string, List<Board>>();
                    b.Add(cboard);
                    newBoards.Add(boardName, b);
                    boards.Add(userEmail, newBoards);
                }


            }
            catch
            {

            }
        }


        /// <summary>
        /// Removing board from the system
        /// Remove a specific board
        /// </summary>
        /// <param name="userEmail">The email of the user</param>
        /// <param name="boardName">The name of the board</param>
        /// <exception cref="System.Exception">Thrown when board/email is not found</exception>
        public void RemoveBoard(string userEmail, string creatorEmail, string boardName)

        {
            if (!boards.ContainsKey(userEmail))
            {
                throw new Exception("email not found");
            }
            if (!boards[userEmail].ContainsKey(boardName))
            {
                throw new Exception("board not found");
            }
            if (!(userEmail.Equals(creatorEmail)))
            {
                throw new Exception("only the creator can remove a board");
            }

            Board removBoard = null;
            foreach (var b in boards[creatorEmail][boardName])
            {
                if (b.getUserEmail().Equals(creatorEmail))
                {
                    removBoard = b;
                    break;
                }
            }

            if (removBoard == null)
            {
                throw new Exception("The creator user does not create such a board - means that the requested board does not exist either");
            }
            else
            {
                //removBoard.resetTaskAssigne();
                removBoard.deleteTasks();

                foreach (var user in boards.Values)
                {
                    foreach (var lst in user.Values)
                    {
                        if (lst.Contains(removBoard))
                        {
                            lst.Remove(removBoard);
                        }
                    }
                }
                removBoard.deleteData();
            }

        }

        /// <summary>
        /// Give a list of all the task that in column -in progress- from all of the board of a user
        /// </summary>
        /// <param name="userEmail">The email of the user</param>
        /// <returns>list of task from all boards that in progress</returns>
        public List<Task> InProgressTasks(string userEmail)
        {
            try
            {
                if (!boards.ContainsKey(userEmail))
                {
                    throw new Exception("email not found");
                }
                List<Task> inProgTask = new List<Task>();
                foreach (List<Board> lst in boards[userEmail].Values)
                {
                    foreach (Board b in lst)
                    {
                        List<Task> boardTasks = b.InProgressTasksByAssignee(userEmail);
                        inProgTask = inProgTask.Concat(boardTasks).ToList();
                    }
                }
                return inProgTask;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>

        /// Returns the list of board of a user. The user must be logged-in. The function returns all the board names the user created or joined.
        /// </summary>
        /// <param name="userEmail">The email of the user. Must be logged-in.</param>
        /// <returns>A response object with a value set to the board, instead the response should contain a error message in case of an error</returns>
        public List<String> GetBoardNames(string userEmail)

        {
            try
            {
                List<String> boardsName = new List<String>();
                if (!boards.ContainsKey(userEmail))
                {
                    throw new Exception("email not found");
                }
                foreach (var name in boards[userEmail].Keys.ToList())
                {

                    int numBoardName = boards[userEmail][name].Count;
                    boardsName.AddRange(Enumerable.Repeat(name, numBoardName));

                }
                return boardsName;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Assigns a task to a user
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>        
        /// <param name="emailAssignee">Email of the user to assign to task to</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public void AssignTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string emailAssignee)

        {
            try
            {
                if (!boards.ContainsKey(userEmail))
                {
                    throw new Exception("user email not found");
                }
                if (!boards.ContainsKey(creatorEmail))
                {
                    throw new Exception("creator email not found - means that the requested board does not exist either");
                }
                if (!boards[userEmail].ContainsKey(boardName))
                {
                    throw new Exception("board not found");
                }
                if (!boards[creatorEmail].ContainsKey(boardName))
                {
                    throw new Exception("board name not found in creator user - means that the requested board does not exist either");
                }

                Board cbord = findBoardByCreator(creatorEmail, boardName);
                if (cbord == null)
                {
                    throw new Exception("The creator user does not create such a board - means that the requested board does not exist either");
                }
                if (columnOrdinal < 0 || columnOrdinal > cbord.getMaxIdOfColumns())
                {
                    throw new Exception("invalid column number");
                }

                /*
                string columnName = cbord.getColumn(columnOrdinal).getColumnName();
                
                if (columnOrdinal == 0)
                {
                    columnName = "backlog";
                }
                if (columnOrdinal == 1)
                {
                    columnName = "in progress";
                }
                if (columnOrdinal == 2)
                {
                    columnName = "done";
                }*/

                if (!cbord.getColumn(columnOrdinal).getTasks().ContainsKey(taskId))
                {
                    throw new Exception("task not found");
                }
                if (!(cbord.getColumn(columnOrdinal).getTask(taskId).emailAssignee == userEmail))
                {
                    throw new Exception("only the assigned can change task details");
                }
                if (boardDalController.isBoardMember(emailAssignee, creatorEmail, boardName))
                cbord.getColumn(columnOrdinal).getTasks()[taskId].emailAssignee = emailAssignee;
                else throw new Exception("Cannot asign this user, he is not a board member");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// return the board by creator email
        /// </summary>
        /// <param name="creatorEmail">The email of the creator</param>
        /// <param name="boardName">the name of the board</param>
        /// <returns>return the board by creator email</returns>
        public Board findBoardByCreator(string creatorEmail, string boardName)
        {
            try

            {
                //list of creator 
                List<Board> creatorBoardsByName = boards[creatorEmail][boardName];
                Board cboard = null;

                foreach (var b in creatorBoardsByName)
                {
                    if (b.getUserEmail() != creatorEmail)
                    {
                        continue;
                    }
                    else
                    {
                        cboard = b;
                    }
                }
                return cboard;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        /// <summary>
        /// Removing Column from board
        /// </summary>
        /// <param name="userEmail">The email of the requesting user </param>
        /// <param name="creatorEmail">The email of the board creator </param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">columnOrdinal</param>
        public void RemoveColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            try 
            {
                if (!boards.ContainsKey(userEmail))
                {
                    throw new Exception("user email not found");
                }
                if (!boards.ContainsKey(creatorEmail))
                {
                    throw new Exception("creator email not found - means that the requested board does not exist either");
                }
                if (!boards[userEmail].ContainsKey(boardName))
                {
                    throw new Exception("board not found");
                }
                if (!boards[creatorEmail].ContainsKey(boardName))
                {
                    throw new Exception("board name not found in creator user - means that the requested board does not exist either");
                }

                Board cbord = findBoardByCreator(creatorEmail, boardName);
                if (cbord == null)
                {
                    throw new Exception("The creator user does not create such a board - means that the requested board does not exist either");
                }
                if (columnOrdinal < 0 || columnOrdinal > cbord.getMaxIdOfColumns())
                {
                    throw new Exception("invalid column number");
                }
                cbord.removeColumn(columnOrdinal);
            }
            catch(Exception e)
            {
                throw e;

            }
        }

        /// <summary>
        /// Adding new column to the board
        /// </summary>
        /// <param name="userEmail">The email of the requesting user </param>
        /// <param name="creatorEmail">The email of the board creator </param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">columnOrdinal</param>
        /// <param name="newColumnName">New column's name</param>
        public void AddColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, string newColumnName)
        {
            try
            {
                if (!boards.ContainsKey(userEmail))
                {
                    throw new Exception("user email not found");
                }
                if (!boards.ContainsKey(creatorEmail))
                {
                    throw new Exception("creator email not found - means that the requested board does not exist either");
                }
                if (!boards[userEmail].ContainsKey(boardName))
                {
                    throw new Exception("board not found");
                }
                if (!boards[creatorEmail].ContainsKey(boardName))
                {
                    throw new Exception("board name not found in creator user - means that the requested board does not exist either");
                }

                Board cbord = findBoardByCreator(creatorEmail, boardName);
                if (cbord == null)
                {
                    throw new Exception("The creator user does not create such a board - means that the requested board does not exist either");
                }
                if (columnOrdinal < 0 || columnOrdinal > cbord.getMaxIdOfColumns())
                {
                    throw new Exception("invalid column number");
                }
                cbord.addColumn(columnOrdinal, newColumnName);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Moving the column to the new desired location
        /// </summary>
        /// <param name="userEmail">The email of the requesting user </param>
        /// <param name="creatorEmail">The email of the board creator </param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">columnOrdinal</param>
        /// <param name="shiftSize">shift size to move the board</param>
        public void MoveColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int shiftSize)
        {
            try
            {

                if (!boards.ContainsKey(userEmail))
                {
                    throw new Exception("user email not found");
                }
                if (!boards.ContainsKey(creatorEmail))
                {
                    throw new Exception("creator email not found - means that the requested board does not exist either");
                }
                if (!boards[userEmail].ContainsKey(boardName))
                {
                    throw new Exception("board not found");
                }
                if (!boards[creatorEmail].ContainsKey(boardName))
                {
                    throw new Exception("board name not found in creator user - means that the requested board does not exist either");
                }

                Board cbord = findBoardByCreator(creatorEmail, boardName);
                if (cbord == null)
                {
                    throw new Exception("The creator user does not create such a board - means that the requested board does not exist either");
                }

                if (columnOrdinal < 0 || columnOrdinal > cbord.getMaxIdOfColumns())
                {
                    throw new Exception("invalid column number");
                }

                if (cbord.getColumn(columnOrdinal).getTasks().Count != 0)
                {
                    throw new Exception("Only empty columns can be moved.");
                }

                if (columnOrdinal+ shiftSize<0 || columnOrdinal + shiftSize > cbord.getMaxIdOfColumns())
                {
                    throw new Exception("Invalid shiftsize, new requested index is out of bounds");
                }

                if (shiftSize > 0)
                {
                    for (int x = 1; x <= Math.Abs(shiftSize); x++)

                    {

                        cbord.MoveColumnRight(columnOrdinal);

                    }
                }

                if (shiftSize < 0)
                {
                    for (int x = 1; x <= Math.Abs(shiftSize); x++)

                    {

                            cbord.MoveColumnLeft(columnOrdinal);

                    }
                }
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Renames a specific column
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column location. The first column location is identified by 0, the location increases by 1 for each column</param>
        /// <param name="newColumnName">The new column name</param>        
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public void RenameColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, string newColumnName)
        {
            try
            {
                if (!boards.ContainsKey(userEmail))
                {
                    throw new Exception("user email not found");
                }
                if (!boards.ContainsKey(creatorEmail))
                {
                    throw new Exception("creator email not found - means that the requested board does not exist either");
                }
                if (!boards[userEmail].ContainsKey(boardName))
                {
                    throw new Exception("board not found");
                }
                if (!boards[creatorEmail].ContainsKey(boardName))
                {
                    throw new Exception("board name not found in creator user - means that the requested board does not exist either");
                }

                Board cbord = findBoardByCreator(creatorEmail, boardName);
                if (cbord == null)
                {
                    throw new Exception("The creator user does not create such a board - means that the requested board does not exist either");
                }
                if (columnOrdinal < 0 || columnOrdinal > cbord.getMaxIdOfColumns())
                {
                    throw new Exception("invalid column number");
                }
                cbord.setName(columnOrdinal, newColumnName);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Board> getAllBoards()//list of the boards I created 
        {
            //private Dictionary<string, Dictionary<string, List<Board>>> boards;
            Dictionary<string, Dictionary<string, List<Board>>> boards = getBoards();
            List<Board> lst = new List<Board>();
            Dictionary<string, List<string>> emailsNames = new Dictionary<string, List<string>>();
            foreach(Dictionary<string, List<Board>> nameList in boards.Values)
            {
                foreach (List<Board> boardList in nameList.Values)
                {
                    foreach (Board b in boardList)
                    {
                        if (emailsNames.ContainsKey(b.getUserEmail()))
                        {
                            if (emailsNames[b.getUserEmail()].Contains(b.getName()))
                            {

                            }
                            lst.Add(b);
                            emailsNames[b.getUserEmail()].Add(b.getName());
                        }
                        else
                        {
                            lst.Add(b);
                            emailsNames.Add(b.getUserEmail(), new List<string>());
                        }

                    }
                }
            }
            return lst;
        }



    }

}