using FergusonMoriyam.Workflow.Interfaces.Domain.Event;

namespace FergusonMoriyam.Workflow.Interfaces.Application.Event
{
    public interface IEventInfoService
    {
        IEventInformationCollection EventInformation { get; set; }
        IEventInformation GetByFullName(string name);
    }
}
