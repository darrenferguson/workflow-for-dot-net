using System;
using System.Collections.Generic;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using FergusonMoriyam.Workflow.Interfaces.Infrastructure;

namespace FergusonMoriyam.Workflow.Interfaces.Application
{
    public interface IWorkflowConfigurationService
    {
        Type DefaultTaskType { get; set; }
        IRepository<IWorkflowConfiguration, int> ConfigurationRepository { get; set; }

        IWorkflowConfiguration GetConfiguration(int workflowConfigurationId);
        IList<IWorkflowConfiguration> ListConfigurations();

        void CreateWorkflowConfiguration(string name, string typeName);
        void DeleteWorkflowConfiguration(int id);

        void SetConfigurationProperties(int workflowWorkflowId, IDictionary<string, object> properties);

        string AddTask(int workflowId, string taskType, IDictionary<string, object> properties);
        void SetTaskProperties(int workflowId, string workflowTaskId, IDictionary<string, object> properties);

        void AddTransition(int workflowId, string srcTaskId, string destTaskId, string name);
        void RemoveTransition(int workflowId, string srcTaskId, string name);
        void RemoveTransitions(int workflowId);

        void RemoveTask(int workflowId, string taskId);
        void RemoveTasks(int workflowId);

        void SaveChanges(int workflowId);
    }
}
