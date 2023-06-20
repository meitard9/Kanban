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
    class BoardMemberDOB: DOB
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private const string BoardTableName = BoardDOB.TableName;
        public const string TableName = "BoardMembers";
        public const string UserName = "UserName";
        public const string CreatorEmail = "CreatorEmail";
        public const string BoardName = "BoardName";

        //public bool persisted;
        public string user;
        public string creator;
        public string Board;

        public BoardMemberDOB(string user, string creator, string board) : base(TableName)
        {
            this.user = user;
            this.creator = creator;
            this.Board = board;
        }

        public bool Insert()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"INSERT INTO {TableName} ({UserName} ,{CreatorEmail},{BoardName}) " +
                        $"VALUES (@idlVal,@typeVal,@limitVal);"
                };
                try
                {
                    connection.Open();

                    SQLiteParameter userParam = new SQLiteParameter(@"idlVal", user);
                    SQLiteParameter creatorParam = new SQLiteParameter(@"typeVal", creator);
                    SQLiteParameter boardParam = new SQLiteParameter(@"limitVal", Board);

                    command.Parameters.Add(userParam);
                    command.Parameters.Add(creatorParam);
                    command.Parameters.Add(boardParam);
                    command.Prepare();

                    res = command.ExecuteNonQuery();
                    /*if (!persisted)
                        persisted = true;*/
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



    }
}
