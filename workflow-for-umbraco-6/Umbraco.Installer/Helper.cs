using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Common.Logging;
using umbraco;

namespace FergusonMoriyam.Workflow.Umbraco.Installer
{

    public enum DatabaseType
    {
        SqlServer,
        SqlCe,
        MySql,
        Unknown
    }

    public class Helper : Singleton<Helper>
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public DatabaseType GetDatbaseType(string connectionString)
        {
            Log.Debug("Checking database type based on: " + connectionString);
            connectionString = connectionString.ToLower();

            
            if (connectionString.Contains("datalayer=sqlce4umbraco") && connectionString.Contains(".sdf"))
            {
                Log.Debug("Got database type as CE");
                return DatabaseType.SqlCe;

            }
            else if (connectionString.Contains("datalayer=mysql"))
            {
                Log.Debug("Got database type as MySQL");
                return DatabaseType.MySql;
            }

            Log.Debug("Got database type as SqlServer");
            return DatabaseType.SqlServer;
        }
        
        public string GetWorkflowVersion()
        {
            var v = Assembly.GetExecutingAssembly().GetName().Version;
            return v.Major + "." + v.Minor + "." + v.Build +"." + v.Revision;
        }

        public string GetUmbracoVersion()
        {
            return GlobalSettings.CurrentVersion;
        }

        public string DotNetVersion()
        {
            return RuntimeEnvironment.GetSystemVersion();
        }

        public StreamReader GetAssemblyResourceStream(string name)
        {
            var a = Assembly.GetAssembly(GetType());
            return new StreamReader(a.GetManifestResourceStream("FergusonMoriyam.Workflow.Umbraco.Installer."+name));
        }

        public string ReadAssemblyResource(string name)
        {
            var reader = GetAssemblyResourceStream(name);
            var content = reader.ReadToEnd();
            reader.Close();
            return content;
        }
    }
}
