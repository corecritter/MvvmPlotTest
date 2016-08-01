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
        RelayCommand _saveCommand;
        protected IShape _shape;
        private bool _isSelected;
        public InputViewModel(DataRepository dataRepository) : base(dataRepository)
        {

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

        public void Save()
        {
            _dataRepository.AddShape(this.GetType(), this._shape);
        }

        public bool CanSave()
        {
            return !_dataRepository.ContainsShape(this._shape);
        }
    }
}
