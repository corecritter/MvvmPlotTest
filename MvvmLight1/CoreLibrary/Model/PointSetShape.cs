using CoreLibrary.Model.StandardShapes;
using CoreLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace CoreLibrary.Model
{
    public class PointSetShape : IPointSet
    {
        public double x1 { get; set; }
        public double x2 { get; set; }
        public double y1 { get; set; }
        public double y2 { get; set; }

        public GeometryModel3D GetShape(ScalingFactors scalingFactors)
        {
            double slope = PlotUtilities.CalculateSlope(x1, y1, x2, y2);
            double yInt;
            
            //Find midpoint of two points
            double midX = (x1 + x2) / 2.0;
            double midY = (y1 + y2) / 2.0;

            double length;
            //Length of Line from canvas edge to edge
            if (Double.IsInfinity(slope))
            {
                yInt = 0;
                length = PlotUtilities.CalculateLength(PlotUtilities.CalcUndefinedSlopeEdgePoints(scalingFactors, yInt));
            }
            else
            {
                //Length of segment
                //double length = Math.Sqrt(Math.Pow((x1-x2), 2) + Math.Pow((y1-y2), 2));
                yInt = PlotUtilities.CalculateYIntercept(x1, y1, slope);
                length = PlotUtilities.CalculateLength(PlotUtilities.CalcEdgePoints(scalingFactors, slope, yInt));
            }
           
            ModelTransformations transformations = new ModelTransformations();
            //Rotation angle (arcTan of height/width converted to degrees)
            double rotationAngle = (180 / Math.PI) * Math.Atan(slope);
            
            //Scale x and y appropriately
            double yScaleFactor = PlotUtilities.CalcYObjectScaleFactor(scalingFactors, rotationAngle);
            transformations.scale((length / 2), yScaleFactor);
            
            transformations.rotate(rotationAngle);
            //Translate to midpoint
            transformations.translate(midX, midY);

            GeometryModel3D model = new GeometryModel3D();
            model.Material = new DiffuseMaterial(Brushes.Green);
            model.Geometry = StandardSquare.CreateSquare();
            model.Transform = transformations.getTransformations();
            return model;
        }
    }
}
