using log4net;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DOBs
{
    internal abstract class DOB
    {
        //log setup
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected readonly string _connectionString;
        private readonly string _tableName;
        public DOB(string tableName)
        {
             string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "kanban.db"));
            this._connectionString = $"Data Source={path}; Version=3;";
            this._tableName = tableName;
            
        }

    }
}   


