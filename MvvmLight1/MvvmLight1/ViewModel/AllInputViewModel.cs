using BaseViewModels.ViewModel;
using CoreLibrary.DataAccess;
using CoreLibrary.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmLight1.ViewModel
{
    public class AllInputViewModel : WorkspaceViewModel
    {
        public ObservableCollection<InputViewModel> AllInputs { get; set; }
        //public ObservableCollection<CommandViewModel> _commands { get; set; }

        readonly DataRepository _dataRepository;

        public AllInputViewModel(DataRepository dataRepository)
        {
            this.AllInputs = new ObservableCollection<InputViewModel>();
            this.AllInputs.CollectionChanged += this.OnCollectionChanged;

            this._dataRepository = dataRepository;
            this._dataRepository.ShapeAdded += this.OnShapeAddedToRepository;
            for (int i = 0; i < 5; i++)
            {
                PointInputViewModel temp = new PointInputViewModel(null, null);
                PointSetShape testPoint = new PointSetShape();
                testPoint.x1 = i;
                testPoint.x2 = i+1;
                testPoint.y1 = i+2;
                testPoint.y2 = i+3;
                this._dataRepository.AddShape(temp.GetType(), testPoint);
            }
            LineInputViewModel temp2 = new LineInputViewModel(null, null);
            SlopeInterceptShape testLine = new SlopeInterceptShape();
            testLine.slope = 1;
            testLine.yIntercept = 5;
            this._dataRepository.AddShape(temp2.GetType(), testLine);
            //base.DisplayName = "All Inputs";
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (InputViewModel pointVM in e.NewItems)
                    pointVM.PropertyChanged += this.OnInputViewModelPropertyChanged;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (InputViewModel pointVM in e.OldItems)
                    pointVM.PropertyChanged -= this.OnInputViewModelPropertyChanged;
        }

        void OnInputViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }

        void OnShapeAddedToRepository(object sender, ShapeAddedEventArgs e)
        {
            var viewModel = Activator.CreateInstance(e.SenderType, new object[] { _dataRepository, e.NewShape });
            //var viewModel = new PointInputViewModel(_dataRepository, (IPointSet)e.NewShape);
            this.AllInputs.Add((InputViewModel)viewModel);
        }
    }
}
