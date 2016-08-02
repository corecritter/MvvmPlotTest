using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace CoreLibrary.Model
{
    public class SlopeInterceptShape : ILine
    {
        public double slope
        {
            get; set;
        }

        public double yIntercept
        {
            get; set;
        }

        public GeometryModel3D GetShape(ScalingFactors scalingFactors)
        {
            return null;
        }
    }
}
