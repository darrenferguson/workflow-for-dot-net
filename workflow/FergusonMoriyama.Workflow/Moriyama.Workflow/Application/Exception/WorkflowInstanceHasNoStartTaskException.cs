using System;

namespace Moriyama.Workflow.Application.Exception
{
    [Serializable]
    public class WorkflowInstanceHasNoStartTaskException : System.Exception
    {
        public WorkflowInstanceHasNoStartTaskException(string message) : base(message) { }
    }
}
