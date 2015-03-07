using System;
using Moriyama.Workflow.Interfaces.Application.Runtime;
using Moriyama.Workflow.Interfaces.Domain;

namespace Moriyama.Workflow.Umbraco6.Domain.Task
{
    [Serializable]
    public class BasicEmailWorkflowTask : BaseEmailWorkflowTask, IWorkflowTask, IRunnableWorkflowTask
    {

        public string Body { get; set; }

        public override void Run(IWorkflowInstance workflowInstance, IWorkflowRuntime runtime)
        {
            base.Run(workflowInstance, runtime);

            var nodes = ((UmbracoWorkflowInstance) workflowInstance).CmsNodes;

            SendMail(Body + Environment.NewLine + GetAttachmentLinks(nodes));
            runtime.Transition(workflowInstance, this, "done");
        }
    }
}
