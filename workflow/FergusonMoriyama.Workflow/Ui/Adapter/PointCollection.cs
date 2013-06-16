using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FergusonMoriyam.Workflow.Interfaces.Ui.Adapter;

namespace FergusonMoriyam.Workflow.Ui.Adapter
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
