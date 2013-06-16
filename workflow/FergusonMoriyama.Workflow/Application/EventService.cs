using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FergusonMoriyam.Workflow.Application.Reflection;
using FergusonMoriyam.Workflow.Application.Runtime;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Application.Event;
using FergusonMoriyam.Workflow.Interfaces.Application.Runtime;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using log4net;

namespace FergusonMoriyam.Workflow.Application
{
    public abstract class EventService : IEventService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public IWorkflowInstantiationCriteriaService TheWorkflowInstantiationCriteriaService { get; set; }
        public IWorkflowInstanceService TheWorkflowInstanceService { get; set; }
        public IEventInfoService TheEventInfoService { get; set; }
        
        public IGlobalisationService TheGlobalisationService { get; set; }
        public IWorkflowRuntime TheWorkflowRuntime   { get; set; }

        private IDictionary<string, TracerEx> _tracers;
        private readonly IList<string> _registeredEvents;

        public Helper TheHelper { get; set; }

        private IDictionary<int, DateTime> _lastPublishFires;

        protected EventService()
        {
            _lastPublishFires = new Dictionary<int, DateTime>();
            _registeredEvents = new List<string>();
            Init();
        }

        private void Init()
        {
            _tracers = new Dictionary<string, TracerEx>();
        }

        public void OnEvent(object sender, string eventName, object[] args)
        {
            var name = ((Type)sender).FullName + "." + eventName;

            Log.Debug(string.Format("Got event {0}", name));

            if (!_registeredEvents.Contains(name)) return;

            Log.Debug(string.Format("Checking instantiation criteria for {0}", name));
            var criterias = TheWorkflowInstantiationCriteriaService.GetCriteriaForEvents(name);

            foreach(var criteria in criterias)
            {
                if(!criteria.Active) continue;
                if(!ValidateCriteria(criteria)) continue;

                Log.Debug(string.Format("Found valid criteria '{0}' for event '{1}'", criteria.Name, name));
                
                var inst = TheWorkflowInstanceService.Instantiate(criteria.WorkflowConfiguration);
                
                if(OtherInstancesRunning(inst, args[0]))
                {
                    TheWorkflowInstanceService.DeleteWorkflowInstance(inst.Id);
                    if (criteria.CancelEvent) SetCancelPropertyInEventArgs(args);
                    continue;
                }

                AddInstantiatingObjectsToInstance(args[0], inst);
                TheWorkflowInstanceService.Update(inst);
                NotifyInstantiation(inst);

                if (criteria.CancelEvent) SetCancelPropertyInEventArgs(args);

                TheWorkflowInstanceService.Start(inst.Id);
                TheWorkflowRuntime.RunWorkflows();
            }
        }

        protected virtual void NotifyInstantiation(IWorkflowInstance inst) { }
        protected abstract void AddInstantiatingObjectsToInstance(object sender, IWorkflowInstance inst);
        protected abstract bool ValidateCriteria(IWorkflowInstantiationCriteria criteria);
        protected abstract bool OtherInstancesRunning(IWorkflowInstance inst, object sender);

        protected void SetCancelPropertyInEventArgs(object[] args)
        {
            var eventArgs = args.Where(arg => arg.GetType() == typeof(EventArgs) || arg.GetType().IsSubclassOf(typeof(EventArgs))).SingleOrDefault();
            if (eventArgs == null) return;

            var prop = eventArgs.GetType().GetProperty("Cancel");
            if (prop == null) return;

            prop.SetValue(eventArgs, true, null);
        }

        public void RegisterEvents()
        {
            Init();

            foreach(var criteria in TheWorkflowInstantiationCriteriaService.List())
            {
                var hydratedCriteria = TheWorkflowInstantiationCriteriaService.GetCriteria(criteria.Id);
                if (hydratedCriteria.Events == null) continue;

                foreach (var eve in hydratedCriteria.Events)
                {
                    var eventInfo = TheEventInfoService.GetByFullName(eve);
                    
                    if (_registeredEvents.Contains(eventInfo.FullName)) return;

                    if (!_tracers.ContainsKey(eventInfo.TypeAssemblyQualifiedName))
                    {
                        var t = Type.GetType(eventInfo.TypeAssemblyQualifiedName);
                        _tracers.Add(eventInfo.TypeAssemblyQualifiedName, new TracerEx(t, OnEvent));
                    }
                    var tracer = _tracers[eventInfo.TypeAssemblyQualifiedName];
                    tracer.HookEvent(eventInfo.EventName);
                    _registeredEvents.Add(eventInfo.FullName);
                } 
            }
        }
    }
}
