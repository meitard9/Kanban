using System.Collections.Generic;
using System;
using System.Linq;
using IntroSE.Kanban.Backend.ServiceLayer.Objects;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class Service
    {
        private UserService userService;
        private boardService boardService;
       
        public Service()
        {
             userService=new UserService();
             boardService=new boardService();
        }

        ///<summary>This method loads the data from the persistance.
        ///         You should call this function when the program starts. </summary>
        public Response LoadData()
        {
            Response us = userService.LoadData();
            
            Response bs = boardService.LoadAll();
            if (bs.ErrorOccured)
                return bs;
            return us;
        }


        ///<summary>Removes all persistent data.</summary>
        public Response DeleteData()
        {
            Response us=  userService.DeleteAll();
            Response bs=  boardService.DeleteAll();
            if (bs.ErrorOccured)
                return bs;
            return us;
        }

        /// <summary>
        /// Log in an existing user
        /// </summary>
        /// <param name="userEmail">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns>A response object with a value set to the user, instead the response should contain a error message in case of an error</returns>
        public Response<User> Login(string userEmail, string password)
        {
            return userService.Login(userEmail, password);
        }

        /// <summary>        
        /// Log out an logged-in user. 
        /// </summary>
        /// <param name="userEmail">The email of the user to log out</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response Logout(string userEmail)
        {
            if (!userService.isLoggedIn(userEmail))
                return new Response("user not logged in");
            else
                return userService.Logout(userEmail);
        }

        /// <summary>
        /// Limit the number of tasks in a specific column
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="limit">The new limit value. A value of -1 indicates no limit.</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response LimitColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int limit)
        {

            if (!userService.isLoggedIn(userEmail))
                return new Response("user not logged in");
            else
                return boardService.LimitColumn(userEmail, creatorEmail, boardName, columnOrdinal, limit);

        }

        /// <summary>
        /// Get the limit of a specific column
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>The limit of the column.</returns>
        public Response<int> GetColumnLimit(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {

            if (!userService.isLoggedIn(userEmail))
                return Response<int>.FromError("user not logged in");
            else
                return boardService.GetColumnLimit(userEmail, creatorEmail, boardName, columnOrdinal);

        }

        /// <summary>
        /// Get the name of a specific column
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>The name of the column.</returns>
        public Response<string> GetColumnName(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {

            if (!userService.isLoggedIn(userEmail))
                return Response<string>.FromError("user not logged in");
            else
                return boardService.GetColumnName(userEmail, creatorEmail, boardName, columnOrdinal);

        }

        /// <summary>
        /// Add a new task.
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date of the new task</param>
        /// <returns>A response object with a value set to the Task, instead the response should contain a error message in case of an error</returns>
        public Response<TaskS> AddTask(string userEmail, string creatorEmail, string boardName, string title, string description, DateTime dueDate)
        {

            if (!userService.isLoggedIn(userEmail))
                return Response<TaskS>.FromError("user not logged in");
            else
                return boardService.AddTask(userEmail, creatorEmail, boardName, title, description, dueDate);

        }

        /// <summary>
        /// Update the due date of a task
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="dueDate">The new due date of the column</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskDueDate(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {

            if (!userService.isLoggedIn(userEmail))
                return new Response("user not logged in");
            else
                return boardService.UpdateTaskDueDate(userEmail, creatorEmail, boardName, columnOrdinal, taskId, dueDate);

        }

        /// <summary>
        /// Update task title
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="title">New title for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskTitle(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string title)
        {

            if (!userService.isLoggedIn(userEmail))
                return new Response("user not logged in");
            else
                return boardService.UpdateTaskTitle(userEmail, creatorEmail, boardName, columnOrdinal, taskId, title);

        }

        /// <summary>
        /// Update the description of a task
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="description">New description for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskDescription(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string description)
        {

            if (!userService.isLoggedIn(userEmail))
                return new Response("user not logged in");
            else
                return boardService.UpdateTaskDescription(userEmail, creatorEmail, boardName, columnOrdinal, taskId, description);

        }

        /// <summary>
        /// Advance a task to the next column
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AdvanceTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId)
        {

            if (!userService.isLoggedIn(userEmail))
                return new Response("user not logged in");
            else
                return boardService.AdvanceTask(userEmail, creatorEmail, boardName, columnOrdinal, taskId);

        }

        /// <summary>
        /// Returns a column given it's name
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>A response object with a value set to the Column, The response should contain a error message in case of an error</returns>
        public Response<IList<TaskS>> GetColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {

            if (!userService.isLoggedIn(userEmail))
                return Response<IList<TaskS>>.FromError("user not logged in");
            else
                return boardService.GetColumn(userEmail, creatorEmail, boardName, columnOrdinal);

        }


        /// <summary>
        /// Creates a new board for the logged-in user.
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="boardName">The name of the new board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AddBoard(string userEmail, string boardName)
        {

            if (!userService.isLoggedIn(userEmail))
                return new Response("user not logged in");
            else
                return boardService.AddBoard(userEmail, boardName);

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

            if (!userService.isLoggedIn(userEmail))
                return new Response("user not logged in");
            else
                return boardService.JoinBoard(userEmail, creatorEmail, boardName);

        }
        /// <summary>
        /// Removes a board.

        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response RemoveBoard(string userEmail, string creatorEmail, string boardName)
        {
            if (!userService.isLoggedIn(userEmail))
                return new Response("user not logged in");
            else
                return boardService.RemoveBoard(userEmail, creatorEmail, boardName);
        }
        /// <summary>
        /// Returns all the in-progress tasks of the logged-in user is assigned to.
        /// </summary>

        /// <param name="userEmail">Email of the logged in user</param>
        /// <returns>A response object with a value set to the list of tasks, The response should contain a error message in case of an error</returns>
        public Response<IList<TaskS>> InProgressTasks(string userEmail)
        {

            if (!userService.isLoggedIn(userEmail))
                return Response<IList<TaskS>>.FromError("user not logged in");
            else
                return boardService.InProgressTasks(userEmail);

        }


        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="userEmail">The email address of the user to register</param>
        /// <param name="password">The password of the user to register</param>
        /// <returns>A response object. The response should contain a error message in case of an error<returns>
        public Response Register(string userEmail, string password)
        {

            /*if (!userService.isLoggedIn(userEmail))
                return new Response("user not logged in");
            else*/
                return userService.Register(userEmail, password);

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
            if (!userService.isLoggedIn(userEmail))
                return new Response("user not logged in");
            else
                return boardService.AssignTask(userEmail, creatorEmail, boardName, columnOrdinal, taskId, emailAssignee);

        }

        /// <summary>
        /// Returns the list of board of a user. The user must be logged-in. The function returns all the board names the user created or joined.
        /// </summary>
        /// <param name="userEmail">The email of the user. Must be logged-in.</param>
        /// <returns>A response object with a value set to the board, instead the response should contain a error message in case of an error</returns>
        public Response<IList<String>> GetBoardNames(string userEmail)
        {
            if (!userService.isLoggedIn(userEmail))
                return Response<IList<String>>.FromError("user not logged in");
            else
                return boardService.GetBoardNames(userEmail);

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
            if (!userService.isLoggedIn(userEmail))
                return new Response("user not logged in");
            else
                return boardService.AddColumn(userEmail, creatorEmail, boardName, columnOrdinal, columnName);
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
            if (!userService.isLoggedIn(userEmail))
                return new Response("user not logged in");
            else
                return boardService.RemoveColumn(userEmail, creatorEmail, boardName, columnOrdinal);
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
            if (!userService.isLoggedIn(userEmail))
                return new Response("user not logged in");
            else
                return boardService.RenameColumn(userEmail, creatorEmail, boardName, columnOrdinal, newColumnName);
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
            if (!userService.isLoggedIn(userEmail))
                return new Response("user not logged in");
            else
                return boardService.MoveColumn(userEmail, creatorEmail, boardName, columnOrdinal, shiftSize);
        }
        public Response<List<BoardS>> getAllBoards()//list of the boards I created 
        {
            return boardService.getAllBoards();
        }



    }
}