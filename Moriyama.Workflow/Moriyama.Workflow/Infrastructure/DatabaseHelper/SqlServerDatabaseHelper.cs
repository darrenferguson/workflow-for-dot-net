using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using Moriyama.Workflow.Interfaces.Infrastructure;
using log4net;

namespace Moriyama.Workflow.Infrastructure.DatabaseHelper
{
    public class SqlServerDatabaseHelper : IDatabaseHelper
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public IConnectionStringProvider ConnectionStringProvider { get; set; }

        private SqlConnection _connection;

        protected void PrepareConnection()
        {
            if(_connection == null)
            {
                _connection = new SqlConnection(ConnectionStringProvider.GetConncetionString());
            }
            if(_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        public string IdentityQuery
        {
            get { return "select @@Identity"; }
        }

        public DbTransaction BeginTransaction()
        {
            PrepareConnection();
            return _connection.BeginTransaction();
        }

        public DbCommand CreateCommand(string text)
        {
            PrepareConnection();
            return new SqlCommand(text, _connection);
        }

        public DbParameter CreateParameter(string name, object value)
        {
            PrepareConnection();
            return new SqlParameter("@" + name, value);
        }

        public void CloseConnection()
        {
            _connection.Close();
        }
    }
}
