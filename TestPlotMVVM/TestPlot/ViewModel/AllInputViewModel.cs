using BaseViewModels.ViewModel;
using Controls.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlot.ViewModel
{
    public class AllInputViewModel : WorkspaceViewModel
    {
        public ObservableCollection<PointInputViewModel> AllInputs { get; set; }
        public ObservableCollection<CommandViewModel> _commands { get; set; }

        public AllInputViewModel()
        {
            this.AllInputs = new ObservableCollection<PointInputViewModel>();
            this.AllInputs.CollectionChanged += this.OnCollectionChanged;

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
    }
}
