namespace FergusonMoriyam.Workflow.Interfaces.Application.Event
{
    public interface IEventService
    {
        void RegisterEvents();
        void OnEvent(object sender, string eventName, object[] args);
    }
}
