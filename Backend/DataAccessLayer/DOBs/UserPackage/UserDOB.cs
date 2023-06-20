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
    class UserDOB : DOB
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public const string TableName = "Users";
        public const string EmailColumnName = "email";
        public const string PasswordColumnName = "password";
        public const string IdColumn = "email";

        internal bool persisted {private get; set; } = false;
        internal string email { get;private set; }
        private string password;
        public string Password { get => password; set { password = value; if (persisted) Update(IdColumn,email, PasswordColumnName, value); } }
        
        public UserDOB(string email,string pass) :base (TableName)

        {
            this.email = email;
            Password = pass;
        }

        public bool Insert()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand {
                    Connection = connection,
                    CommandText = $"INSERT INTO {TableName} ({EmailColumnName} ,{PasswordColumnName}) " +
                        $"VALUES (@emailVal,@passVal);"
                };
                try
                {
                    connection.Open();
                    SQLiteParameter emailParam = new SQLiteParameter(@"emailVal", email);
                    SQLiteParameter passParam = new SQLiteParameter(@"passVal", Password);

                    command.Parameters.Add(emailParam);
                    command.Parameters.Add(passParam);
                    command.Prepare();

                    res = command.ExecuteNonQuery();
                    if (!persisted)

                    persisted = true;
                    log.Info("Insert is Success");

                }
                catch(Exception e)
                {
                    log.Error("ERROR"+e.ToString());
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


        public bool Update(string IdColumn, string IdValue, string attributeName, string attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update {TableName} set {attributeName}=@aValue where email=@emailVal"
                };
                try
                {

                    command.Parameters.Add(new SQLiteParameter(@"aValue", attributeValue));
                    command.Parameters.Add(new SQLiteParameter(@"emailVal", email));
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch(Exception e)
                {
                    log.Error("ERROR " + e);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;
        }

        public bool Delete(string IdValue)
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {TableName} where [{IdColumn}]=@{IdColumn}={IdValue}"
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
