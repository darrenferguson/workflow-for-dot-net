using System.Collections.Generic;

namespace Moriyama.Workflow.Interfaces.Ui.Adapter
{
    public interface IPointCollection
    {
        IDictionary<string, IUiPoint> Points { get; }
    }
}
