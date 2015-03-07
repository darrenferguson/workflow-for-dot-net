using System;
using System.Collections.Generic;
using Moriyama.Workflow.Interfaces.Domain;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Interfaces.Ui.Adapter;
using Moriyama.Workflow.Interfaces.Ui.Factory;
using Moriyama.Workflow.Ui.Adapter;

namespace Moriyama.Workflow.Ui.Factory
{
    public class UiWorkflowTaskFactory : IUiWorkflowTaskFactory
    {
        private static readonly UiWorkflowTaskFactory Factory = new UiWorkflowTaskFactory();

        public static UiWorkflowTaskFactory Instance
        {
            get { return Factory; }
        }

        public IUiWorkflowTask Create(IWorkflowTask task, IUiPoint point)
        {
            var t = task.GetType();


            var entityUi = (IWorkflowTaskEntityUi)WorkflowEntityUiResolver.Instance.Resolve(task);

            var cssClass = entityUi.UiAttributes.ContainsKey("class") ? entityUi.UiAttributes["class"] : string.Empty;

            var w = new UiWorkflowTask
                        {
                            Top = point.Top,
                            Left = point.Left,
                            Description = task.Description,
                            AvailableTransitions = task.AvailableTransitions,
                            Id = task.Id,
                            Name = task.Name,
                            Transitions = task.Transitions,
                            TypeName = t.FullName,
                            AssemblyQualifiedTypeName = t.AssemblyQualifiedName,
                            Class = cssClass,
                            IsStartTask = task.IsStartTask,
                            TransitionDescriptions = new Dictionary<string, string>(),
                            CustomProperties = new Dictionary<string, object>()
                        };

            foreach (var transition in task.AvailableTransitions)
            {
                w.TransitionDescriptions.Add(transition, entityUi.TransitionDescriptions[transition]);
            }
            
            foreach(var p in t.GetProperties())
            {

                if (p.Name == "Name" || p.Name == "Description" || p.Name == "IsStartTask") continue;
                if (
                    p.PropertyType != typeof(string) && 
                    p.PropertyType != typeof(int) &&
                    p.PropertyType != typeof(Boolean) && 
                    p.PropertyType != typeof(IList<string>) &&
                    p.PropertyType != typeof(IList<int>)
                ) continue;



                w.CustomProperties.Add(p.Name,  p.GetValue(task, null));
            }

            return w;
        }
    }
}
