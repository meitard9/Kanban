using IntroSE.Kanban.Backend.DataAccessLayer.DOBs;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DOBs.BoardPackage
{
    class BoardDalController :DOB
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private const string TableName = BoardDOB.TableName;
        public BoardDalController() : base(TableName){}

        public bool DeleteAll()
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

                    CommandText = $"delete from {TableName}"

                };
                var command2 = new SQLiteCommand
                {
                    Connection = connection,

                    CommandText = $"delete from {ColumnDOB.TableName}"

                };
                var command3 = new SQLiteCommand
                {
                    Connection = connection,

                    CommandText = $"delete from {TaskDOB.TableName}"

                };
                var command4 = new SQLiteCommand
                {
                    Connection = connection,

                    CommandText = $"delete from {BoardMemberDOB.TableName}"

                };
                try
                {
                    connection.Open();
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
                    command4.Dispose();
                    connection.Close();
                }

            }
            return res1 > 0 | res2 > 0 | res3 > 0 | res4 > 0 ;
        }
        private BoardDOB ConvertReaderToBoardDOB(SQLiteDataReader reader)
        {

            try
            {
                BoardDOB result = new BoardDOB(reader.GetString(0), reader.GetString(1), (int)(long)reader.GetValue(2));
                result.persisted = true;
                log.Info("ConvertReaderToBoardDOB- SUCCESS");
                return result;
            }

            catch (Exception e)
            {
                log.Error("ConvertReaderToBoardDOB- ERROR " + e);
                throw e;
            }
            
        }

        private ColumnDOB ConvertReaderToColumnDOB(SQLiteDataReader reader)
        {
            try
            {
                ColumnDOB result = new ColumnDOB(reader.GetString(0), reader.GetString(1), reader.GetString(2), (int)(long)reader.GetValue(3), (int)(long)reader.GetValue(4));
                result.persisted = false;
                log.Info("ConvertReaderToColumnDOB- SUCCESS");
                return result;
            }
            catch (Exception e)
            {
                log.Error("ConvertReaderToColumnDOB- ERROR " + e);
                throw e;
            }
        }

        private TaskDOB ConvertReaderToTaskDOB(SQLiteDataReader reader)
        {
            try
            {
                TaskDOB result = new TaskDOB((int)(long)reader.GetValue(0), reader.GetString(1), reader.GetString(2), (int)(long)reader.GetValue(3)
                    , reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8));
                log.Info("ConvertReaderToTaskDOB- SUCCESS");
                return result;
            }
            catch (Exception e)
            {
                log.Error("ConvertReaderToTaskDOB- ERROR " + e);
                throw e;
            }

        }

        public List<TaskDOB> LoadTasks(string boardCreatorName, string boardName)
        {
            List<TaskDOB> results = new List<TaskDOB>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {TaskDOB.TableName} where {TaskDOB.boardCreator}=@BC_NamelVal and {TaskDOB.bName}=@B_NamelVal";
                SQLiteDataReader dataReader = null;
                command.Parameters.Add(new SQLiteParameter(@"BC_NamelVal", boardCreatorName));
                command.Parameters.Add(new SQLiteParameter(@"B_NamelVal", boardName));
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        results.Add(ConvertReaderToTaskDOB(dataReader));

                    }
                    log.Info("LoadTasks- SECCESS");
                }
                catch (Exception e)
                {
                    log.Error("LoadTasks- ERRORO " + e);
                    throw e;
                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }

                    command.Dispose();
                    connection.Close();
                }

            }
            return results;
        }

        public List<ColumnDOB> LoadColumns(string boardCreator, string boardName)
        {
            List<ColumnDOB> results = new List<ColumnDOB>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {ColumnDOB.TableName} where {ColumnDOB.boardCreator}=@BoardCreator and {ColumnDOB.bName}=@BoardName  ORDER BY {ColumnDOB.ColumnId} ASC";
                SQLiteDataReader dataReader = null;
                command.Parameters.Add(new SQLiteParameter(@"BoardCreator", boardCreator));
                command.Parameters.Add(new SQLiteParameter(@"BoardName", boardName));
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        results.Add(ConvertReaderToColumnDOB(dataReader));

                    }
                    log.Info("LoadColumns- SECCESS");
                }
                catch (Exception e)
                {
                    log.Error("LoadColumns- ERRORO " + e);
                    throw e;
                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }

                    command.Dispose();
                    connection.Close();
                }

            }
            return results;
        }

        public List<BoardDOB> LoadBoards()
        {
            List<BoardDOB> results = new List<BoardDOB>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {TableName}";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        results.Add(ConvertReaderToBoardDOB(dataReader));

                    }
                    log.Info("LoadBoards- SECCESS");
                }
                catch (Exception e)
                {
                    log.Error("LoadBoards- ERRORO " + e);
                    throw e;
                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }

                    command.Dispose();
                    connection.Close();
                }

            }
            return results;
        }

        public List<BoardMemberDOB> LoadBoardMembers(string creatorParam, string boardNameParam)
        {
            
            List<BoardMemberDOB> results = new List<BoardMemberDOB>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {BoardMemberDOB.TableName} where {BoardMemberDOB.CreatorEmail}=@creatorEmail and {BoardMemberDOB.BoardName}=@boardName";
                SQLiteDataReader dataReader = null;
                command.Parameters.Add(new SQLiteParameter(@"creatorEmail", creatorParam));
                command.Parameters.Add(new SQLiteParameter(@"boardName", boardNameParam));
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();
                    
                    while (dataReader.Read())
                    {
                        results.Add(ConvertReaderToBoardMemberDOB(dataReader));

                    }
                    log.Info("LoadBoardMembers- SECCESS");
                }
                catch (Exception e)
                {
                    log.Error("LoadBoardMembers- ERROR " + e);
                    throw e;
                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }

                    command.Dispose();
                    connection.Close();
                }

            }
            return results;
        }

        public Boolean isBoardMember(string userName, string boardCreator, string boardName)
        {

            if (userName == boardCreator) return true;

            List<BoardMemberDOB> boardMembers = LoadBoardMembers(boardCreator, boardName);
            foreach (BoardMemberDOB member in boardMembers)
            {
                if (member.user == userName) return true;
            }
            return false;
        }

        public BoardMemberDOB ConvertReaderToBoardMemberDOB(SQLiteDataReader reader)
        {
            try
            {
                BoardMemberDOB result = new BoardMemberDOB(reader.GetString(0), reader.GetString(1), reader.GetString(2));
                //result.persisted = true;
                log.Info("ConvertReaderToBoardMemberDOB- SUCCESS");
                return result;
            }
            catch (Exception e)
            {
                log.Error("ConvertReaderToBoardMemberDOB- ERROR " + e);
                throw e;
            }

        }

        /*
        public int GetMaxBoardId()
        {
            int result = 0;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"SELECT MAX({BoardDOB.IdColumn}) FROM {BoardDOB.TableName};";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();
                    if (dataReader.Read()&&!dataReader.IsDBNull(0))
                    {
                        result = dataReader.GetInt32(0);
                    }
                    else
                        result = 0;
                    
                    log.Info("GetMaxBoardId- SUCCESS");
                }
                catch (Exception e)
                {
                    log.Error("GetMaxBoardId- ERROR " + e);
                    throw e;
                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }

                    command.Dispose();
                    connection.Close();
                }

            }
            return result;
        }*/

    }
}
