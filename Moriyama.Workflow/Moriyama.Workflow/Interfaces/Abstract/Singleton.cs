namespace Moriyama.Workflow.Interfaces.Abstract
{
    public abstract class Singleton<TClassType> where TClassType : new()
    {
        static Singleton()
        {
        }

        private static readonly TClassType instance = new TClassType();

        public static TClassType Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
