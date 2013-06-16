using System.Collections.Generic;
using FergusonMoriyam.Workflow.Interfaces.Ui.Adapter;

namespace Workflow.MVC3.WebUi.Models
{
    public class WorkflowDesignViewModel
    {
        public int Id { get; set; }
        public string GuidsJson { get; set; }

        public string TaskInfoJson { get; set; }
        public string ConfigJson { get; set; }

        public ICollection<IWorkflowTaskUiAdapter> AvailableTasks { get; set; }
        public ICollection<IUiWorkflowTask> ConfigurationTasks { get; set; }
    }
}