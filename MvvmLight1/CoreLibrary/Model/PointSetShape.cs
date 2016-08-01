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
            GeometryModel3D model = new GeometryModel3D();
            model.Material = new DiffuseMaterial(Brushes.Black);
            model.Geometry = StandardSquare.CreateSquare();
            ModelTransformations transformations = new ModelTransformations();
            return model;
        }
    }
}
