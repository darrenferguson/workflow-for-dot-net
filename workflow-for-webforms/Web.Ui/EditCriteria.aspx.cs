using System;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Application.Event;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using Common.Logging;

namespace Web.Ui
{
    public partial class EditCriteria : Page
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public IGlobalisationService TheGlobalisationService { get; set; }
        
        public IWorkflowInstantiationCriteriaService TheWorkflowInstantiationCriteriaService { get; set; }
        public IUiResolver TheWorkflowEntityUiResolver { get; set; }
        public IEventService TheEventService { get; set; }

        private IWorkflowInstantiationCriteria _instantiationCriteria;
        private IWorkflowEntityUi _entityUi;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            SaveCriteriaButton.Text = TheGlobalisationService.GetString("save_criteria");

            var criteriaId = Convert.ToInt32(Request["id"]);
            _instantiationCriteria = TheWorkflowInstantiationCriteriaService.GetCriteria(criteriaId);
            _entityUi = TheWorkflowEntityUiResolver.Resolve(_instantiationCriteria);

            foreach (var control in _entityUi.Render(_instantiationCriteria))
            {
                CiteriaControlsPanel.Controls.Add(control);
            }
        }

        protected void SaveCriteriaButtonClick(object sender, EventArgs e)
        {
            var values = _entityUi.UiProperties.ToDictionary(uiProperty => uiProperty.PropertyName, uiProperty => uiProperty.Value);

            TheWorkflowInstantiationCriteriaService.SetConfigurationProperties(_instantiationCriteria.Id, values);
            TheEventService.RegisterEvents();
        }
    }
}