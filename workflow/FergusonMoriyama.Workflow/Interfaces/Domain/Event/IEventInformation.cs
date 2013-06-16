namespace FergusonMoriyam.Workflow.Interfaces.Domain.Event
{
    public interface IEventInformation
    {
        string FullName { get; set; }
        string TypeName { get; set; }
        string TypeAssemblyQualifiedName { get; set; }
        string EventName { get; set; }
    }
}
