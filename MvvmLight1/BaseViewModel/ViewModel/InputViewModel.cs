using CoreLibrary.DataAccess;
using CoreLibrary.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BaseViewModels.ViewModel
{
    public class InputViewModel : WorkspaceViewModel
    {
        RelayCommand _saveCommand;
        protected IShape _shape;
        //private bool _isSelected;
        public InputViewModel(DataRepository dataRepository) : base(dataRepository)
        {

        }

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(this.Save, null);
                }
                return _saveCommand;
            }
        }

        //public bool IsSelected
        //{
        //    get
        //    {
        //        return this._isSelected;
        //    }
        //    set
        //    {
        //        //if (this.ShapeEdit != null)
        //        //    this.ShapeEdit(this, new ShapeEditEventArgs());
        //        if (value != _isSelected)
        //        {
        //            _isSelected = value;
        //            if (_isSelected)
        //                Messenger.Default.Send<EditMessage>(new EditMessage { ViewModel = this });
        //        }
        //    }
        //}

        public void Save()
        {
            _dataRepository.AddShape(this.GetType(), this._shape);
        }
    }
}
