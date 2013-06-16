using System;
using FergusonMoriyam.Workflow.Domain.Task;
using FergusonMoriyam.Workflow.Interfaces.Domain;

namespace CustomDelayTask
{
    [Serializable]
    class MyDelayWorkflowTask : BaseWorkflowTask, IDelayWorkflowTask
    {
        public MyDelayWorkflowTask()
        {
            AvailableTransitions = new[] { "done" };
        }

        public bool IsComplete()
        {
            var now = DateTime.Now;
            var interval = now.Subtract(StartTime).Minutes;

            Log.Debug(string.Format("The workflow started at {0} it is now {1} and the interval is {2} minutes.", StartTime, now, interval));
            return interval >= 1;
        }

        public DateTime StartTime
        {
            get;
            set;
        }
    }
}
