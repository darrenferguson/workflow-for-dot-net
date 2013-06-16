using System;
using System.Collections.Generic;
using System.Reflection;
using Common.Logging;
using FergusonMoriyam.Workflow.Interfaces.Application;

namespace FergusonMoriyam.Workflow.Application
{
    public class GuidPool : IGuidPool
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region singleton
        private static readonly GuidPool Pool = new GuidPool();

        public static GuidPool Instance
        {
            get { return Pool; }
        }
        #endregion

        public IEnumerable<string> CreateGuids(int count)
        {
            IList<string> guids = new List<string>();
            for (var a = 0; a < count; a++)
            {
                guids.Add(Guid.NewGuid().ToString());
            }
            return guids;
        }
    }
}
