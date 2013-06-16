using System;
using FergusonMoriyam.Workflow.Domain.Task;
using FergusonMoriyam.Workflow.Interfaces.Domain;

namespace FergusonMoriyam.Workflow.Test.Classes
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
