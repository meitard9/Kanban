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
     class TaskDOB : DOB
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public const string TableName = "Tasks";
        public const string TitleColumnName = "title";
        public const string DescriptionColumnName = "description";
        public const string IdColumn = "id";
        public const string boardCreator = "BoardCreator";
        public const string bName = "BoardName";
        public const string CurrColumnName = "currColumn";
        public const string creationColumnName = "creationDate";
        public const string DueColumnName = "dueDate";
        public const string AssigneeColumnName = "assignee";

        internal bool persisted { private get; set; } = false;

        internal int id { get; private set; }
        internal string boardCreatorName { get; private set; }
        internal string boardName { get; private set; }
        internal string creationDate { get; private set; }
        private int _currColumn;
        private string _dueDate;
        private string _title;
        private string _description;
        private string _assignee;

        public int currColumn { get => _currColumn;
            set { _currColumn = value; if (persisted) Update(CurrColumnName, value); } }
        public string dueDate { get => _dueDate;
            set { _dueDate = value; if (persisted) Update(DueColumnName, value); } }
        public string title { get => _title;
            set { _title = value; if (persisted) Update(TitleColumnName, value); } }
        public string description { get => _description;
            set { _description = value; if (persisted) Update(DescriptionColumnName, value); } }
        public string assignee { get => _assignee;
            set { _assignee = value; if (persisted) Update(AssigneeColumnName, value); } }

        public TaskDOB(int id, string boardCreatorName, string boardName, int currColumn, string dueDate
            , string creationDate, string title, string description , string assignee) : base(TableName)

        {
            this.id = id;
            this.boardCreatorName = boardCreatorName;
            this.boardName = boardName;
            this.creationDate = creationDate;
            this.currColumn = currColumn;
            this.title = title;
            this.description = description;
            this.assignee = assignee;
            this.dueDate = dueDate;
        }
        public bool Insert()
        {

            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {TableName} ({IdColumn} ,{boardCreator},{bName},{CurrColumnName}" +
                        $",{DueColumnName},{creationColumnName},{TitleColumnName},{DescriptionColumnName},{AssigneeColumnName}) " +
                        $"VALUES (@idVal,@BC_NamelVal,@B_NamelVal,@currVal,@dueVal,@creatVal,@titleVal,@desVal,@assiVal);";

                    SQLiteParameter idParam = new(@"idVal", id);
                    //SQLiteParameter boardParam = new SQLiteParameter(@"boardVal", boardID);
                    SQLiteParameter BCNParam = new SQLiteParameter(@"BC_NamelVal", boardCreatorName);
                    SQLiteParameter BoardNameParam = new SQLiteParameter(@"B_NamelVal", boardName);
                    SQLiteParameter currParam = new SQLiteParameter(@"currVal", currColumn);
                    SQLiteParameter dueParam = new SQLiteParameter(@"dueVal", dueDate);
                    SQLiteParameter creatParam = new SQLiteParameter(@"creatVal", creationDate);
                    SQLiteParameter titleParam = new SQLiteParameter(@"titleVal", title);
                    SQLiteParameter desParam = new SQLiteParameter(@"desVal", description);
                    SQLiteParameter assiParam = new SQLiteParameter(@"assiVal", assignee);

                    command.Parameters.Add(idParam);
                    command.Parameters.Add(BCNParam);
                    command.Parameters.Add(BoardNameParam);
                    command.Parameters.Add(currParam);
                    command.Parameters.Add(dueParam);
                    command.Parameters.Add(creatParam);
                    command.Parameters.Add(titleParam);
                    command.Parameters.Add(desParam);
                    command.Parameters.Add(assiParam);
                    command.Prepare();

                    res = command.ExecuteNonQuery();
                    if (!persisted)
                        persisted = true;
                    log.Info("Insert is Success");
                }
                catch(Exception e)
                {
                    log.Error("ERROR" + e.ToString());
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

        public bool Update(string attributeName, string attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update {TableName} set {attributeName}=@attName " +
                    $"where {IdColumn}=@idVal AND {boardCreator}=@BC_NamelVal and {bName}=@B_NamelVal "
                };
                try
                {

                    command.Parameters.Add(new SQLiteParameter(@"attName", attributeValue));
                    command.Parameters.Add(new SQLiteParameter(@"idVal", id));
                    command.Parameters.Add(new SQLiteParameter(@"BC_NamelVal", boardCreatorName));
                    command.Parameters.Add(new SQLiteParameter(@"B_NamelVal", boardName));
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    log.Info("Update SECCESS");
                }
                catch(Exception e)
                {
                    log.Error("Update FAIL- " + e);
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
        public bool Update(string attributeName, int attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update {TableName} set {attributeName}=@attName " +
                    $"where {IdColumn}=@idVal AND {boardCreator}=@BC_NamelVal and {bName}=@B_NamelVal "
                };
                try
                {

                    command.Parameters.Add(new SQLiteParameter(@"attName", attributeValue));
                    command.Parameters.Add(new SQLiteParameter(@"idVal", id));
                    command.Parameters.Add(new SQLiteParameter(@"BC_NamelVal", boardCreatorName));
                    command.Parameters.Add(new SQLiteParameter(@"B_NamelVal", boardName));
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    log.Info("Update SECCESS");
                }
                catch (Exception e)
                {
                    log.Error("Update FAIL- " + e);
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
       
        public bool Delete()
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {TableName} where {IdColumn}=@idVal AND {boardCreator}=@BC_NamelVal and {bName}=@B_NamelVal"
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter(@"idVal", id));
                    command.Parameters.Add(new SQLiteParameter(@"BC_NamelVal", boardCreatorName));
                    command.Parameters.Add(new SQLiteParameter(@"B_NamelVal", boardName));
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
