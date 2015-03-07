using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Moriyama.Workflow.Interfaces.Domain.Event;
using Moriyama.Workflow.Interfaces.Domain.Event.Factory;
using Common.Logging;

namespace Moriyama.Workflow.Domain.Event.Factory
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

            var assemblies = AppDomain
                .CurrentDomain
                .GetAssemblies();


            try
            {
                foreach (var t in assemblies.SelectMany(assembly => assembly.GetTypes()))
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
            }
            catch (ReflectionTypeLoadException ex)
            {
                var sb = new StringBuilder();
                foreach (Exception exSub in ex.LoaderExceptions)
                {
                    sb.AppendLine(exSub.Message);
                    var exFileNotFound = exSub as FileNotFoundException;
                    if (exFileNotFound != null)
                    {
                        if (!string.IsNullOrEmpty(exFileNotFound.FusionLog))
                        {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(exFileNotFound.FusionLog);
                        }
                    }
                    sb.AppendLine();
                }
                var errorMessage = sb.ToString();
                throw new Exception(errorMessage);
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
