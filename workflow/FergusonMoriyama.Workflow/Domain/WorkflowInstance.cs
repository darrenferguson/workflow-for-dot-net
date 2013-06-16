using System;
using System.Collections.Generic;
using System.Reflection;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using log4net;

namespace FergusonMoriyam.Workflow.Domain
{
    [Serializable]
    public class WorkflowInstance :  IWorkflowInstance
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public int Id { get; set; }
        public string Name { get; set; }

        public string Comment { get; set; }

        public DateTime InstantiationTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string TypeName
        {
            get; set;
        }

        public bool Started { get; set; }
        public bool Ended { get; set; }

        public IWorkflowTask CurrentTask { get; set; }

        public IList<IWorkflowTask> Tasks { get; set; }
        public IList<string> TransitionHistory { get; private set; } 

        public WorkflowInstance()
        {
            Tasks = new List<IWorkflowTask>();
            TransitionHistory = new List<string>();

            InstantiationTime = DateTime.Now;
        }
    }
}
