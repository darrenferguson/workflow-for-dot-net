using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using Common.Logging;
using umbraco.DataLayer;

namespace FergusonMoriyam.Workflow.Umbraco.Installer.Database
{
    public class InstallMySql : Singleton<InstallMySql>
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

         public void Run(string configFile)
         {
             Log.Debug("Running MySql install");

             var config = File.ReadAllText(configFile);
             config = config.Replace(".SqlServerDatabaseHelper", ".MySqlDatabaseHelper");
             File.WriteAllText(configFile, config);

             var sql = Helper.Instance.ReadAssemblyResource("Resources.Sql.MySql.Install.sql");
             Log.Debug("Read SQL from resource stream");
             Log.Debug(sql);

             Log.Debug("Executing SQL");
             var helper = DataLayerHelper.CreateSqlHelper(ConfigurationManager.AppSettings["umbracoDbDSN"]);

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
