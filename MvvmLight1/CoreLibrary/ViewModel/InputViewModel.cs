using CoreLibrary.DataAccess;
using CoreLibrary.Message;
using CoreLibrary.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoreLibrary.ViewModel
{
    public class InputViewModel : WorkspaceViewModel
    {
        private RelayCommand _saveCommand;
        private RelayCommand _deleteCommand;
        protected IShape _shape;
        private bool _isSelected;
        private EditPanelViewModel _editPanel;
        public InputViewModel(DataRepository dataRepository) : base(dataRepository)
        {

        }

        //Uses BooleanToVisibilityConverter
        public bool ShowSaveButton
        {
            get
            {
                return !this._dataRepository.ContainsShape(this._shape);
            }
        }

        public bool IsSelected
        {
            get
            {
                return this._isSelected;
            }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    if (_isSelected)
                        Messenger.Default.Send<EditMessage>(new EditMessage { ViewModel = this });
                }
            }
        }

        public EditPanelViewModel EditPanel
        {
            get
            {
                if (this._editPanel == null)
                {
                    this._editPanel = new EditPanelViewModel(this._dataRepository, this._shape);
                }
                return this._editPanel;
            }
        }
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(this.Save, this.CanSave);
                }
                return _saveCommand;
            }
        }
        public void Save()
        {
            _dataRepository.AddShape(this, this._shape);
            //Toggles button visibility
            base.RaisePropertyChanged("EditPanel");
        }

        public bool CanSave()
        {
            return !_dataRepository.ContainsShape(this._shape);
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(this.Delete);
                }
                return _deleteCommand;
            }
        }

        public void Delete()
        {
            if (_dataRepository.RemoveShape(this, this._shape))
                Messenger.Default.Send<DeleteMessage>(new DeleteMessage { ViewModel = this });
        }
    }
}
