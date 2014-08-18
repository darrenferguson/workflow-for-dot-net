using System.Collections.Generic;
using Moriyama.Workflow.Interfaces.Ui.Adapter;

namespace Moriyama.Workflow.Interfaces.Ui.Factory
{
    public interface ITransitionInfoFactory
    {
        ITransitionInfo Create(IDictionary<string, object> dict);
    }
}
