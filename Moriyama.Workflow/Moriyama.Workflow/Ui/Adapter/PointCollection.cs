using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moriyama.Workflow.Interfaces.Ui.Adapter;

namespace Moriyama.Workflow.Ui.Adapter
{
    public class PointCollection : IPointCollection
    {
        public IDictionary<string, IUiPoint> Points
        {
            get;
            set;
        }
    }
}
