using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Model
{
    public class ShapeAddedEventArgs : EventArgs
    {
        public Type SenderType { get; private set; }
        public IShape NewShape { get; private set; }

        public ShapeAddedEventArgs(Type senderType, IShape newShape)
        {
            this.SenderType = senderType;
            this.NewShape = newShape;
        }
    }
}
