using CoreLibrary.DataAccess;
using CoreLibrary.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoreLibrary.ViewModel
{
    public class EditPanelViewModel
    {
        private DataRepository _dataRepository;
        private IShape _shape;

        public EditPanelViewModel(DataRepository dataRepository, IShape shape)
        {
            this._dataRepository = dataRepository;
            this._shape = shape;
        }
        public bool ShowSaveButton
        {
            get
            {
                return !this._dataRepository.ContainsShape(this._shape);
            }
        }
        public bool ShowDeleteButton
        {
            get
            {
                return this._dataRepository.ContainsShape(this._shape);
            }
        }
    }
}
