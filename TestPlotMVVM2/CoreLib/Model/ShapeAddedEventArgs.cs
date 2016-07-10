using Controls.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Model
{
    public class ShapeAddedEventArgs : EventArgs
    {
        public IShape NewShape { get; private set; }

        public ShapeAddedEventArgs(IShape newShape)
        {
            this.NewShape = newShape;
        }
    }
}
