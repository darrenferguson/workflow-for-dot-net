using System.Collections.Generic;

namespace Moriyama.Workflow.Interfaces.Ui.Adapter
{
    public interface ITransitionInfoCollection
    {
        IList<ITransitionInfo> Transitions { get;  }
    }
}
