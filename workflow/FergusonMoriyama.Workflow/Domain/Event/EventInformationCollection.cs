using System.Collections.Generic;
using FergusonMoriyam.Workflow.Interfaces.Domain.Event;

namespace FergusonMoriyam.Workflow.Domain.Event
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
