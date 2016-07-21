using CoreLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.DataAccess
{
    public class DataRepository
    {
        readonly List<IShape> _shapes;

        public event EventHandler<ShapeAddedEventArgs> ShapeAdded;
        public event EventHandler<ShapeEditEventArgs> ShapeEdit;

        public DataRepository()
        {
            _shapes = new List<IShape>();
        }

        public List<IShape> GetShapes()
        {
            return new List<IShape>(_shapes);
        }

        public bool ContainsShape(IShape shape)
        {
            return _shapes.Contains(shape);
        }

        public void AddShape(IShape shape)
        {
            if (shape == null)
                throw new ArgumentException("null shape");

            if (!ContainsShape(shape))
            {
                this._shapes.Add(shape);

                if (this.ShapeAdded != null)
                    this.ShapeAdded(this, new ShapeAddedEventArgs(shape));
            }
        }

        public void EditShape(IShape shape)
        {
            if (shape == null)
                throw new ArgumentException("null shape");

                if (this.ShapeEdit != null)
                    this.ShapeEdit(this, new ShapeEditEventArgs(shape));
        }
    }
}
