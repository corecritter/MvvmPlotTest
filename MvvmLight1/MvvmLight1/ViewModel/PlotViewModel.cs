using CoreLibrary.Constants;
using CoreLibrary.DataAccess;
using CoreLibrary.Model;
using CoreLibrary.Model.StandardShapes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        private DataRepository _dataRepository;
        private double mouseX;
        private double mouseY;

        private double sceneWidth;
        private double sceneHeight;

        private double worldWidth;

        private ScalingFactors scalingFactors;

        public PlotViewModel(DataRepository dataRepository)
        {
            //position = new Point3D(-40, 40, 40);
            //at = new Vector3D(40, -40, -40);
            position = new Point3D(0, 0, 1);
            at = new Vector3D(0, 0, -1);
            up = new Vector3D(0, 1, 0);

            worldWidth = 15;

            camera = new OrthographicCamera(position, at, up, worldWidth);
            camera.NearPlaneDistance = 1;
            this._dataRepository = dataRepository;
            sceneWidth = 1;
            sceneHeight = 1;

            scalingFactors = new ScalingFactors();
        }

        public Model3DGroup Objects
        {
            get
            {
                scalingFactors.segmentScale = worldWidth / ScalingConstants.Segment_Scale;
                scalingFactors.pointScale = worldWidth / ScalingConstants.Point_Scale;
                Model3DGroup modelGroup = new Model3DGroup();
                foreach (var shape in this._dataRepository.GetShapes())
                {
                    var o = shape.GetShape(scalingFactors);
                    if (o != null)
                        modelGroup.Children.Add(shape.GetShape(scalingFactors));
                }
                modelGroup.Children.Add(TestSquare());
                DirectionalLight light = new DirectionalLight(Colors.White, new Vector3D(-1, -1, -3));
                modelGroup.Children.Add(light);
                return modelGroup;
            }
        }
        public OrthographicCamera Camera
        {
            get
            {
                return this.camera;
            }
        }
        public double MouseX
        {
            get
            {
                //return String.Format("MouseX: {0}", this.mouseX.ToString());
                return this.mouseX;
            }
            set
            {
                this.mouseX = value;
            }
        }
        public double MouseY
        {
            get
            {
                //return String.Format("MouseY: {0}", this.mouseY.ToString());
                return this.mouseY;
            }
        }
        public double SceneWidth
        {
            get
            {
                //return String.Format("Width: {0}", this.sceneWidth.ToString());
                return this.sceneWidth;
            }
        }

        public double SceneHeight
        {
            get
            {
                //return String.Format("Height: {0}", this.sceneHeight.ToString());
                return this.sceneHeight;
            }
        }

        public double WorldWidth
        {
            get
            {
                return this.worldWidth;
            }
            set
            {
                this.worldWidth = value;
                camera.Width = worldWidth;
                base.RaisePropertyChanged("WorldWidth");
                base.RaisePropertyChanged("Camera");
                base.RaisePropertyChanged("Objects");
                base.RaisePropertyChanged("MouseX");
                base.RaisePropertyChanged("MouseY");
            }
        }
        #region Commands
        public ICommand Initialized
        {
            get
            {
                return new RelayCommand<RoutedEventArgs>(this.PlotInitializedEvent);
            }
        }

        public ICommand MouseMove
        {
            get
            {
                return new RelayCommand<MouseEventArgs>(this.MouseMoveEvent);
            }
        }

        public ICommand SizeChanged
        {
            get
            {
                return new RelayCommand<SizeChangedEventArgs>(this.SizeChangedEvent);
            }
        }
        #endregion

        public void PlotInitializedEvent(RoutedEventArgs e)
        {
            sceneWidth = ((Canvas)e.Source).ActualWidth;
            sceneHeight = ((Canvas)e.Source).ActualHeight;

            base.RaisePropertyChanged("SceneWidth");
            base.RaisePropertyChanged("SceneHeight");
        }

        public void MouseMoveEvent(MouseEventArgs e)
        {
            Point mousePosition = e.GetPosition((Canvas)e.Source);
            mouseX = mousePosition.X;
            mouseY = mousePosition.Y;
            base.RaisePropertyChanged("MouseX");
            base.RaisePropertyChanged("MouseY");
        }
        public void SizeChangedEvent(SizeChangedEventArgs e)
        {
            sceneWidth = e.NewSize.Width;
            sceneHeight = e.NewSize.Height;

            scalingFactors.sceneAspectRatio = sceneWidth / sceneHeight;
            scalingFactors.sceneScalingRatio = 1 / (((2 / sceneHeight - 1) * (-1)) * scalingFactors.sceneAspectRatio);

            base.RaisePropertyChanged("SceneWidth");
            base.RaisePropertyChanged("SceneHeight");
            base.RaisePropertyChanged("Objects");
        }

        private GeometryModel3D TestSquare()
        {
            GeometryModel3D model = new GeometryModel3D();
            model.Geometry = StandardSquare.CreateSquare();

            ModelTransformations transform = new ModelTransformations();
            transform.scale(.5, .5);

            DiffuseMaterial material = new DiffuseMaterial();
            SolidColorBrush brush = new SolidColorBrush(Colors.Black);
            material.Brush = brush;
            model.Material = material;
            model.Transform = transform.getTransformations();
            return model;
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
