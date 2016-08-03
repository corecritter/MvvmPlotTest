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
            GeometryModel3D model = new GeometryModel3D();
            ////Find midpoint of two points
            //double midX = (x1 + x2) / 2.0;
            //double midY = (y1 + y2) / 2.0;
            ////Length of segment
            //double length = Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2));
            ////Slope
            //double m = (y2 - y1) / (x2 - x1);
            //ModelTransformations transformations = new ModelTransformations();
            ////Scale x and y appropriately
            //double rotationAngle = (180 / Math.PI) * Math.Atan(m);
            //double yScaleFactor = PlotUtilities.CalcYObjectScaleFactor(scalingFactors, rotationAngle);
            //transformations.scale((length / 2), yScaleFactor);
            ////Rotation angle (arcTan of height/width converted to degrees)
            //transformations.rotate(rotationAngle);
            ////Translate to midpoint
            //transformations.translate(midX, midY);

            //GeometryModel3D model = new GeometryModel3D();
            //model.Material = new DiffuseMaterial(Brushes.Green);
            //model.Geometry = StandardSquare.CreateSquare();
            //model.Transform = transformations.getTransformations();
            return model;
        }
    }
}
