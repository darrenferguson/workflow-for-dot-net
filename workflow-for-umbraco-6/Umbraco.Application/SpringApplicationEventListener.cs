using FergusonMoriyam.Workflow.Interfaces.Application.Event;
using Spring.Context;
using Spring.Context.Events;

namespace FergusonMoriyam.Workflow.Umbraco.Application
{
    public class SpringApplicationEventListener : IApplicationEventListener 
    {

        public void HandleApplicationEvent(object sender, ApplicationEventArgs e)
        {
            var eventArgs = e as ContextRefreshedEventArgs;

            if (eventArgs == null) return;

            var args = eventArgs;

            if (args.Event != ContextEventArgs.ContextEvent.Refreshed) return;

            var ctx = (IApplicationContext)sender;
            var eventService = (IEventService)ctx.GetObject("EventService");
            eventService.RegisterEvents();
        }
    }
}
