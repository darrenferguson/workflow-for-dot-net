using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using Common.Logging;
using umbraco.DataLayer;

namespace Moriyama.Workflow.Umbraco6.Installer.Database
{
    public class InstallSqlCe : Singleton<InstallSqlCe>
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void Run(string configFile, string connectionString)
        {
            Log.Debug("Running SqlCe install");

            var config = File.ReadAllText(configFile);
            config = config.Replace(
                "Moriyama.Workflow.Infrastructure.DatabaseHelper.SqlServerDatabaseHelper, Moriyama.Workflow",
                "Moriyama.Workflow.Umbraco6.Infrastructure.UmbracoSqlCeDatabaseHelper, Moriyama.Workflow.Umbraco6");
            
            File.WriteAllText(configFile, config);

            var sql = Helper.Instance.ReadAssemblyResource("Resources.Sql.Ce.Install.sql");
            Log.Debug("Read SQL from resource stream");
            Log.Debug(sql);

            Log.Debug("Executing SQL");
            var helper = DataLayerHelper.CreateSqlHelper(connectionString);

            foreach (var s in sql.Split(';'))
            {
                if (s.Trim().Length <= 0) continue;

                try
                {
                    helper.ExecuteNonQuery(s);
                } catch(Exception ex)
                {
                    Log.Fatal(ex);
                }
            }
            
            Log.Debug("SQL Executed.");
        }
    }    
}
