using System.Data;
using System.Data.Common;
using System.Data.SqlServerCe;
using System.Reflection;
using FergusonMoriyam.Workflow.Interfaces.Infrastructure;
using Common.Logging;

namespace FergusonMoriyam.Workflow.Infrastructure.DatabaseHelper
{
    public class SqlCeDatbaseHelper : IDatabaseHelper
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public IConnectionStringProvider ConnectionStringProvider { get; set; }

        private SqlCeConnection _connection;

        protected void PrepareConnection()
        {
            if (_connection == null)
            {
                _connection = new SqlCeConnection(ConnectionStringProvider.GetConncetionString());
            }

            if (_connection.State != ConnectionState.Open)
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
            return new SqlCeCommand(text, _connection);
        }

        public DbParameter CreateParameter(string name, object value)
        {
            PrepareConnection();
            return new SqlCeParameter("@" + name, value);
        }

        public void CloseConnection()
        {
            _connection.Close();
        }
    }
}
