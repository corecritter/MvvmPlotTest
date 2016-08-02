using CoreLibrary.DataAccess;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace MvvmLight1.ViewModel
{
    public class PlotViewModel : ViewModelBase
    {
        private OrthographicCamera camera;
        private Point3D position;
        private Vector3D at;
        private Vector3D up;
        private Viewport3D viewport;
        private DataRepository _dataRepository;

        public PlotViewModel(DataRepository dataRepository)
        {
            position = new Point3D(-40, 40, 40);
            at = new Vector3D(40, -40, -40);



            //position = new Point3D(0, 0, 1);
            //at = new Vector3D(0, 0, -1);
            up = new Vector3D(0, 1, 0);
            camera = new OrthographicCamera(position, at, up, 10);
            camera.NearPlaneDistance = 1;
            this._dataRepository = dataRepository;
        }

        public Viewport3D ViewPort
        {
            get
            {
                if (viewport == null)
                {
                    viewport = new Viewport3D();
                }
                viewport.Children.Clear();
                viewport.Camera = camera;
                //viewport.Children.Add(Objects);
                return viewport;
            }
        }

        public Model3DGroup Objects
        {
            get
            {
                ModelVisual3D objects = new ModelVisual3D();
                Model3DGroup group = new Model3DGroup();
                foreach (var shape in this._dataRepository.GetShapes())
                {
                    var o = shape.GetShape();
                    if (o != null)
                       group.Children.Add(shape.GetShape());
                }
                //objects.Content = group;
                DirectionalLight light = new DirectionalLight(Colors.White, new Vector3D(-1, -1, -3));
                group.Children.Add(light);



                
                //group.Children.Add(m);
                //objects.Content = group;
                //return objects;
                return group;
            }
        }
        public OrthographicCamera Camera
        {
            get
            {
                return this.camera;
            }
        }
        private GeometryModel3D TestObject()
        {
            GeometryModel3D m = new GeometryModel3D();
            //m.Geometry = new MeshGeometry3D()

            MeshGeometry3D mesh = new MeshGeometry3D();
            //mesh.Positions = new 

            Point3DCollection points = new Point3DCollection();
            points.Add(new Point3D(0, 0, 0));
            points.Add(new Point3D(10, 0, 0));
            points.Add(new Point3D(10, 10, 0));
            points.Add(new Point3D(0, 10, 0));
            points.Add(new Point3D(0, 0, 10));
            points.Add(new Point3D(10, 0, 10));
            points.Add(new Point3D(10, 10, 10));
            points.Add(new Point3D(0, 10, 10));

            Int32Collection indices = new Int32Collection();
            indices.Add(0);
            indices.Add(1);
            indices.Add(3);
            indices.Add(1);
            indices.Add(2);
            indices.Add(3);
            indices.Add(0);
            indices.Add(4);
            indices.Add(3);
            indices.Add(4);
            indices.Add(7);
            indices.Add(3);
            indices.Add(4);
            indices.Add(6);
            indices.Add(7);
            indices.Add(4);
            indices.Add(5);
            indices.Add(6);
            indices.Add(0);
            indices.Add(4);
            indices.Add(1);
            indices.Add(1);
            indices.Add(4);
            indices.Add(5);
            indices.Add(1);
            indices.Add(2);
            indices.Add(6);
            indices.Add(6);
            indices.Add(5);
            indices.Add(1);
            indices.Add(2);
            indices.Add(3);
            indices.Add(7);
            indices.Add(7);
            indices.Add(6);
            indices.Add(2);

            mesh.Positions = points;
            mesh.TriangleIndices = indices;
            m.Geometry = mesh;
            DiffuseMaterial material = new DiffuseMaterial();
            SolidColorBrush brush = new SolidColorBrush(Colors.Black);
            material.Brush = brush;
            m.Material = material;
            return m;
        }
    //        <!--<Viewport3D>
    //    <Viewport3D.Camera>
    //        <PerspectiveCamera Position = "-40,40,40" LookDirection="40,-40,-40 " 
    //                     UpDirection="0,0,1" />
    //    </Viewport3D.Camera>
    //    <ModelVisual3D>
    //        <ModelVisual3D.Content>
    //            <Model3DGroup>
    //                <DirectionalLight Color = "White" Direction="-1,-1,-3" />
    //                <GeometryModel3D>
    //                    <GeometryModel3D.Geometry>
    //                        <MeshGeometry3D Positions = "0,0,0 10,0,0 10,10,0 0,10,0 0,0,10 
    //                    10,0,10 10,10,10 0,10,10"
    //                    TriangleIndices="0 1 3 1 2 3  0 4 3 4 7 3  4 6 7 4 5 6 
    //                                     0 4 1 1 4 5  1 2 6 6 5 1  2 3 7 7 6 2"/>
    //                    </GeometryModel3D.Geometry>
    //                    <GeometryModel3D.Material>
    //                        <DiffuseMaterial Brush = "Red" />
    //                    </ GeometryModel3D.Material >
    //                </ GeometryModel3D >
    //            </ Model3DGroup >
    //        </ ModelVisual3D.Content >
    //    </ ModelVisual3D >
    //</ Viewport3D > -->
    }
}
