using System;

namespace Moriyama.Workflow.Application.Exception
{
    [Serializable]
    public class WorkflowTaskInvalidTransitionException : System.Exception
    {
        public WorkflowTaskInvalidTransitionException(string message) : base(message) { }
    }
}
