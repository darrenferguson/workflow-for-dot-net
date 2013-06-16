using System.Collections.Generic;

namespace FergusonMoriyam.Workflow.Interfaces.Domain.Event
{
    public interface IEventInformationCollection
    {
        IList<IEventInformation> Events { get; set; }
    }
}
