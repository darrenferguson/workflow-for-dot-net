using Moriyama.Workflow.Interfaces.Domain.Event;

namespace Moriyama.Workflow.Domain.Event
{
    public class EventInformation : IEventInformation
    {
        public string FullName
        {

            get;
            set;
        }

        public string TypeName
        {
            get;
            set;
        }

        public string TypeAssemblyQualifiedName
        {
            get;
            set;
        }

        public string EventName
        {
            get;
            set;
        }
    }
}
