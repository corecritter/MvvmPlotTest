using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Model
{
    public class ShapeEditEventArgs : EventArgs
    {
        public IShape _shape;
        public ShapeEditEventArgs(IShape shape)
        {
            _shape = shape;
        }
    }
}
