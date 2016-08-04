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
            double yScale = CalcYHeightScaleFactor(factors);

            //Max scale
            double wMax = factors.SegmentScale;
            //Min scale
            double wMin = wMax * factors.SceneScalingRatio;

            if (wMin > wMin * yScale)
            {
                wMin = wMax * factors.SceneScalingRatio * yScale;
            }
            else
            {
                wMax = wMax / yScale;
            }

            return Math.Min(Math.Max(wMin, wMax), (((wMax - wMin) * Math.Max(0, Math.Cos(rotationAngle)) + wMin) + .01));
            //return (((wMax - wMin) * Math.Cos(rotationAngle) + wMin) + .01);
        }

        /// <summary>
        /// Calculates the Y scaling factor for the entire collection of plot objects (ModelGroup3D) based on the WorldHeight
        /// </summary>
        /// <param name="factors"></param>
        /// <returns></returns>
        public static double CalcYHeightScaleFactor(ScalingFactors factors)
        {
            return ((factors.SceneHeight / factors.WorldHeight) / (factors.SceneWidth / factors.WorldWidth));// * 1 / factors.SceneScalingRatio;
            //Multiplying the return value by 1/sceneScaleRatio would treat the Y and X range the same (if the WorldWidth == WorldHeight (always))
            //* 1 / scalingFactors.sceneScalingRatio
        }

        /// <summary>
        /// Calculates the y intercept.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="slope">The slope.</param>
        /// <returns></returns>
        public static double CalculateYIntercept(double x1, double y1, double slope)
        {
            return slope * (0 - x1) + y1;
        }

        /// <summary>
        /// Calculates the length between two points
        /// </summary>
        /// <param name="edgePoints">The edge points.</param>
        /// <returns></returns>
        internal static double CalculateLength(Point[] edgePoints)
        {
            double x1 = edgePoints[0].X;
            double y1 = edgePoints[0].Y;

            double x2 = edgePoints[1].X;
            double y2 = edgePoints[1].Y;

            return Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2));
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
            //double maxLength = CalcMaxWidth(factors);
            double x1 = factors.WorldWidth / 2.0;
            double y1 = slope * x1 + intercept;

            double x2 = -factors.WorldWidth / 2.0;
            double y2 = slope * x2 - intercept;

            Point P1 = new Point(x1, y1);
            Point P2 = new Point(x2, y2);
            Point[] points = new Point[] { P1, P2 };

            return points;
        }
        /// <summary>
        /// Calculates the edge points when the slope is undefined (vertical line)
        /// </summary>
        /// <param name="factors">The factors.</param>
        /// <param name="x">The x.</param>
        /// <returns></returns>
        public static Point[] CalcUndefinedSlopeEdgePoints(ScalingFactors factors, double x)
        {
            //double maxLength = CalcMaxWidth(factors);
            double y1 = factors.WorldHeight / 2.0;
            double y2 = -factors.WorldHeight / 2.0;

            Point[] points = new Point[] { new Point(x, y1), new Point(x, y2) };
            return points;
        }
        /// <summary>
        /// Calculates the slope based on two points.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <returns></returns>
        public static double CalculateSlope(double x1, double y1, double x2, double y2)
        {
            //return((x2-x1) == 0 ? 0 : (y2 - y1) / (x2 - x1));
            return (y2 - y1) / (x2 - x1);
        }

        /// <summary>
        /// Calculates the rotation angle based on the slope (return value in degrees)
        /// </summary>
        /// <returns></returns>
        public static double CalcRotationAngle(double slope)
        {
            return (180 / Math.PI) * Math.Atan(slope);
        }

        /// <summary>
        /// Calculates the midpoint of two points
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <returns></returns>
        public static Point CalculateMidpoint(double x1, double y1, double x2, double y2)
        {
            double midX = (x1 + x2) / 2.0;
            double midY = (y1 + y2) / 2.0;

            return new Point(midX, midY);
        }
        public static Point CalculateMidpoint(Point point1, Point point2)
        {
            double midX = (point1.X + point2.X) / 2.0;
            double midY = (point1.Y + point2.Y) / 2.0;

            return new Point(midX, midY);
        }

        public static double CalcMaxWidth(ScalingFactors factors)
        {
            return Math.Max(factors.WorldWidth, factors.WorldHeight);
        }
    }
}
