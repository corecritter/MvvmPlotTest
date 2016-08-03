using CoreLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CoreLibrary.Utilities
{
    public class PlotUtilities
    {
        /// <summary>
        /// Calculates the Y scaling factor for an individual (Model3D) plot object based on the rotation angle
        /// </summary>
        /// <param name="factors"></param>
        /// <param name="rotationAngle"></param>
        /// <returns></returns>
        public static double CalcYObjectScaleFactor(ScalingFactors factors, double rotationAngle)
        {
            //Max scale
            double wMax = factors.SegmentScale;
            //Min scale
            double wMin = wMax * factors.SceneScalingRatio;

            return ((wMax - wMin) * Math.Cos(rotationAngle) + wMin) + .01;
        }

        public static double CalculateYIntercept(double x1, double y1, double slope)
        {
            return slope * (0 - x1) + y1;
        }

        internal static double CalculateLength(Point[] edgePoints)
        {
            double x1 = edgePoints[0].X;
            double y1 = edgePoints[0].Y;

            double x2 = edgePoints[1].X;
            double y2 = edgePoints[1].Y;

            return Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2));
        }

        /// <summary>
        /// Calculates the Y scaling factor for the entire collection of plot objects (ModelGroup3D) based on the WorldHeight
        /// </summary>
        /// <param name="factors"></param>
        /// <returns></returns>
        public static double CalcYHeightScaleFactor(ScalingFactors factors)
        {
            return ((factors.SceneHeight / factors.WorldHeight) / (factors.SceneWidth / factors.WorldWidth));
        }

        /// <summary>
        /// Calculates points on opposite ends of PlotWindow for scaling
        /// </summary>
        /// <param name="factors"></param>
        /// <param name="slope"></param>
        /// <param name="intercept"></param>
        /// <returns></returns>
        public static Point[] CalcEdgePoints(ScalingFactors factors, double slope, double intercept)
        {
            //y = mx+b
            double maxLength = CalcMaxWidth(factors);
            double x1 = maxLength / 2.0;
            double y1 = slope * x1 + intercept;

            double x2 = -maxLength / 2.0;
            double y2 = slope * x2 + intercept;

            Point P1 = new Point(x1, y1);
            Point P2 = new Point(x2, y2);
            Point[] points = new Point[] { P1, P2 };

            return points;
        }

        public static Point[] CalcZeroSlopeEdgePoints(ScalingFactors factors, double x)
        {
            double maxLength = CalcMaxWidth(factors);
            double y1 = maxLength / 2.0;
            double y2 = -maxLength / 2.0;

            Point[] points = new Point[] { new Point(x, y1), new Point(x, y2) };
            return points;
        }

        public static double CalculateSlope(double x1, double y1, double x2, double y2)
        {
            //return((x2-x1) == 0 ? 0 : (y2 - y1) / (x2 - x1));
            return (y2 - y1) / (x2 - x1);
        }

        public static double CalcMaxWidth(ScalingFactors factors)
        {
            return Math.Max(factors.SceneWidth, factors.SceneHeight);
        }
    }
}
