using System;

namespace FergusonMoriyam.Workflow.Application.Exception
{
    [Serializable]
    public class WorkflowInstanceHasNoStartTaskException : System.Exception
    {
        public WorkflowInstanceHasNoStartTaskException(string message) : base(message) { }
    }
}
