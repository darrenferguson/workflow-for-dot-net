using System;

namespace FergusonMoriyam.Workflow.Application.Exception
{
    [Serializable]
    public class WorkflowTaskInvalidTransitionException : System.Exception
    {
        public WorkflowTaskInvalidTransitionException(string message) : base(message) { }
    }
}
