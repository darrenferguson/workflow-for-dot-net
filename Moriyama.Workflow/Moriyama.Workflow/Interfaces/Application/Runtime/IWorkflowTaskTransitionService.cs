using System.Collections.Generic;
using Moriyama.Workflow.Interfaces.Domain;

namespace Moriyama.Workflow.Interfaces.Application.Runtime
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
