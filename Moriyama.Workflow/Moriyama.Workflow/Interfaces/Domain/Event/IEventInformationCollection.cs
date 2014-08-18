using System.Collections.Generic;

namespace Moriyama.Workflow.Interfaces.Domain.Event
{
    public interface IEventInformationCollection
    {
        IList<IEventInformation> Events { get; set; }
    }
}
