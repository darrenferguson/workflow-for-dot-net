using System.Collections.Generic;
using System.Web.UI;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Ui;
using Moriyama.Workflow.Umbraco6.Ui.Controls;

namespace Moriyama.Workflow.Umbraco6.Ui.Property
{
    public class CriteriaOperandEntityUi : PropertyUi, IWorkflowUiProperty, IGlobalisable
    {
        public CriteriaOperandEntityUi()
        {
            RenderControl = new CriteriaOperandList { ID = PropertyName };
        }

        public string PropertyName
        {
            get { return "CriteriaOperand"; }
        }

        public Control RenderControl { get; private set; }

        public string Label
        {
            get { return TheGlobalisationService.GetString("criteria_operand"); }
        }

        public object Value
        {
            get { return ((CriteriaOperandList)RenderControl).GetValue(); }
            set { ((CriteriaOperandList)RenderControl).SetValue((string) value); }
        }

        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }
    }
}