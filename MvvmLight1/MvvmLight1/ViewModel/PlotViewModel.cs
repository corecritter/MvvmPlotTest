using CoreLibrary.DataAccess;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public PlotViewModel(DataRepository dataRepository)
        {
            position = new Point3D(0, 0, 1);
            at = new Vector3D(0, 0, -1);
            up = new Vector3D(0, 1, 0);
            camera = new OrthographicCamera(position, at, up, 0);
            camera.NearPlaneDistance = 1;
            this._dataRepository = dataRepository;
        }

        public ModelVisual3D Objects
        {
            get
            {
                ModelVisual3D objects = new ModelVisual3D();
                Model3DGroup group = new Model3DGroup();
                foreach (var shape in this._dataRepository.GetShapes())
                {
                    var o = shape.GetShape();
                    if(o!=null)
                        group.Children.Add(shape.GetShape());
                }
                objects.Content = group;
                return objects;
            }
        }
    }
}
