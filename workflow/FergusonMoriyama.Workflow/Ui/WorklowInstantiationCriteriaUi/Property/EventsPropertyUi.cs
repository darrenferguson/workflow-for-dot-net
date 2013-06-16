using System.Collections.Generic;
using System.Web.UI;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Ui.WorklowInstantiationCriteriaUi.Controls;

namespace FergusonMoriyam.Workflow.Ui.WorklowInstantiationCriteriaUi.Property
{
    public class EventsPropertyUi : PropertyUi, IWorkflowUiProperty, IGlobalisable
    {
        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }

        public EventsPropertyUi()
        {
            RenderControl = new AvailableEventsDropDownList { ID = PropertyName, CssClass = "eventsListBox" };
        }
       
        public string PropertyName
        {
            get { return "Events"; }
        }

        public Control RenderControl { get; private set; }

        public string Label
        {
            get { return TheGlobalisationService.GetString("instantiating_events"); }
        }

        public object Value
        {
            get { return ((AvailableEventsDropDownList) RenderControl).GetValue(); }
            set { ((AvailableEventsDropDownList) RenderControl).SetValue((List<string>) value); }
        }
    }
}
