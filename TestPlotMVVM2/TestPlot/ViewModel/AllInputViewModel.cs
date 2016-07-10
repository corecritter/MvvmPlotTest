using BaseViewModels.ViewModel;
using Controls.DataAccess;
using Controls.Model;
using Controls.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestPlot.ViewModel
{
    public class AllInputViewModel : WorkspaceViewModel
    {
        public ObservableCollection<PointInputViewModel> AllInputs { get; set; }
        public ObservableCollection<CommandViewModel> _commands { get; set; }

        readonly DataRepository _dataRepository;

        public AllInputViewModel(DataRepository dataRepository)
        {
            this.AllInputs = new ObservableCollection<PointInputViewModel>();
            this.AllInputs.CollectionChanged += this.OnCollectionChanged;

            this._dataRepository = dataRepository;
            this._dataRepository.ShapeAdded += this.OnShapeAddedToRepository;
            base.DisplayName = "All Inputs";
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (PointInputViewModel pointVM in e.NewItems)
                    pointVM.PropertyChanged += this.OnInputViewModelPropertyChanged;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (PointInputViewModel pointVM in e.OldItems)
                    pointVM.PropertyChanged -= this.OnInputViewModelPropertyChanged;
        }

        void OnInputViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }

        void OnShapeAddedToRepository(object sender, ShapeAddedEventArgs e)
        {
            var viewModel = new PointInputViewModel(_dataRepository, e.NewShape);
            this.AllInputs.Add(viewModel);
        }

        public ICommand DoubleClickCommand
        {
            get
            {
                return null;
            }
        }
    }
}
