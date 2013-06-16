using System;

namespace FergusonMoriyam.Workflow.Application.Exception
{
    [Serializable]
    public class WorkflowInstanceAlreadyStartedException : System.Exception
    {
        public WorkflowInstanceAlreadyStartedException(string message) : base(message) { }
    }
}
