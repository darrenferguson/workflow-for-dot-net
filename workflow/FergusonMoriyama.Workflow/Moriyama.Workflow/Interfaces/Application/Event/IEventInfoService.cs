using Moriyama.Workflow.Interfaces.Domain.Event;

namespace Moriyama.Workflow.Interfaces.Application.Event
{
    public interface IEventInfoService
    {
        IEventInformationCollection EventInformation { get; set; }
        IEventInformation GetByFullName(string name);
    }
}
