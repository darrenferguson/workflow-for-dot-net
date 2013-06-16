using System.Web.UI;
using FergusonMoriyam.Workflow.Interfaces.Application;

namespace FergusonMoriyam.Workflow.Ui.Base
{
    public abstract class BasePropertyUi : PropertyUi, IGlobalisable
    {
        public Control RenderControl { get; set; }
        public abstract string PropertyName { get; }

        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }
    }
}
