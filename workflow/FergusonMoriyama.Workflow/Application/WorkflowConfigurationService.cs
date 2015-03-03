using System;
using System.Collections.Generic;
using System.Reflection;
using FergusonMoriyam.Workflow.Application.Reflection;
using System.Linq;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using FergusonMoriyam.Workflow.Interfaces.Domain.Factory;
using FergusonMoriyam.Workflow.Interfaces.Infrastructure;
using log4net;

namespace FergusonMoriyam.Workflow.Application
{
    public class WorkflowConfigurationService : IWorkflowConfigurationService
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region singleton
        private static readonly WorkflowConfigurationService ConfigService = new WorkflowConfigurationService();

        public static WorkflowConfigurationService Instance
        {
            get { return ConfigService; }
        }

        private WorkflowConfigurationService() { }
        #endregion

        #region properties
        public Type DefaultTaskType { get; set; }
        public IRepository<IWorkflowConfiguration, int> ConfigurationRepository { get; set; }
        public IWorkflowConfigurationFactory WorkflowConfigurationFactory { get; set; }
        #endregion

        public IWorkflowConfiguration GetConfiguration(int workflowConfigurationId)
        {
            var workflowConfiguration = ConfigurationRepository.GetById(workflowConfigurationId);
            return ConfigurationRepository.RestoreState(workflowConfiguration);  
        }

        public IList<IWorkflowConfiguration> ListConfigurations()
        {
            return ConfigurationRepository.List();
        }

        public void CreateWorkflowConfiguration(string name, string typeName)
        {
            var workflowConfiguration = WorkflowConfigurationFactory.Create(typeName, name);
            ConfigurationRepository.Create(workflowConfiguration);
        }

        public void DeleteWorkflowConfiguration(int workflowConfigurationId)
        {
            var workflowConfiguration = GetConfiguration(workflowConfigurationId);
            ConfigurationRepository.Delete(workflowConfiguration);
        }

        public void SetConfigurationProperties(int workflowConfigurationId, IDictionary<string, object> properties)
        {
            var workflowConfiguration = GetConfiguration(workflowConfigurationId);
            workflowConfiguration = ConfigurationRepository.RestoreState(workflowConfiguration);

            workflowConfiguration = (IWorkflowConfiguration)Helper.Instance.SetProperties(workflowConfiguration, properties);
            ConfigurationRepository.Update(workflowConfiguration);
        }

        public string AddTask(int workflowConfigurationId, string taskType, IDictionary<string, object> properties)
        {
            var taskObj = (IWorkflowTask) Activator.CreateInstance(Type.GetType(taskType));

            if (properties != null) taskObj = (IWorkflowTask)Helper.Instance.SetProperties(taskObj, properties);

            var workflowConfiguration = GetConfiguration(workflowConfigurationId);
            workflowConfiguration.AddTask(taskObj);

            if (taskObj.IsStartTask)
            {
                workflowConfiguration.StartTask = taskObj;
            }

            ConfigurationRepository.Update(workflowConfiguration);
            return taskObj.Id.ToString();
        }

        public void SetTaskProperties(int workflowConfigurationId, string workflowTaskId, IDictionary<string, object> properties)
        {
            var workflowConfiguration = GetConfiguration(workflowConfigurationId);

            var workflowTask = workflowConfiguration.Tasks.Single(task => task.Id == new Guid(workflowTaskId));
            workflowTask = (IWorkflowTask) Helper.Instance.SetProperties(workflowTask, properties);

            workflowConfiguration.UpdateTask(workflowTask);
            ConfigurationRepository.Update(workflowConfiguration);
        }

        public void AddTransition(int workflowConfigurationId, string sourceWorkflowTaskId, string destWorkflowTaskId, string name)
        {
            var workflowConfiguration = GetConfiguration(workflowConfigurationId);
            workflowConfiguration.AddTransition(name, new Guid(sourceWorkflowTaskId), new Guid(destWorkflowTaskId));
            ConfigurationRepository.Update(workflowConfiguration);
        }

        public void RemoveTransition(int workflowConfigurationId, string sourceWorkflowTaskId, string name)
        {
            var workflowConfiguration = GetConfiguration(workflowConfigurationId);
            workflowConfiguration.RemoveTransition(name, new Guid(sourceWorkflowTaskId));
            ConfigurationRepository.Update(workflowConfiguration);
        }

        public void RemoveTransitions(int workflowConfigurationId)
        {
            var workflowConfiguration = GetConfiguration(workflowConfigurationId);
            workflowConfiguration.RemoveTransitions();
            ConfigurationRepository.Update(workflowConfiguration);
        }

        public void RemoveTask(int workflowConfigurationId, string workflowTaskId)
        {
            var workflowConfiguration = GetConfiguration(workflowConfigurationId);
            workflowConfiguration.RemoveTask(new Guid(workflowTaskId));
            ConfigurationRepository.Update(workflowConfiguration);
        }

        public void RemoveTasks(int workflowConfigurationId)
        {
            var workflowConfiguration = GetConfiguration(workflowConfigurationId);
            workflowConfiguration.RemoveTasks();
            ConfigurationRepository.Update(workflowConfiguration);
        }

        public void SaveChanges(int workflowConfigurationId)
        {
            ConfigurationRepository.Update(GetConfiguration(workflowConfigurationId));
        }
    }
}
