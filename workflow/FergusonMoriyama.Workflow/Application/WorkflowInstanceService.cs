using System.Collections.Generic;
using System.Reflection;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Application.Runtime;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using FergusonMoriyam.Workflow.Interfaces.Infrastructure;
using log4net;


namespace FergusonMoriyam.Workflow.Application
{
    public class WorkflowInstanceService : IWorkflowInstanceService
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region singleton
        private static readonly WorkflowInstanceService Service = new WorkflowInstanceService();
        
        public static WorkflowInstanceService Instance
        {
            get { return Service; }
        }
        

        private WorkflowInstanceService() { }
        #endregion

        #region properties
        public IRepository<IWorkflowInstance, int> TheWorkflowInstanceRepository { get; set; }
        public IRepository<IWorkflowConfiguration, int> TheConfigurationRepository { get; set; }
        public IWorkflowRuntime TheWorkflowRuntime { get; set; }
        #endregion

        protected IWorkflowInstance CreateInstance(int workflowConfigurationId)
        {
            var workflowConfiguration = TheConfigurationRepository.GetById(workflowConfigurationId);
            workflowConfiguration = TheConfigurationRepository.RestoreState(workflowConfiguration);
            var workflowInstance = workflowConfiguration.CreateInstance();
            
            return workflowInstance;
        }
        
        public IList<IWorkflowInstance> ListInstances()
        {
            var workflowInstances = new List<IWorkflowInstance>();
            var allWorkflowInstances = TheWorkflowInstanceRepository.List();

            foreach(var instance in allWorkflowInstances)
            {
                if(instance.Started && !instance.Ended)
                {
                    workflowInstances.Add(TheWorkflowInstanceRepository.RestoreState(instance));
                } else
                {
                    workflowInstances.Add(instance);
                }
            }
            return workflowInstances;
        }

        public IWorkflowInstance Instantiate(int workflowConfigurationId)
        {

            var workflowInstance = CreateInstance(workflowConfigurationId);
            TheWorkflowInstanceRepository.Create(workflowInstance);
            return workflowInstance;
        }

        public IWorkflowInstance Instantiate(int workflowConfigurationId, string comment)
        {
            var workflowInstance = Instantiate(workflowConfigurationId);
            workflowInstance.Comment = comment;
            Log.Info(string.Format("Instantiation comment for '{0}' '{1}' -> '{2}'", workflowInstance.Name, workflowInstance.Id, comment));

            TheWorkflowInstanceRepository.Update(workflowInstance);
            return workflowInstance;
        }

        public IWorkflowInstance Instantiate(int workflowConfigurationId, IDictionary<string, object> properties)
        {

            var workflowInstance = CreateInstance(workflowConfigurationId);
            TheWorkflowInstanceRepository.Create(workflowInstance);
            return workflowInstance;
        }

        public void Update(IWorkflowInstance workflowInstance)
        {
            TheWorkflowInstanceRepository.Update(workflowInstance);
        }

        public void DeleteWorkflowInstance(int workflowInstanceId)
        {
            var workflowInstance = TheWorkflowInstanceRepository.GetById(workflowInstanceId);
            TheWorkflowInstanceRepository.Delete(workflowInstance);
        }

        public void Start(int workflowInstanceId)
        {

            var workflowInstance = TheWorkflowInstanceRepository.GetById(workflowInstanceId);
            var hydratedInstance = TheWorkflowInstanceRepository.RestoreState(workflowInstance);

            TheWorkflowRuntime.Start(hydratedInstance);
        }

        public IWorkflowInstance GetInstance(int workflowInstanceId)
        {
            var workflowInstance = TheWorkflowInstanceRepository.GetById(workflowInstanceId);
            var hyrdatedWorkflowInstance = TheWorkflowInstanceRepository.RestoreState(workflowInstance);
            return hyrdatedWorkflowInstance;
        }
    }
}
