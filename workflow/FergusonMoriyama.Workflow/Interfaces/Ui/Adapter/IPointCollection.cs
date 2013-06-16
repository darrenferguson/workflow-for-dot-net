using System.Collections.Generic;

namespace FergusonMoriyam.Workflow.Interfaces.Ui.Adapter
{
    public interface IPointCollection
    {
        IDictionary<string, IUiPoint> Points { get; }
    }
}
