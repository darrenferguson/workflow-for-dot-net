using System.Reflection;
using FergusonMoriyam.Workflow.Interfaces.Application.Event;
using FergusonMoriyam.Workflow.Interfaces.Domain.Event;
using System.Linq;
using log4net;

namespace FergusonMoriyam.Workflow.Application
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
