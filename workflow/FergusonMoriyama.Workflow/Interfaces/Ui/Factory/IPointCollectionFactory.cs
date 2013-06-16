using System.Collections.Generic;
using FergusonMoriyam.Workflow.Interfaces.Domain.Designer;
using FergusonMoriyam.Workflow.Interfaces.Ui.Adapter;

namespace FergusonMoriyam.Workflow.Interfaces.Ui.Factory
{
    public interface IPointCollectionFactory
    {
        IPointCollection Create(IEnumerable<IPoint> points);
    }
}
