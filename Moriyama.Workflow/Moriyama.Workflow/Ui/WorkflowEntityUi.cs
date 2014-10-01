using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Common.Logging;

namespace Moriyama.Workflow.Ui
{
    public abstract class WorkflowEntityUi : IWorkflowEntityUi, IWorkflowTaskEntityUi
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected WorkflowEntityUi()
        {
            UiAttributes = new Dictionary<string, string>();
            UiProperties = new List<IWorkflowUiProperty>();
            TransitionDescriptions = new Dictionary<string, string>();
        }

        public abstract bool SupportsType(object o);
        public abstract string EntityName { get; }

        public IList<IWorkflowUiProperty> UiProperties { get; set; }
        public IDictionary<string, string> UiAttributes { get; set; }
        public IDictionary<string, string> TransitionDescriptions { get; set; }

        public IList<Control> Render(object o)
        {
            var controls = new List<Control>();
            foreach (var uiProperty in UiProperties)
            {
                var label = new Label { Text = uiProperty.Label };
                controls.Add(label);

                var currentValue = o.GetType().GetProperty(uiProperty.PropertyName).GetValue(o, null);
                
                if (currentValue != null)
                {
                    uiProperty.Value = currentValue;
                }

                controls.Add(uiProperty.RenderControl);

            }
            return controls;
        }

        protected object CreateGlobalisedObject(Type t)
        {
            var globalisableType = typeof(IGlobalisable);

            if (globalisableType.IsAssignableFrom(GetType()) && globalisableType.IsAssignableFrom(t))
            {
                var glob = (IGlobalisable)this;
                if (glob.TheGlobalisationService != null)
                {
                    var o = FormatterServices.GetUninitializedObject(t);

                    ((IGlobalisable)o).TheGlobalisationService = glob.TheGlobalisationService;

                    var ctor = t.GetConstructors()[0];
                    ctor.Invoke(o, new object[] { });
                    return o;
                }
            }

            return Activator.CreateInstance(t);
        }
    }
}
