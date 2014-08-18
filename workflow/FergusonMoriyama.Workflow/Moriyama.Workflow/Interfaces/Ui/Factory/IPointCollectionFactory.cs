using System.Collections.Generic;
using Moriyama.Workflow.Interfaces.Domain.Designer;
using Moriyama.Workflow.Interfaces.Ui.Adapter;

namespace Moriyama.Workflow.Interfaces.Ui.Factory
{
    public interface IPointCollectionFactory
    {
        IPointCollection Create(IEnumerable<IPoint> points);
    }
}
