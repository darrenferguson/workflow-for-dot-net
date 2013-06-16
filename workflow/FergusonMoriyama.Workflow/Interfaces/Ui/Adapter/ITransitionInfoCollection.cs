using System.Collections.Generic;

namespace FergusonMoriyam.Workflow.Interfaces.Ui.Adapter
{
    public interface ITransitionInfoCollection
    {
        IList<ITransitionInfo> Transitions { get;  }
    }
}
