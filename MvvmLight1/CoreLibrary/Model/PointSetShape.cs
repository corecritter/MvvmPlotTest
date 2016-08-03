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
            GeometryModel3D model = new GeometryModel3D();
            ModelTransformations transformations = new ModelTransformations();

            double slope = PlotUtilities.CalculateSlope(x1, y1, x2, y2);
            double yInt;
            
            Point[] edgePoints;
            //Length of Line from canvas edge to edge
            if (Double.IsInfinity(slope))
            {
                yInt = 0;
                edgePoints = PlotUtilities.CalcUndefinedSlopeEdgePoints(scalingFactors, yInt);
            }
            else
            {
                yInt = PlotUtilities.CalculateYIntercept(x1, y1, slope);
                edgePoints = PlotUtilities.CalcEdgePoints(scalingFactors, slope, yInt);
            }
            double length = PlotUtilities.CalculateLength(edgePoints);
            //Rotation angle (arcTan of height/width converted to degrees)
            double rotationAngle = (180 / Math.PI) * Math.Atan(slope);
            //Scale x and y appropriately
            double yScaleFactor = PlotUtilities.CalcYObjectScaleFactor(scalingFactors, rotationAngle);

            transformations.scale((length / 2), yScaleFactor);
            transformations.rotate(rotationAngle);
            Point midpoint = PlotUtilities.CalculateMidpoint(edgePoints[0], edgePoints[1]);
            //Translate to midpoint
            transformations.translate(midpoint.X, midpoint.Y);
            
            model.Material = new DiffuseMaterial(Brushes.Green);
            model.Geometry = StandardSquare.CreateSquare();
            model.Transform = transformations.getTransformations();
            return model;
        }
    }
}
