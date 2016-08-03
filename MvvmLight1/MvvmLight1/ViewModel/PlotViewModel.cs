using CoreLibrary.Constants;
using CoreLibrary.DataAccess;
using CoreLibrary.Model;
using CoreLibrary.Model.StandardShapes;
using CoreLibrary.Utilities;
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

        private ScalingFactors scalingFactors;

        public PlotViewModel(DataRepository dataRepository)
        {
            scalingFactors = new ScalingFactors();
            //position = new Point3D(-40, 40, 40);
            //at = new Vector3D(40, -40, -40);
            position = new Point3D(0, 0, 1);
            at = new Vector3D(0, 0, -1);
            up = new Vector3D(0, 1, 0);

            scalingFactors.WorldWidth = 10;
            scalingFactors.WorldHeight = 10;

            camera = new OrthographicCamera(position, at, up, WorldWidth);
            camera.NearPlaneDistance = 1;
            this._dataRepository = dataRepository;
            this._dataRepository.ShapeAdded += this.OnShapeAddedOrRemoved;
            this._dataRepository.ShapeDeleted += this.OnShapeAddedOrRemoved;
            scalingFactors.SceneWidth = 1;
            scalingFactors.SceneHeight = 1;
        }

        public void OnShapeAddedOrRemoved(object sender, ShapeAddedEventArgs e)
        {
            base.RaisePropertyChanged("Objects");
        }

        public Model3DGroup Objects
        {
            get
            {
                //Add the shapes
                Model3DGroup modelGroup = new Model3DGroup();
                foreach (var shape in this._dataRepository.GetShapes())
                {
                    var plotObject = shape.GetShape(scalingFactors);
                    if (plotObject != null)
                        modelGroup.Children.Add(plotObject);
                }
                //Add the lighting
                DirectionalLight light = new DirectionalLight(Colors.White, new Vector3D(-1, -1, -3));
                modelGroup.Children.Add(light);
                
                //Scale the group
                if (scalingFactors.SceneScalingRatio != 0)
                {
                    ModelTransformations transform = new ModelTransformations();
                    //transform.scale(scalingFactors.sceneScalingRatio);// sceneHeight / worldHeight * scalingFactors.sceneScalingRatio);
                    double scale = PlotUtilities.CalcYHeightScaleFactor(scalingFactors);//((SceneHeight / WorldHeight) / (SceneWidth / WorldWidth));// * 1 / scalingFactors.sceneScalingRatio;
                    transform.scale(scale);
                    var transforms = transform.getTransformations();

                    modelGroup.Transform = transforms;
                }
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
                return scalingFactors.SceneWidth;
            }
        }

        public double SceneHeight
        {
            get
            {
                //return String.Format("Height: {0}", this.sceneHeight.ToString());
                return scalingFactors.SceneHeight;
            }
        }

        public double WorldWidth
        {
            get
            {
                return scalingFactors.WorldWidth;
            }
            set
            {
                scalingFactors.WorldWidth = value;
                camera.Width = scalingFactors.WorldWidth;
                base.RaisePropertyChanged("WorldWidth");
                base.RaisePropertyChanged("Camera");
                ScaleChange();
            }
        }

        public double WorldHeight
        {
            get
            {
                return scalingFactors.WorldHeight;
            }
            set
            {
                scalingFactors.WorldHeight = value;
                base.RaisePropertyChanged("WorldHeight");
                ScaleChange();
            }

        }

        public ScalingFactors ScaleFactors
        {
            get
            {
                return this.scalingFactors;
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
        public void MouseMoveEvent(MouseEventArgs e)
        {
            Point mousePosition = e.GetPosition((Canvas)e.Source);
            mouseX = mousePosition.X;
            mouseY = mousePosition.Y;
            base.RaisePropertyChanged("MouseX");
            base.RaisePropertyChanged("MouseY");
        }

        public void PlotInitializedEvent(RoutedEventArgs e)
        {
            scalingFactors.SceneWidth = ((Canvas)e.Source).ActualWidth;
            scalingFactors.SceneHeight = ((Canvas)e.Source).ActualHeight;

            ScaleChange();
        }

        public void SizeChangedEvent(SizeChangedEventArgs e)
        {
            scalingFactors.SceneWidth = e.NewSize.Width;
            scalingFactors.SceneHeight = e.NewSize.Height;

            ScaleChange();
        }

        private void ScaleChange()
        {
            scalingFactors.SceneAspectRatio = SceneWidth / SceneHeight;
            scalingFactors.SceneScalingRatio = 1 / (((2 / SceneHeight - 1) * (-1)) * scalingFactors.SceneAspectRatio);

            double scale = Math.Max(WorldWidth, WorldHeight);
            scalingFactors.SegmentScale = scale / ScalingConstants.Segment_Scale;
            scalingFactors.PointScale = scale / ScalingConstants.Point_Scale;

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
