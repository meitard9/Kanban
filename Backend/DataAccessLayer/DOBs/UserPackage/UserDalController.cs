using IntroSE.Kanban.Backend.DataAccessLayer.DOBs;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DOBs.UserPackage
{
    class UserDalController : DOB
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private const string TableName = UserDOB.TableName;
        public UserDalController() :base(TableName) {
        }

        public List<UserDOB> LoadUsers()
        {
            List<UserDOB> results = new List<UserDOB>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {TableName};";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        results.Add(ConvertReaderUserDOB(dataReader));

                    }
                    log.Info("LoadUsers- SECCESS");
                }
                catch(Exception e)
                {
                    log.Error("LoadUsers- ERRORO "+e);
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

        private UserDOB ConvertReaderUserDOB(SQLiteDataReader reader)
        {
            UserDOB result = new UserDOB(reader.GetString(0), reader.GetString(1));
            return result;
        }

        public bool DeleteAll()
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,

                    CommandText = $"delete from {TableName}"

                };
                try
                {
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


