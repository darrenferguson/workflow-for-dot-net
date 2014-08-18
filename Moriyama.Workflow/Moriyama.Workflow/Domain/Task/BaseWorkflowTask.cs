using System;
using System.Collections.Generic;
using System.Reflection;
using Moriyama.Workflow.Interfaces.Domain;
using Common.Logging;

namespace Moriyama.Workflow.Domain.Task
{
    [Serializable]
    public abstract class BaseWorkflowTask : IWorkflowTask
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected BaseWorkflowTask()
        {
            AvailableTransitions = new List<string>();
            Transitions = new Dictionary<string, Guid>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }
        public bool IsStartTask { get; set; }

        public string Description { get; set; }

        public IList<string> AvailableTransitions { get; set; }
        public IDictionary<string, Guid> Transitions { get; set; }
    }
}
