using System.Collections.Generic;
using Moriyama.Workflow.Interfaces.Domain.Designer;
using Moriyama.Workflow.Interfaces.Ui.Adapter;
using Moriyama.Workflow.Interfaces.Ui.Factory;
using Moriyama.Workflow.Ui.Adapter;

namespace Moriyama.Workflow.Ui.Factory
{
    public class PointCollectionFactory : IPointCollectionFactory
    {
        private static readonly PointCollectionFactory Factory = new PointCollectionFactory();

        public static PointCollectionFactory Instance
        {
            get { return Factory; }
        }

        public IPointCollection Create(IEnumerable<IPoint> points)
        {
            var p = new PointCollection {Points = new Dictionary<string, IUiPoint>()};

            foreach(var point in points)
            {
                p.Points.Add(point.OwnerId, new UiPoint {Left = point.X, Top = point.Y});
            }

            return p;
        }
    }
}
