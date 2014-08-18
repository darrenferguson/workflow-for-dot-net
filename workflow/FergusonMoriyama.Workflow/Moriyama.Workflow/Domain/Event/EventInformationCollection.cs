using System.Collections.Generic;
using Moriyama.Workflow.Interfaces.Domain.Event;

namespace Moriyama.Workflow.Domain.Event
{
    public class EventInformationCollection : IEventInformationCollection
    {
        public IList<IEventInformation> Events
        {
            get;
            set;
        }
    }
}
