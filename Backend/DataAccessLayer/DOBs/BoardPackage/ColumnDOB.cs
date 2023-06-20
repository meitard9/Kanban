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
    class ColumnDOB : DOB
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public const string TableName = "Columns";
        public const string ColumnName = "columnType";
        public const string LimitColumnName = "columnLimit";
        public const string boardCreator = "BoardCreator";
        public const string bName = "BoardName";
        public const string ColumnId = "id";
        internal bool persisted { get; set; } = false;
        //private int boardId { get; set; }

        private string boardCreatorName;
        public string BoardCreatorName {
            get { return boardCreatorName; }   
            set { boardCreatorName = value; }
        }
        private string boardName;
        public string BoardName
        {
            get { return boardName; }
            set { boardName = value; }
        }
        private int id;
        public int Id { get => id; set { id = value; if (persisted) Update(ColumnId, value); } }
        private int _limit;
        public int limit { get =>_limit;
            set { _limit = value; if (persisted) Update(LimitColumnName, value); } }

        private string _type;
        public string type
        {
            get => _type;
            set { if (persisted) Update(ColumnName, value);  _type = value;  }
        }

        public ColumnDOB(string boardCreatorName, string boardName, string type, int limit, int id) : base(TableName)

        {
            this.boardCreatorName = boardCreatorName;
            this.boardName = boardName;
            this.type = type;
            this.limit = limit;
            this.id = id;
        }
        public bool Insert()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"INSERT INTO {TableName} ({boardCreator},{bName} ,{ColumnName},{LimitColumnName},{ColumnId}) " +
                        $"VALUES (@BC_NamelVal,@B_NamelVal,@typeVal,@limitVal,@idVal);"
                };
                try
                {connection.Open();

                    SQLiteParameter BCNParam = new SQLiteParameter(@"BC_NamelVal", boardCreatorName);
                    SQLiteParameter BoardNameParam = new SQLiteParameter(@"B_NamelVal", boardName);
                    SQLiteParameter typeParam = new SQLiteParameter(@"typeVal", type);
                    SQLiteParameter limitParam = new SQLiteParameter(@"limitVal", limit);
                    SQLiteParameter idParam = new SQLiteParameter(@"idVal", id);

                    command.Parameters.Add(BCNParam);
                    command.Parameters.Add(BoardNameParam);
                    command.Parameters.Add(typeParam);
                    command.Parameters.Add(limitParam);
                    command.Parameters.Add(idParam);
                    command.Prepare();

                    res = command.ExecuteNonQuery();
                    if (!persisted)
                        persisted = true;
                    log.Info("Insert- SECCESS");
                }
                catch (Exception e)
                {
                    log.Error("Insert- ERROR " + e);
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
                    CommandText = $"update {TableName} set {attributeName}=@val where {boardCreator}=@BC_NamelVal and {bName}=@B_NamelVal and {ColumnName}=@columnType"
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter(@"val", attributeValue));
                    command.Parameters.Add(new SQLiteParameter(@"BC_NamelVal", boardCreatorName));
                    command.Parameters.Add(new SQLiteParameter(@"B_NamelVal", boardName));
                    command.Parameters.Add(new SQLiteParameter(@"columnType", type));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch(Exception e)
                {
                    log.Error("Insert- ERROR " + e);
                    throw e;
                }
                finally
                {
                    command.Dispose();
                    connection.Close();

                }

            }
            return res > 0;
        }

        public bool Update(string attributeName, string attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update {TableName} set {attributeName}=@val where {boardCreator}=@BC_NamelVal and {bName}=@B_NamelVal and {ColumnName}=@columnType"
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter(@"val", attributeValue));
                    command.Parameters.Add(new SQLiteParameter(@"BC_NamelVal", boardCreatorName));
                    command.Parameters.Add(new SQLiteParameter(@"B_NamelVal", boardName));
                    command.Parameters.Add(new SQLiteParameter(@"columnType", type));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    log.Error("Insert- ERROR " + e);
                    throw e;
                }
                finally
                {
                    command.Dispose();
                    connection.Close();

                }

            }
            return res > 0;
        }

        /// <summary>
        /// Delete column from the data base
        /// </summary>
        /// <param name="email">The email address of the user </param>
        /// <param name="col">The column id to delete from the database</param>
        public bool Delete()
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {TableName} where {boardCreator}=@BC_NamelVal and {bName}=@B_NamelVal and {ColumnName}=@columnType"
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter(@"BC_NamelVal", boardCreatorName));
                    command.Parameters.Add(new SQLiteParameter(@"B_NamelVal", boardName));
                    command.Parameters.Add(new SQLiteParameter(@"columnType", type));
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;
        }

    }

}

