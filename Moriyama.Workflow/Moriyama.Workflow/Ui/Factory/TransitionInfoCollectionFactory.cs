using System.Collections.Generic;
using Moriyama.Workflow.Application.Reflection;
using Moriyama.Workflow.Interfaces.Ui.Adapter;
using Moriyama.Workflow.Interfaces.Ui.Factory;
using Moriyama.Workflow.Ui.Adapter;

namespace Moriyama.Workflow.Ui.Factory
{
    public class TransitionInfoCollectionFactory : ITransitionInfoCollectionFactory
    {
        private static readonly TransitionInfoCollectionFactory Factory = new TransitionInfoCollectionFactory();

        public static TransitionInfoCollectionFactory Instance
        {
            get { return Factory; }
        }

        public ITransitionInfoCollection Parse(string json)
        {
            var c = new TransitionInfoCollection {Transitions = new List<ITransitionInfo>()};

            var transitions = (object[]) Helper.Instance.JsSerializer.DeserializeObject(json);
            foreach (Dictionary<string, object> transitionInfo in transitions)
            {
                c.Transitions.Add(TransitionInfoFactory.Instance.Create(transitionInfo));
            }
            
            return c;
        }
    }
}
