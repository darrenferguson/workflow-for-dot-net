using System;
using FergusonMoriyam.Workflow.Interfaces.Domain;

namespace FergusonMoriyam.Workflow.Domain.Task
{
    [Serializable]
    public class DelayWorkflowTask : BaseWorkflowTask, IDelayWorkflowTask
    {
        public DelayWorkflowTask() : base()
        {
            AvailableTransitions.Add("done");
        }

        public DateTime StartTime
        {
            get;
            set;
        }

        public string Unit
        {
            get;
            set;
        }

        public int Delay
        {
            get;
            set;
        }

        public bool IsComplete()
        {

            TimeSpan t;
            switch (Unit)
            {
                case "seconds":
                    t = TimeSpan.FromSeconds(Delay);
                    break;
                case "minutes":
                    t = TimeSpan.FromMinutes(Delay);
                    break;
                case "hours":
                    t = TimeSpan.FromHours(Delay);
                    break;
                case "days":
                    t = TimeSpan.FromDays(Delay);
                    break;
                default:
                    t = TimeSpan.FromSeconds(Delay);
                    break;
            }
            return DateTime.Now > StartTime.Add(t);
        }
    }
}
