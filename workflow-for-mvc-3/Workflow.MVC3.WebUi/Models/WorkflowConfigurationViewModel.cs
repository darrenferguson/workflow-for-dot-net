using System;
using System.Collections.Generic;
using FergusonMoriyam.Workflow.Domain;
using FergusonMoriyam.Workflow.Interfaces.Domain;

namespace Workflow.MVC3.WebUi.Models
{
    public class WorkflowConfigurationViewModel
    {
        public WorkflowConfigurationViewModel()
        {
            AvailableConfigurationTypes = new List<Type> { typeof(WorkflowConfiguration) };
            
        }

        public IEnumerable<Type> AvailableConfigurationTypes { get; private set; }
        public IEnumerable<IWorkflowConfiguration> Configurations
        {
            get;
            set;
        }
    }
}