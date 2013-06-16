using System.Collections.Generic;
using System.Reflection;
using FergusonMoriyam.Workflow.Application.Reflection;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using FergusonMoriyam.Workflow.Interfaces.Domain.Factory;
using FergusonMoriyam.Workflow.Interfaces.Infrastructure;
using log4net;

namespace FergusonMoriyam.Workflow.Application
{
    public class WorkflowInstantiationCriteriaService : IWorkflowInstantiationCriteriaService
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region singleton
        private static readonly WorkflowInstantiationCriteriaService Service = new WorkflowInstantiationCriteriaService();
        
        public static WorkflowInstantiationCriteriaService Instance
        {
            get { return Service; }
        }

        private WorkflowInstantiationCriteriaService() { }
        #endregion
        
        #region properties
        public IRepository<IWorkflowInstantiationCriteria, int> TheWorkflowInstantiationCriteriaRepository { get; set; }
        public IWorkflowInstantiationCriteriaFactory TheWorkflowInstantiationCriteriaFactory { get; set; }
        #endregion

        public void CreateWorkflowInstantiationCriteria(string name)
        {
            License.Validator.Instance.ValidateRuntimeRestriction();
            var criteria = TheWorkflowInstantiationCriteriaFactory.Create(name);
            TheWorkflowInstantiationCriteriaRepository.Create(criteria);
        }

        public IList<IWorkflowInstantiationCriteria> List()
        {
            License.Validator.Instance.ValidateRuntimeRestriction();
            return TheWorkflowInstantiationCriteriaRepository.List();
        }

        public IWorkflowInstantiationCriteria GetCriteria(int id)
        {
            License.Validator.Instance.ValidateRuntimeRestriction();
            var criteria = TheWorkflowInstantiationCriteriaRepository.GetById(id);
            return TheWorkflowInstantiationCriteriaRepository.RestoreState(criteria);
        }

        public void Delete(int id)
        {
            var instantiationCriteria = TheWorkflowInstantiationCriteriaRepository.GetById(id);
            TheWorkflowInstantiationCriteriaRepository.Delete(instantiationCriteria);
        }

        public void Save(IWorkflowInstantiationCriteria workflowInstantiationCriteria)
        {
            TheWorkflowInstantiationCriteriaRepository.Update(workflowInstantiationCriteria);
        }

        public void SetConfigurationProperties(int criteriaId, IDictionary<string, object> properties)
        {
            var config = GetCriteria(criteriaId);
            var instantiationCriteria = TheWorkflowInstantiationCriteriaRepository.RestoreState(config);

            instantiationCriteria = (IWorkflowInstantiationCriteria)Helper.Instance.SetProperties(instantiationCriteria, properties);
            TheWorkflowInstantiationCriteriaRepository.Update(instantiationCriteria);
        }

        public IEnumerable<IWorkflowInstantiationCriteria> GetCriteriaForEvents(string eventName)
        {
            License.Validator.Instance.ValidateRuntimeRestriction();
            var criterias = new List<IWorkflowInstantiationCriteria>();
            foreach(var criteria in List())
            {
                var hydrated = TheWorkflowInstantiationCriteriaRepository.RestoreState(criteria);
                if(hydrated.Events.Contains(eventName))
                {
                    criterias.Add(hydrated);
                }
            }
            return criterias;
        }
    }
}
