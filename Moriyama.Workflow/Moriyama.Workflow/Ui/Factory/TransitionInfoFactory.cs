using System.Collections.Generic;
using Moriyama.Workflow.Interfaces.Ui.Adapter;
using Moriyama.Workflow.Interfaces.Ui.Factory;
using Moriyama.Workflow.Ui.Adapter;

namespace Moriyama.Workflow.Ui.Factory
{
    class TransitionInfoFactory : ITransitionInfoFactory
    {

        private static readonly TransitionInfoFactory Factory = new TransitionInfoFactory();

        public static TransitionInfoFactory Instance
        {
            get { return Factory; }
        }

        public ITransitionInfo Create(IDictionary<string, object> transitionInfo)
        {
            var t = new TransitionInfo
                        {
                            Source = (string) transitionInfo["source"],
                            Target = (string) transitionInfo["target"],
                            Transition = (string) transitionInfo["transition"]
                        };

            return t;
        }
    }
}
