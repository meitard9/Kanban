using log4net;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DOBs.BoardPackage
{
    class BoardDOB : DOB
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public const string TableName = "Boards";
        public const string EmailColumnName = "userEmail";
        public const string NameColumnName = "name";
        //public const string IdColumn = "boardID";
        public /*const*/ string TaskIndexColumnName = "taskIndex";

        internal bool persisted { get; set; } = false;

        //public int boardId { get;private set; }
        public string userEmail { get;private set; }
        public string name { get; private set; }

        private int _taskIndex;
        public int taskIndex { get { return _taskIndex; } set{ _taskIndex = value; if (persisted) Update(TaskIndexColumnName, value); } }


        public BoardDOB(/*int boardId,*/ string userEmail, string name, int taskIndex) : base(TableName)

        {
            //this.boardId = boardId;
            this.userEmail = userEmail;
            this.name = name;
            this.taskIndex = taskIndex;
        }
        public bool Insert()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"INSERT INTO {TableName} ({EmailColumnName},{NameColumnName},{TaskIndexColumnName}) " +
                        $"VALUES (@emailVal,@nameVal,@taskIndexVal);"
                };
                try
                {

                    connection.Open();
                    //SQLiteParameter idParam = new SQLiteParameter(@"idVal", boardId);
                    SQLiteParameter emailParam = new SQLiteParameter(@"emailVal", userEmail);
                    SQLiteParameter nameParam = new SQLiteParameter(@"nameVal", name);
                    SQLiteParameter taskIndexParam = new SQLiteParameter(@"taskIndexVal", taskIndex);

                    //command.Parameters.Add(idParam);
                    command.Parameters.Add(emailParam);
                    command.Parameters.Add(nameParam);
                    command.Parameters.Add(taskIndexParam);
                    command.Prepare();

                    res = command.ExecuteNonQuery();
                    if (!persisted)
                        persisted = true;
                    log.Info("Insert- SECCESS");
                }
                catch (Exception e)
                {
                    log.Error("Insert- ERRORO " + e);
                    throw e;
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
                return res > 0;
            }

        }

        public bool Update(string attributeName, int attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update {TableName} set {attributeName}=@val where {EmailColumnName}=@email and {NameColumnName}=@column"
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter(@"val", attributeValue));
                    command.Parameters.Add(new SQLiteParameter(@"email", userEmail)); 
                    command.Parameters.Add(new SQLiteParameter(@"column", name));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                finally
                {
                    command.Dispose();
                    connection.Close();

                }

            }
            return res > 0;
        }

        public bool Delete()
        {
            int res1 = -1;
            int res2 = -1;
            int res3 = -1;
            int res4 = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command1 = new SQLiteCommand
                {
                    Connection = connection,

                    CommandText = $"delete from {TableName} where {EmailColumnName}=@email and {NameColumnName}=@column"

                };
                var command2 = new SQLiteCommand
                {
                    Connection = connection,

                    CommandText = $"delete from {ColumnDOB.TableName} where {ColumnDOB.boardCreator}=@email and {ColumnDOB.bName}=@column"

                };
                var command3 = new SQLiteCommand
                {
                    Connection = connection,

                    CommandText = $"delete from {TaskDOB.TableName} where {TaskDOB.boardCreator}=@email and {TaskDOB.bName}=@column"

                };
                var command4 = new SQLiteCommand
                {
                    Connection = connection,

                    CommandText = $"delete from {BoardMemberDOB.TableName} where {BoardMemberDOB.CreatorEmail}=@BoardMembersCreatorEmail and {BoardMemberDOB.BoardName}=@BoardMembersBoardName"

                };
                try
                {
                    connection.Open();
                    command1.Parameters.Add(new SQLiteParameter(@"email", userEmail));
                    command1.Parameters.Add(new SQLiteParameter(@"column", name));
                    command2.Parameters.Add(new SQLiteParameter(@"email", userEmail));
                    command2.Parameters.Add(new SQLiteParameter(@"column", name));
                    command3.Parameters.Add(new SQLiteParameter(@"email", userEmail));
                    command3.Parameters.Add(new SQLiteParameter(@"column", name));
                    command4.Parameters.Add(new SQLiteParameter(@"BoardMembersCreatorEmail", userEmail));
                    command4.Parameters.Add(new SQLiteParameter(@"BoardMembersBoardName", name));

                    res1 = command1.ExecuteNonQuery();
                    res2 = command2.ExecuteNonQuery();
                    res3 = command3.ExecuteNonQuery();
                    res4 = command4.ExecuteNonQuery();

                }
                finally
                {
                    command1.Dispose();
                    command2.Dispose();
                    command3.Dispose();
                    connection.Close();
                }

            }
            return res1 > 0 | res2 > 0 | res3 > 0;
        }

    }
}
