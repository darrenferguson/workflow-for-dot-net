using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moriyama.Workflow.Interfaces.Ui.Adapter;

namespace Moriyama.Workflow.Ui.Adapter
{
    public class TransitionInfoCollection : ITransitionInfoCollection
    {
        public IList<ITransitionInfo> Transitions
        {
            get;
            set;
        }
    }
}
