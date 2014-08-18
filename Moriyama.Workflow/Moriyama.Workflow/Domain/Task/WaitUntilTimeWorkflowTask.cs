using System;
using Moriyama.Workflow.Interfaces.Domain;

namespace Moriyama.Workflow.Domain.Task
{
    [Serializable]
    public class WaitUntilTimeWorkflowTask : BaseWorkflowTask, IDelayWorkflowTask
    {
        public WaitUntilTimeWorkflowTask()
            : base()
        {
            AvailableTransitions.Add("done");
        }

        public DateTime StartTime
        {
            get; set;
        }

        public int Hour { get; set; }
        public int Minute { get; set; }

        public bool IsComplete()
        {
            var now = DateTime.Now;
            return Hour >= now.Hour && Minute >= now.Minute;
        }
    }
}
