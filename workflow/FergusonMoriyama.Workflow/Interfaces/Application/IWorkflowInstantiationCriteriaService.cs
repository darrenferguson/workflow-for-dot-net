using System.Collections.Generic;
using FergusonMoriyam.Workflow.Interfaces.Domain;

namespace FergusonMoriyam.Workflow.Interfaces.Application
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
