using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace CoreLibrary.Model
{
    public interface IShape
    {
        GeometryModel3D GetShape();
    }
}
