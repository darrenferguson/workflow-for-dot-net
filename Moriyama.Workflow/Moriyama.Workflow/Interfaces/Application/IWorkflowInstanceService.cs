using System.Collections.Generic;
using Moriyama.Workflow.Interfaces.Domain;

namespace Moriyama.Workflow.Interfaces.Application
{
    public interface IWorkflowInstanceService
    {
        IList<IWorkflowInstance> ListInstances();

        IWorkflowInstance Instantiate(int workflowId);
        IWorkflowInstance Instantiate(int workflowId, string comment);
        
        IWorkflowInstance Instantiate(int workflowId, IDictionary<string, object> properties);

        void DeleteWorkflowInstance(int id);
        void Start(int workflowInstanceId);
        void Update(IWorkflowInstance inst);
        IWorkflowInstance GetInstance(int workflowInstanceId);
    }
}
