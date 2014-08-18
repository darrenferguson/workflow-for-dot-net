using System.Collections.Generic;
using Moriyama.Workflow.Interfaces.Domain;

namespace Moriyama.Workflow.Interfaces.Application
{
    public interface IWorkflowInstantiationCriteriaService
    {
        void CreateWorkflowInstantiationCriteria(string name);
        IList<IWorkflowInstantiationCriteria> List();
        
        IWorkflowInstantiationCriteria GetCriteria(int id);
        void Delete(int id);

        void Save(IWorkflowInstantiationCriteria workflowInstantiationCriteria);
        void SetConfigurationProperties(int criteriaId, IDictionary<string, object> properties);

        IEnumerable<IWorkflowInstantiationCriteria> GetCriteriaForEvents(string eventName);
    }
}
