using Moriyama.Workflow.Interfaces.Application.Event;
using Moriyama.Workflow.Umbraco6.Installer.Database;
using Spring.Context;
using Spring.Context.Events;

namespace Moriyama.Workflow.Umbraco6.Application
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
