using CoreLibrary.Model.StandardShapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public GeometryModel3D GetShape()
        {

            //double worldAspect = (rangeX) / rangeY;
            double midX = (x1 + x2) / 2.0;                      //Find midpoint of two points
            double midY = (y1 + y2) / 2.0;
            double length = Math.Sqrt(Math.Pow((x1-x2), 2) + Math.Pow((y1-y2), 2));
            ModelTransformations transformations = new ModelTransformations();
            transformations.scale((length / 2), 10);                                                          //Scale x and y appropriately
            transformations.rotate((180 / Math.PI) * Math.Atan(((y1-y2)) / (midX)));                      //Rotation angle (arcTan of height/width converted to degrees)
            transformations.translate(midX, midY);



            GeometryModel3D model = new GeometryModel3D();
            model.Material = new DiffuseMaterial(Brushes.Green);
            model.Geometry = StandardSquare.CreateSquare();
            model.Transform = transformations.getTransformations();
            return model;
        }
    }
}
