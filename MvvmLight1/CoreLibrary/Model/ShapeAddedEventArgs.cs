using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Model
{
    public class ShapeAddedEventArgs : EventArgs
    {
        public object Sender { get; private set; }
        public IShape NewShape { get; private set; }

        public ShapeAddedEventArgs(object sender, IShape newShape)
        {
            this.Sender = sender;
            this.NewShape = newShape;
        }
    }
}
