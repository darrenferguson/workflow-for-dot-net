using System.Configuration;
using System.Data.Common;
using FergusonMoriyam.Workflow.Interfaces.Infrastructure;

namespace FergusonMoriyam.Workflow.Umbraco.Application.Infrastructure
{
    public class UmbracoConnectionStringProvider : IConnectionStringProvider
    {
        #region Instance
        private static readonly UmbracoConnectionStringProvider Provider = new UmbracoConnectionStringProvider();

        public static UmbracoConnectionStringProvider Instance
        {
            get { return Provider; }
        }

        private UmbracoConnectionStringProvider()
            : base()
        {
        }
        #endregion

        private DbConnectionStringBuilder connectionStringBuilder;

        public string GetConncetionString()
        {

            if(connectionStringBuilder == null)
            {
                connectionStringBuilder = new DbConnectionStringBuilder
                                              {ConnectionString = ConfigurationManager.AppSettings["umbracoDbDSN"]};

                connectionStringBuilder.Remove("datalayer");
            }

            return connectionStringBuilder.ConnectionString;

        }
    }
}
