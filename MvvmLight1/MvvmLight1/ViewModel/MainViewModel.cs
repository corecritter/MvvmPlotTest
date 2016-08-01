using Controls.View;
using CoreLibrary.DataAccess;
using CoreLibrary.Message;
using CoreLibrary.Model;
using CoreLibrary.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MvvmLight1.Model;
using System;
using System.Collections.Generic;
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
        private WorkspaceViewModel _currentWorkspace;

        RelayCommand _addCommand;

        private Dictionary<string, Type> _addOptions;
        private string _selectedOption;

        private PlotViewModel _plot;

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
                    var viewModel = msg.ViewModel;
                    ChangeCurrentWorkspace((WorkspaceViewModel)viewModel);
                });
        }

        public Dictionary<string, Type> AddOptions
        {
            get
            {
                if (this._addOptions == null)
                {
                    _addOptions = new Dictionary<string, Type>();
                    _addOptions.Add("Point Set", typeof(PointInputViewModel));
                    _addOptions.Add("Slope Intercept", typeof(LineInputViewModel));
                }
                return this._addOptions;
            }
        }

        public string SelectedOption
        {
            get
            {
                if (String.IsNullOrEmpty(_selectedOption))
                    _selectedOption = "Point Set";
                return _selectedOption;
            }
            set
            {
                if (_selectedOption.Equals(value))
                    return;
                _selectedOption = value;
            }
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
                    AllSlopeInterceptInputViewModel workspace2 = new AllSlopeInterceptInputViewModel(_dataRepository);
                    this.Workspaces.Add(workspace2);
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
        public void Add()
        {
            if (!String.IsNullOrEmpty(_selectedOption))
            {
                var workspace = Activator.CreateInstance(_addOptions[_selectedOption], new object[] { this._dataRepository, null});
                ChangeCurrentWorkspace((WorkspaceViewModel)workspace);
            }
        }

        public void ChangeCurrentWorkspace(WorkspaceViewModel newWorkspace)
        {
            this.Workspaces.Remove(_currentWorkspace);
            this._currentWorkspace = newWorkspace;
            this.Workspaces.Add(this._currentWorkspace);
        }
        #endregion
        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}