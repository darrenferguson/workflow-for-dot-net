using System.Reflection;
using Common.Logging;
using Moriyama.Workflow.Interfaces.Application.Event;
using Moriyama.Workflow.Interfaces.Domain.Event;
using System.Linq;

namespace Moriyama.Workflow.Application
{
    public class EventInfoService : IEventInfoService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region singleton
        private static readonly EventInfoService Service = new EventInfoService();

        public static EventInfoService Instance
        {
            get { return Service; }
        }

        private EventInfoService()
        {    
        }
        #endregion

        public IEventInformationCollection EventInformation
        {
            get;
            set;
        }

        public IEventInformation GetByFullName(string name)
        {
            return EventInformation.Events.SingleOrDefault(eventInformation => eventInformation.FullName == name);
        }
    }
}
