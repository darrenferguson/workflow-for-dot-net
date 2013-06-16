using System;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClientDependency.Core;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Application.Event;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Umbraco.Web.Ui.Extensions;
using log4net;
using umbraco.BasePages;

[assembly: WebResource("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.Util.js", "text/javascript")]
[assembly: WebResource("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.Autocomplete.js", "text/javascript")]
[assembly: WebResource("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Css.Controls.css", "text/css")]
namespace FergusonMoriyam.Workflow.Umbraco.Web.Ui
{
    public partial class EditInstantiationCriteria : UmbracoEnsuredPage
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

            this.AddResourceToClientDependency("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.Util.js", ClientDependencyType.Javascript);
            this.AddResourceToClientDependency("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.Autocomplete.js", ClientDependencyType.Javascript);
            this.AddResourceToClientDependency("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Css.Controls.css", ClientDependencyType.Css);


        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SaveCriteriaButton.Text = TheGlobalisationService.GetString("save_criteria");

            var criteriaId = Convert.ToInt32(Request["id"]);
            _instantiationCriteria = TheWorkflowInstantiationCriteriaService.GetCriteria(criteriaId);

            Log.Debug(string.Format("Got criteria of type {0}", _instantiationCriteria.GetType()));

            _entityUi = TheWorkflowEntityUiResolver.Resolve(_instantiationCriteria);

            // if (IsPostBack) return;

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

            SavedLiteral.Visible = true;

        }
    }
}