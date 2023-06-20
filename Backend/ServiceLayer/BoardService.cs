using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Text;
using IntroSE.Kanban.Backend.BusinessLayer;
using log4net;
using IntroSE.Kanban.Backend.BusinessLayer.BoardPackage;
using IntroSE.Kanban.Backend.ServiceLayer.Objects;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    class boardService
    {
        private readonly BoardController boardController;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public boardService()
        {
            boardController = new BoardController();
            log.Info("starting up! BoardService");
        }



        /// <summary>
        /// Limit the number of tasks in a specific column
        /// </summary>
        /// <param name="userEmail">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="limit">The new limit value. A value of -1 indicates no limit.</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>

        public Response LimitColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int limit)

        { 
            try
            {
                boardController.LimitColumn(userEmail, creatorEmail, boardName, columnOrdinal, limit);
                log.Info("column successfully limited");
            }
            catch(Exception e)
            {
                log.Error("LimitColumn- " + e.Message);
                return new Response(e.Message);
            }
            return new Response();
        }

        /// <summary>
        /// Get the limit of a specific column
        /// </summary>
        /// <param name="userEmail">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>The limit of the column.</returns>

        public Response<int> GetColumnLimit(string userEmail, string creatorEmail, string boardName, int columnOrdinal)

        {
            int limit = -1;
            try
            {
                limit = boardController.GetColumnLimit( userEmail,  creatorEmail,  boardName, columnOrdinal);
                log.Info("GetColumnLimit- SUCCESS");
            }
            catch (Exception e)
            {
                log.Error("GetColumnLimit- " + e.Message);
                return Response<int>.FromError(e.Message);
            }
            return Response<int>.FromValue(limit);
        }

        /// <summary>
        /// Get the name of a specific column
        /// </summary>
        /// <param name="userEmail">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>The name of the column.</returns>

        public Response<string> GetColumnName(string userEmail, string creatorEmail, string boardName, int columnOrdinal)

        {
            string columnName;
            try
            {
                columnName = boardController.GetColumnName(userEmail, creatorEmail, boardName, columnOrdinal);
                log.Info("GetColumnName- SUCCESS");

            }
            catch (Exception e)
            {
                log.Error("GetColumnName- " + e.Message);
                return Response<string>.FromError(e.Message);
            }
            return Response<string>.FromValue(columnName);
        }

        /// <summary>
        /// Add a new task.
        /// </summary>
        /// <param name="userEmail">Email of the user. The user must be logged in.</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        /// <returns>A response object with a value set to the Task, instead the response should contain a error message in case of an error</returns>

        public Response<TaskS> AddTask(string userEmail, string creatorEmail, string boardName, string title, string description, DateTime dueDate)

        {
            TaskS newTask;
            try
            {

                BusinessLayer.BoardPackage.Task t = boardController.AddTask(userEmail, creatorEmail, boardName,  title, description, dueDate);
                newTask = new TaskS(t.id, DateTime.Now, title, description, dueDate, t.emailAssignee);

                log.Info("AddTask - SUCCESS" + "task id - "+t.id+ " successfully added");
            }
            catch (Exception e)
            {
                log.Error("AddTask- " + e.Message);
                return Response<TaskS>.FromError(e.Message);
            }
            return Response<TaskS>.FromValue(newTask);
        }
        /// <summary>
        /// Update the due date of a task
        /// </summary>
        /// <param name="userEmail">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="dueDate">The new due date of the column</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>

        public Response UpdateTaskDueDate(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, DateTime dueDate)

        {
            try
            {
                boardController.UpdateTaskDueDate(userEmail, creatorEmail, boardName, columnOrdinal, taskId, dueDate);
                log.Info("UpdateTaskDueDate - SUCCESS" + "task id - " + taskId + " successfully Updated");
            }
            catch (Exception e)
            {
                log.Error("UpdateTaskDueDate- " + e.Message);
                return new Response(e.Message);
            }
            return new Response();
        }
        /// <summary>
        /// Update task title
        /// </summary>
        /// <param name="userEmail">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="title">New title for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>

        public Response UpdateTaskTitle(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string title)

        {
            try
            {
                boardController.UpdateTaskTitle(userEmail, creatorEmail, boardName, columnOrdinal, taskId, title);
                log.Info("UpdateTaskTitle - SUCCESS" + "task id - " + taskId + " successfully Updated");

            }
            catch (Exception e)
            {
                log.Error("UpdateTaskTitle- " + e.Message);
                return new Response(e.Message);
            }
            return new Response();
        }
        /// <summary>
        /// Update the description of a task
        /// </summary>
        /// <param name="userEmail">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="description">New description for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>

        public Response UpdateTaskDescription(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string description)

        {
            try
            {
                boardController.UpdateTaskDescription(userEmail, creatorEmail, boardName, columnOrdinal, taskId, description);
                log.Info("UpdateTaskDescription - SUCCESS" + "task id - " + taskId + " successfully Updated");
            }
            catch (Exception e)
            {
                log.Error("UpdateTaskDescription- " + e.Message);
                return new Response(e.Message);
            }
            return new Response();
        }
        /// <summary>
        /// Advance a task to the next column
        /// </summary>
        /// <param name="userEmail">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>

        public Response AdvanceTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId)

        {
            try
            {
                boardController.AdvanceTask(userEmail, creatorEmail, boardName, columnOrdinal, taskId);
                log.Info("AdvanceTask - SUCCESS " + "task id - " + taskId + " successfully Updated");
            }
            catch (Exception e)
            {
                log.Error("AdvanceTask- " + e.Message);
                return new Response(e.Message);
            }
            return new Response();
        }
        /// <summary>
        /// Returns a column given it's name
        /// </summary>
        /// <param name="userEmail">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>A response object with a value set to the Column, The response should contain a error message in case of an error</returns>

        public Response<IList<TaskS>> GetColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal)

        {
            string taskInColumnList = "";
            List<BusinessLayer.BoardPackage.Task> tasks = new List<BusinessLayer.BoardPackage.Task>();
            List<TaskS> tasksToAdd = new List<TaskS>();
            try
                {

                tasks = boardController.GetColumn(userEmail, creatorEmail, boardName, columnOrdinal);

                foreach (BusinessLayer.BoardPackage.Task task in tasks)
                    {
                        TaskS temp = new TaskS(task.id, task.getCreationTime(), task.getTitle(), task.getDescription(), task.getDueDate(), task.emailAssignee);
                        tasksToAdd.Add(temp);
                    taskInColumnList += task.id;
                    }
                log.Info("GetColumn - SUCCESS" + " columnOrdinal - " + columnOrdinal +" : "+ taskInColumnList);
            }
                catch (Exception e)
                {
                    log.Error("GetColumn- " + e.Message);
                    return Response<IList<TaskS>>.FromError(e.Message);
                }
                return Response<IList<TaskS>>.FromValue(tasksToAdd);
        }
        /// <summary>
        /// Adds a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the new board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AddBoard(string email, string name)
        {
            try
            {
                boardController.AddBoard(email, name);
                log.Info(" AddBoard - SUCCESS" + ", board successfully added to "+email);
            }
            catch(Exception e)
            {
                log.Error("AddBoard- " + e.Message);
                return new Response(e.Message);
            }
            return new Response();
        }
        /// <summary>
        /// Adds a board created by another user to the logged-in user. 
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the new board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response JoinBoard(string userEmail, string creatorEmail, string boardName)
        {
            try
            {
                boardController.JoinBoard(userEmail, creatorEmail, boardName);
                log.Info(" JoinBoard - SUCCESS" + ", board successfully attached to "+ userEmail);
            }
            catch(Exception e)
            {
                log.Error("JoinBoard- " + e.Message);
                return new Response(e.Message);
            }
            return new Response();
        }

        /// <summary>
        /// Removes a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>

        public Response RemoveBoard(string userEmail, string creatorEmail, string boardName)

        {
            try
            {
                boardController.RemoveBoard(userEmail, creatorEmail, boardName);
                log.Info("RemoveBoard - SUCCESS" + ", board successfully removed for " + userEmail);
            }
            catch (Exception e)
            {
                log.Error("RemoveBoard- " + e.Message);
                return new Response(e.Message);
            }
            return new Response();
        }
        /// <summary>
        /// Returns all the In progress tasks of the user.
        /// </summary>
        /// <param name="userEmail">Email of the user. Must be logged in</param>
        /// <returns>A response object with a value set to the list of tasks, The response should contain a error message in case of an error</returns>
        public Response<IList<TaskS>> InProgressTasks(string userEmail)
        {
            string inProTasks = "";
            IList<TaskS> inProgress;
            IList<TaskS> inProgressToAdd = new List<TaskS>();
            try
            {
                foreach (BusinessLayer.BoardPackage.Task task in boardController.InProgressTasks(userEmail))
                {
                    TaskS temp = new TaskS(task.id, task.getCreationTime(), task.getTitle(), task.getDescription(), task.getDueDate(), task.emailAssignee);
                    inProgressToAdd.Add(temp);
                    inProTasks += task.id;
                }
                inProgress = inProgressToAdd.ToList().AsReadOnly();
                log.Info("InProgressTasks - SUCCESS" + " columnOrdinal - " + 1 + " : " + inProTasks);
            }
            catch (Exception e)
            {
                log.Error("InProgressTasks- " + e.Message);
                return Response<IList<TaskS>>.FromError(e.Message);
            }
            return Response<IList<TaskS>>.FromValue(inProgress);
        }
        /// <summary>
        /// Returns the list of board of a user. The user must be logged-in. The function returns all the board names the user created or joined.
        /// </summary>
        /// <param name="userEmail">The email of the user. Must be logged-in.</param>
        /// <returns>A response object with a value set to the board, instead the response should contain a error message in case of an error</returns>
        public Response<IList<String>> GetBoardNames(string userEmail)
        {
            List<String> boardsName;
            try
            {
                boardsName = boardController.GetBoardNames(userEmail);
                log.Info("GetBoardNames - SUCCESS" + ", get names board of user " + userEmail);
            }
            catch (Exception e)
            {
                return Response<IList<String>>.FromError(e.Message);
            } 
            return Response<IList<String>>.FromValue(boardsName);
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
        public Response AssignTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string emailAssignee)
        {
            try
            {
                boardController.AssignTask(userEmail, creatorEmail, boardName, columnOrdinal, taskId, emailAssignee);

                /*
                if (columnOrdinal < 0 || columnOrdinal > 2)
                    return new Response("invalid column number");
                if (columnOrdinal == 0)
                {
                    boardController.AssignTask(userEmail, creatorEmail, boardName, "backlog", taskId, emailAssignee);
                }
                if (columnOrdinal == 1)
                {
                    boardController.AssignTask(userEmail, creatorEmail, boardName, "in progress", taskId, emailAssignee);
                }
                if (columnOrdinal == 3)
                {
                    boardController.AssignTask(userEmail, creatorEmail, boardName, "done", taskId, emailAssignee);
                }
                */
                log.Info("AssignTask - SUCCESS" + ", task successfully moved to " + emailAssignee);

            }
            catch (Exception e)
            {
                log.Error("AssignTask- " + e.Message);
                return new Response(e.Message);
            }
            return new Response();
        }

        public Response DeleteAll()
        {
            try
            {
                boardController.DeleteAll();
                log.Info("DeleteAll - SUCCESS");
            }
            catch (Exception e)
            {
                log.Error("DeleteAll- FAILL " + e.Message);
                return new Response(e.Message);
            }
            return new Response();
        }

        public Response LoadAll()
        {
            try
            {
                boardController.LoadAll();
                log.Info("LoadAll - SUCCESS");
            }   
            catch (Exception e)
            {
                log.Error("LoadAll- FAILL " + e.Message);
                return new Response(e.Message);
            }
            return new Response();

        }

        /// <summary>
        /// Adds a new column
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The location of the new column. Location for old columns with index>=columnOrdinal is increased by 1 (moved right). The first column is identified by 0, the location increases by 1 for each column.</param>
        /// <param name="columnName">The name for the new columns</param>        
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AddColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, string columnName)
        {
            try
            {
                boardController.AddColumn( userEmail,  creatorEmail,  boardName, columnOrdinal, columnName);
                log.Info("AddColumn - SUCCESS" + ", column has been successfully added for " + creatorEmail+" " +boardName+" board");
            }
            catch (Exception e)
            {
                log.Error("AddColumn- FAIL " + e.Message);
                return new Response(e.Message);
            }
            return new Response();
        }



        /// <summary>
        /// Removes a specific column
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column location. The first column location is identified by 0, the location increases by 1 for each column</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response RemoveColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            try
            {
                boardController.RemoveColumn(userEmail, creatorEmail, boardName, columnOrdinal);
                log.Info("RemoveColumn - SUCCESS" + ", board successfully removed for " + creatorEmail + " " + boardName + " board");
            }
            catch (Exception e)
            {
                log.Error("RemoveColumn- FAIL " + e.Message);
                return new Response(e.Message);
            }
            return new Response();
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
        public Response RenameColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, string newColumnName)
        {
            try
            {
                boardController.RenameColumn(userEmail, creatorEmail, boardName, columnOrdinal, newColumnName);
                log.Info("RenameColumn - SUCCESS" + ", Column name successfully change for " + creatorEmail + " " + boardName + " board");
            }
            catch (Exception e)
            {
                log.Error("RenameColumn- FAIL " + e.Message);
                return new Response(e.Message);
            }
            return new Response();
        }

        /// <summary>
        /// Moves a column shiftSize times to the right. If shiftSize is negative, the column moves to the left
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column location. The first column location is identified by 0, the location increases by 1 for each column</param>
        /// <param name="shiftSize">The number of times to move the column, relativly to its current location. Negative values are allowed</param>  
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response MoveColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int shiftSize)
        {
            try
            {
                boardController.MoveColumn(userEmail, creatorEmail, boardName, columnOrdinal, shiftSize);
                log.Info("MoveColumn - SUCCESS" + ", Column is successfully moved for " + creatorEmail + " " + boardName + " board");
            }
            catch (Exception e)
            {
                log.Error("MoveColumn- FAIL " + e.Message);
                return new Response(e.Message);
            }
            return new Response();
        }
        public Response<List<BoardS>> getAllBoards()//list of the boards I created 
        {
            try
            {
                List<BoardS> Slst = new List<BoardS>();
                List<Board> lst = boardController.getAllBoards();
                foreach (Board b in lst)
                {
                    Slst.Add(new BoardS(b.getName(), b.getUserEmail()));
                }
                return Response<List<BoardS>>.FromValue(Slst);
            }
            catch(Exception e)
            {
                return Response<List<BoardS>>.FromError(e.Message);
            }
        }
   
    }
}