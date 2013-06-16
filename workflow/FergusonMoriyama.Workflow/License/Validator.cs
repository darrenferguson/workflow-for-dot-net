using System;

namespace FergusonMoriyam.Workflow.License
{
    public class Validator
    {
        private static readonly Validator Service = new Validator();

        public static Validator Instance
        {
            get { return Service; }
        }

        private Validator()
        {
            StartTime = DateTime.Now;
        }

        protected DateTime StartTime { get; set; }

        protected static bool ThrowExceptionsOnError = true;


        public void ValidateRuntimeRestriction()
        {
        }
    }
}