using BaseViewModels.ViewModel;
using Controls.DataAccess;
using Controls.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace TestPlot.ViewModel
{
    public class MainViewModel : WorkspaceViewModel
    {
        ObservableCollection<WorkspaceViewModel> _workspaces;
        //ObservableCollection
        RelayCommand _addCommand;
        DataRepository _dataRepository;

        public MainViewModel()
        {
            base.DisplayName = "From Main View Model";
            this._dataRepository = new DataRepository();
        }

        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (_workspaces == null)
                {
                    _workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _workspaces.CollectionChanged += this.OnWorkSpacesChanged;
                    AllInputViewModel workspace = new AllInputViewModel(_dataRepository);
                    this.Workspaces.Add(workspace);
                    //this.SetActiveWorkspace(workspace);
                }

                return _workspaces;
            }
        }

        void OnWorkSpacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

        }

        void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);

            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }
        #region Commands

        public ICommand AddCommand
        {
            get
            {
                if(_addCommand == null)
                {
                    _addCommand = new RelayCommand(param => this.Add(), null);
                }
                return _addCommand;
            }
        }

        public void Add()
        {
            PointInputViewModel workspace = new PointInputViewModel(this._dataRepository, null);
            this.Workspaces.Add(workspace);
        }
        #endregion

    }
}
