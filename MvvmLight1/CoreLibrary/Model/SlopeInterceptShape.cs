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
            ModelTransformations transformations = new Model.ModelTransformations();

            Point[] edgePoints = PlotUtilities.CalcEdgePoints(scalingFactors, slope, yIntercept);
            Point midpoint = PlotUtilities.CalculateMidpoint(edgePoints[0], edgePoints[1]);

            double length = PlotUtilities.CalculateLength(edgePoints);
            double rotationAngle = PlotUtilities.CalcRotationAngle(slope);
            double yScaleFactor = PlotUtilities.CalcYObjectScaleFactor(scalingFactors, rotationAngle);
            
            transformations.scale((length / 2), yScaleFactor);
            transformations.rotate(rotationAngle);
            transformations.translate(midpoint.X, midpoint.Y);

            model.Material = new DiffuseMaterial(Brushes.Green);
            model.Geometry = StandardSquare.CreateSquare();
            model.Transform = transformations.getTransformations();
            return model;
        }
    }
}
