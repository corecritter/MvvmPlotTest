using CoreLibrary.DataAccess;
using CoreLibrary.Model;
using CoreLibrary.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight1.ViewModel
{
    public class AllSlopeInterceptInputViewModel : WorkspaceViewModel
    {
        //public ObservableCollection<InputViewModel> AllInputs { get; set; }

        //readonly DataRepository _dataRepository;

        public AllSlopeInterceptInputViewModel(DataRepository dataRepository) : base(dataRepository)
        {
            this.AllInputs.CollectionChanged += this.OnCollectionChanged;
            this._dataRepository.ShapeAdded += this.OnShapeAddedToRepository;
            for (int i = 0; i < 5; i++)
            {
                LineInputViewModel temp = new LineInputViewModel(null, null);
                SlopeInterceptShape testLine = new SlopeInterceptShape();
                testLine.slope = i+1;
                testLine.yIntercept = i+2;
                this._dataRepository.AddShape(temp.GetType(), testLine);
            }
        }

        void OnShapeAddedToRepository(object sender, ShapeAddedEventArgs e)
        {
            if (e.SenderType == typeof(LineInputViewModel))
            {
                var viewModel = Activator.CreateInstance(e.SenderType, new object[] { _dataRepository, e.NewShape });
                //var viewModel = new PointInputViewModel(_dataRepository, (IPointSet)e.NewShape);
                this.AllInputs.Add((InputViewModel)viewModel);
            }
        }
    }
}
