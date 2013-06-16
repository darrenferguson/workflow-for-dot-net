using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FergusonMoriyam.Workflow.Interfaces.Domain.Event;
using FergusonMoriyam.Workflow.Interfaces.Domain.Event.Factory;
using log4net;

namespace FergusonMoriyam.Workflow.Domain.Event.Factory
{
    public class EventInformationCollectionFactory : IEventInformationCollectionFactory
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly EventInformationCollectionFactory Factory = new EventInformationCollectionFactory();

        public static EventInformationCollectionFactory Instance
        {
            get { return Factory; }
        }

        private EventInformationCollectionFactory() {}

        public IEventInformationCollection Create()
        {
            Log.Info("Discovering events");

            var e = new EventInformationCollection {Events = new List<IEventInformation>()};

            foreach (var t in AppDomain
               .CurrentDomain
               .GetAssemblies().SelectMany(assembly => assembly.GetTypes()))
            {
                foreach (var info in t.GetEvents(BindingFlags.Static |
                                               BindingFlags.Public |
                                               BindingFlags.FlattenHierarchy))
                {

                    e.Events.Add(new EventInformation
                                     {
                                         FullName = t.FullName + "." + info.Name,
                                         EventName = info.Name,
                                         TypeName = t.FullName,
                                         TypeAssemblyQualifiedName = t.AssemblyQualifiedName
                                     });

                }

            }
            
            e.Events = e.Events.OrderBy(x => x.FullName).ToList();
            Log.Info(string.Format("Found {0} events at starup", e.Events.Count));

            return e;

        }

        public IEventInformationCollection Create(string filter)
        {
            var e = (EventInformationCollection) Create();
            filter = filter.ToLower();
            var infos = e.Events.Where(eventInfo => (eventInfo.TypeName + "." + eventInfo.EventName).ToLower().Contains(filter)).ToList();
            e.Events = infos;
            return e;
        }
    }
}
