using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FergusonMoriyam.Workflow.Domain.Task;
using FergusonMoriyam.Workflow.Interfaces.Application.Runtime;
using FergusonMoriyam.Workflow.Interfaces.Domain;

namespace FergusonMoriyam.Workflow.Web.Domain.Task
{
    public class ExampleRunnableTask : BaseWorkflowTask, IRunnableWorkflowTask
    {
        public ExampleRunnableTask()
        {
            AvailableTransitions = new[] {"done"};

            
        }
        
        public void Run(IWorkflowInstance workflowInstance, IWorkflowRuntime runtime)
        {

            Log.Debug("This is where my task logic would happen");

            runtime.Transition(workflowInstance, this, "done");
        }
    }
}
