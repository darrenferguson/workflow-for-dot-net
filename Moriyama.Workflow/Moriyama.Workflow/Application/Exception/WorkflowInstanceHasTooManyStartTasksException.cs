using System;

namespace Moriyama.Workflow.Application.Exception
{
    [Serializable]
    public class WorkflowInstanceHasTooManyStartTasksException : System.Exception
    {
        public WorkflowInstanceHasTooManyStartTasksException(string message) : base(message) { }
    }
}
