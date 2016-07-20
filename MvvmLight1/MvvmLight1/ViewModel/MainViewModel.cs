using BaseViewModels.ViewModel;
using Controls.View;
using CoreLibrary.DataAccess;
using CoreLibrary.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Message.Message;
using MvvmLight1.Model;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace MvvmLight1.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private DataRepository _dataRepository;
        ObservableCollection<WorkspaceViewModel> _workspaces;

        RelayCommand _addCommand;
        /// <summary>
        /// The <see cref="WelcomeTitle" /> property's name.
        /// </summary>
        public const string WelcomeTitlePropertyName = "WelcomeTitle";

        private string _welcomeTitle = string.Empty;

        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string WelcomeTitle
        {
            get
            {
                return _welcomeTitle;
            }
            set
            {
                Set(ref _welcomeTitle, value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService, DataRepository data)
        {
            _dataService = dataService;
            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }

                    WelcomeTitle = item.Title;
                });
            this._dataRepository = data;
            //this._dataRepository = new DataRepository();
            Messenger.Default.Register<EditMessage>(
                this,
                msg =>
                {
                    var viewModel = (PointInputViewModel)msg.ViewModel;
                    if (!this._workspaces.Contains(viewModel))
                        this.Workspaces.Add(viewModel);
                });
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


                    //PointInputViewModel w = new PointInputViewModel(this._dataRepository, null);
                    //this.Workspaces.Add(w);


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
                if (_addCommand == null)
                {
                    _addCommand = new RelayCommand(this.Add, null);
                }
                return _addCommand;
            }
        }

        //public ICommand DoubleClickCommand
        //{
        //    get
        //    {
        //        if(_doubleClickCommand  == null)
        //        {

        //        }
        //        return null;
        //    }
        //}
        public void Add()
        {
            PointInputViewModel workspace = new PointInputViewModel(this._dataRepository, null);
            //workspace.ShapeEdit += this.Edit;
            this.Workspaces.Add(workspace);
        }
        #endregion
        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}