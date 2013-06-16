using System;
using System.Configuration;
using System.Reflection;
using Common.Logging;
using umbraco.DataLayer;

namespace FergusonMoriyam.Workflow.Umbraco.Installer.Database
{
    public class InstallSqlServer : Singleton<InstallSqlServer>
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void Run(string configFile, string connectionString)
        {
            Log.Debug("Running SQL Server install");

            var sql = Helper.Instance.ReadAssemblyResource("Resources.Sql.SqlServer.Install.sql");
            Log.Debug("Read SQL from resource stream");
            Log.Debug(sql);

            Log.Debug("Executing SQL");
            var helper = DataLayerHelper.CreateSqlHelper(connectionString);

            try
            {
                helper.ExecuteNonQuery(sql);
            } catch(Exception e)
            {
                Log.Warn(e);
            }

            Log.Debug("SQL Executed.");
        }
    }
}
