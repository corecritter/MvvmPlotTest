using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace CoreLibrary.Model.StandardShapes
{
    public class StandardSquare
    {
        //private MeshGeometry3D meshGeometry3D; //To hold points
        //private Point3DCollection pointCollection3D; //For collection of points
        //private Int32Collection int32Collection; //For triangle vertices
        public StandardSquare()
        {

        }
        public static MeshGeometry3D CreateSquare()
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            Point3DCollection pointCollection3D = new Point3DCollection();
            Int32Collection int32Collection = new Int32Collection();

            pointCollection3D.Add(new Point3D(-1, -1, 0));
            pointCollection3D.Add(new Point3D(-1, 1, 0));
            pointCollection3D.Add(new Point3D(1, -1, 0));
            pointCollection3D.Add(new Point3D(1, 1, 0));

            int32Collection.Add(0); int32Collection.Add(2); int32Collection.Add(1); int32Collection.Add(1); int32Collection.Add(2); int32Collection.Add(3);

            mesh.Positions = pointCollection3D;
            mesh.TriangleIndices = int32Collection;
            return mesh;
        }
    }
}
