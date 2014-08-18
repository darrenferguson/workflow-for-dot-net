using System;
using System.Collections.Generic;
using Moriyama.Workflow.Interfaces.Domain;
using Moriyama.Workflow.Interfaces.Domain.Factory;

namespace Moriyama.Workflow.Domain.Factory
{
    public class WorkflowConfigurationFactory : IWorkflowConfigurationFactory
    {

        private static readonly WorkflowConfigurationFactory ConfigurationFactory = new WorkflowConfigurationFactory();

        public static WorkflowConfigurationFactory Instance
        {
            get { return ConfigurationFactory; }
        }

        public IWorkflowConfiguration Create(string typeSpec, string name)
        {
            var workflowConfiguration = (IWorkflowConfiguration) Activator.CreateInstance(Type.GetType(typeSpec));
            workflowConfiguration.Name = name;

            workflowConfiguration.Tasks = new List<IWorkflowTask>();
            workflowConfiguration.TypeName = workflowConfiguration.GetType().FullName;

            return workflowConfiguration;
        }
    }
}
