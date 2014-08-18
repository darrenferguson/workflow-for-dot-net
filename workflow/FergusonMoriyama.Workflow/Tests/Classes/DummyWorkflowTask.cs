using System;
using Moriyama.Workflow.Domain.Task;
using Moriyama.Workflow.Interfaces.Domain;

namespace Moriyama.Workflow.Tests.Classes
{
    [Serializable]
    class DummyWorkflowTask : BaseWorkflowTask, IWorkflowTask
    {
        public DummyWorkflowTask() : base()
        {
            AvailableTransitions.Add("approve");
            AvailableTransitions.Add("reject");
        }
    }
}
