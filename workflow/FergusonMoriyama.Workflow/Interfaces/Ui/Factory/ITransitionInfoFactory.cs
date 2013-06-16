using System.Collections.Generic;
using FergusonMoriyam.Workflow.Interfaces.Ui.Adapter;

namespace FergusonMoriyam.Workflow.Interfaces.Ui.Factory
{
    public interface ITransitionInfoFactory
    {
        ITransitionInfo Create(IDictionary<string, object> dict);
    }
}
