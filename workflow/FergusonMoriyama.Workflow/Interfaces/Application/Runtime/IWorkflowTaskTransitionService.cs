using System.Collections.Generic;
using FergusonMoriyam.Workflow.Interfaces.Domain;

namespace FergusonMoriyam.Workflow.Interfaces.Application.Runtime
{
    public interface IWorkflowTaskTransitionService
    {
        IWorkflowInstance GetWorkflowInstance(int workflowInstanceId);
        IDictionary<string, string> GetTransitions(IWorkflowInstance instance);

        void Transition(IWorkflowInstance instance, string transiton);
        void Transition(IWorkflowInstance instance, string transition, string comment);

        bool CanTransition(IWorkflowInstance instance);
    }
}
