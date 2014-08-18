using System;
using Moriyama.Workflow.Interfaces.Ui.Adapter;

namespace Moriyama.Workflow.Ui.Adapter
{
    class TransitionInfo : ITransitionInfo
    {
        public string Source
        {
            get;
            set;
        }

        public string Target
        {
            get;
            set;
        }

        public string Transition
        {
            get; 
            set; 
        }
    }
}
