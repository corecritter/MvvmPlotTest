using CoreLibrary.DataAccess;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseViewModels.ViewModel
{
    public class WorkspaceViewModel : ViewModelBase
    {
        public ObservableCollection<InputViewModel> AllInputs { get; set; }
        //public ObservableCollection<CommandViewModel> _commands { get; set; }

        public readonly DataRepository _dataRepository;

        public WorkspaceViewModel(DataRepository dataRepository)
        {
            this.AllInputs = new ObservableCollection<InputViewModel>();
            _dataRepository = dataRepository;
        }

        public void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (InputViewModel pointVM in e.NewItems)
                    pointVM.PropertyChanged += this.OnInputViewModelPropertyChanged;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (InputViewModel pointVM in e.OldItems)
                    pointVM.PropertyChanged -= this.OnInputViewModelPropertyChanged;
        }

        public void OnInputViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }
    }
}
