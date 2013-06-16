using System;
using FergusonMoriyam.Workflow.Interfaces.Ui.Adapter;

namespace FergusonMoriyam.Workflow.Ui.Adapter
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
