using System;
using System.Collections.Generic;
using System.Reflection;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Interfaces.Ui.Adapter;
using FergusonMoriyam.Workflow.Interfaces.Ui.Factory;
using FergusonMoriyam.Workflow.Ui.Adapter;
using Common.Logging;

namespace FergusonMoriyam.Workflow.Ui.Factory
{
    public class WorkflowTaskUiAdapterFactory : IWorkflowTaskUiAdapterFactory
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly WorkflowTaskUiAdapterFactory Factory = new WorkflowTaskUiAdapterFactory();

        public static WorkflowTaskUiAdapterFactory Instance
        {
            get { return Factory; }
        }

        public IWorkflowTaskUiAdapter CreateWorkflowTaskUiAdapter(IWorkflowTask adaptedTask)
        {
            var t = adaptedTask.GetType();
            Log.Debug(string.Format("Getting adapater for {0}", t.AssemblyQualifiedName));

            var w = new WorkflowTaskUiAdapter
                        {
                            AvailableTransitions = adaptedTask.AvailableTransitions.Count,
                            TypeName = t.FullName,
                            AssemblyQualifiedTypeName = t.AssemblyQualifiedName, 
                            IsStartTask = adaptedTask.IsStartTask
                            
                        };

            Log.Debug("Finding WorkflowTaskEntityUi");
            var entityUi = (IWorkflowTaskEntityUi)WorkflowEntityUiResolver.Instance.Resolve(adaptedTask);

            if (entityUi == null)
            {
                var message = string.Format("No IWorkflowTaskEntityUi defined for {0}", t.AssemblyQualifiedName);
                Log.Fatal(message);
                throw new Exception(message); 
            }

            Log.Debug(string.Format("Got entity Ui {0} for {1}", entityUi.GetType().AssemblyQualifiedName, t.AssemblyQualifiedName));

            w.Name = entityUi.EntityName;
            w.Class = entityUi.UiAttributes.ContainsKey("class") ? entityUi.UiAttributes["class"] : string.Empty;

            w.TransitionDescriptions = new Dictionary<string, string>();

            foreach (var transition in adaptedTask.AvailableTransitions)
            {
                w.TransitionDescriptions.Add(transition, entityUi.TransitionDescriptions[transition]);
            }

            return w;
        }
    }
}
