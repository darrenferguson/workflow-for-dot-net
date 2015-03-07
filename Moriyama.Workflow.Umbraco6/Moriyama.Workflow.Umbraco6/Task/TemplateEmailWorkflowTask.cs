using System;
using Moriyama.Workflow.Interfaces.Application.Runtime;
using Moriyama.Workflow.Interfaces.Domain;
using Moriyama.Workflow.Umbraco6.Application;
using Moriyama.Workflow.Umbraco6.Domain;
using Moriyama.Workflow.Umbraco6.Domain.Task;

namespace Moriyama.Workflow.Umbraco6.Task
{
    [Serializable]
    public class TemplateEmailWorkflowTask : BaseEmailWorkflowTask, IWorkflowTask, IRunnableWorkflowTask
    {
        public int RenderTemplate { get; set; }

        public override void Run(IWorkflowInstance workflowInstance, IWorkflowRuntime runtime)
        {
            base.Run(workflowInstance, runtime);

            var nodes = ((UmbracoWorkflowInstance) workflowInstance).CmsNodes;

            var body = Helper.Instance.RenderTemplate(RenderTemplate) + GetAttachmentLinks(nodes);

            SendMail(body);
            runtime.Transition(workflowInstance, this, "done");
        }
    }
}
