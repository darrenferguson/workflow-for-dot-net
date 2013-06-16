namespace FergusonMoriyam.Workflow.Interfaces.Domain.Event.Factory
{
    public interface IEventInformationCollectionFactory
    {
        IEventInformationCollection Create();
        IEventInformationCollection Create(string filter);
    }
}
