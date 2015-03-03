//using System.Data;
//using System.Data.Common;
//using System.Reflection;
//using FergusonMoriyam.Workflow.Interfaces.Infrastructure;
//using MySql.Data.MySqlClient;
//using log4net;

//namespace FergusonMoriyam.Workflow.Infrastructure.DatabaseHelper
//{
//    public class MySqlDatabaseHelper : IDatabaseHelper
//    {
//        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

//        public IConnectionStringProvider ConnectionStringProvider { get; set; }

//        private MySqlConnection _connection;

//        protected void PrepareConnection()
//        {
//            if (_connection == null)
//            {
//                _connection = new MySqlConnection(ConnectionStringProvider.GetConncetionString());
//            }

//            if (_connection.State != ConnectionState.Open)
//            {
//                _connection.Open();
//            }
//        }

//        public string IdentityQuery
//        {
//            get { return "SELECT LAST_INSERT_ID();"; }
//        }

//        public DbTransaction BeginTransaction()
//        {
//            PrepareConnection();
//            return _connection.BeginTransaction();
//        }

//        public DbCommand CreateCommand(string text)
//        {
//            PrepareConnection();
//            text = text.Replace("@", "?");
//            return new MySqlCommand(text, _connection);
//        }

//        public DbParameter CreateParameter(string name, object value)
//        {
//            PrepareConnection();
//            return new MySqlParameter("?" + name, value);
//        }

//        public void CloseConnection()
//        {
//            _connection.Close();
//        }
//    }
//}
