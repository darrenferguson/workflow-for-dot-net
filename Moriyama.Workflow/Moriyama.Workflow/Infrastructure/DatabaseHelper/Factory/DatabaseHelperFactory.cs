using System;
using System.Reflection;
using Moriyama.Workflow.Interfaces.Infrastructure;
using Moriyama.Workflow.Interfaces.Infrastructure.DatabaseHelper.Factory;
using log4net;

namespace Moriyama.Workflow.Infrastructure.DatabaseHelper.Factory
{
    public class DatabaseHelperFactory : IDatabaseHelperFactory
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region singleton
        private static readonly DatabaseHelperFactory HelperFactory = new DatabaseHelperFactory();

        public static DatabaseHelperFactory Instance
        {
            get { return HelperFactory; }
        }

        private DatabaseHelperFactory()
        {    
        }
        #endregion

        public IDatabaseHelper CreateDatabaseHelper(string assemblyQualifiedName)
        {
            return (IDatabaseHelper) Activator.CreateInstance(Type.GetType(assemblyQualifiedName));
        }
    }
}
