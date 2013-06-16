namespace FergusonMoriyam.Workflow.Interfaces.Ui.Adapter
{
    public interface ITransitionInfo
    {
        string Source { get;  }
        string Target { get; }
        string Transition { get; }
    }
}
