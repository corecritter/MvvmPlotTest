using BaseViewModels.ViewModel;
using CoreLibrary.DataAccess;
using CoreLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight1.ViewModel
{
    public class LineInputViewModel : WorkspaceViewModel
    {
        readonly IShape _shape;
        readonly DataRepository _dataRepository;
        private bool _isSelected;

        public LineInputViewModel(DataRepository dataRepository, IShape shape)
        {
            this._dataRepository = dataRepository;
            _shape = shape == null ? new PointSetShape() : shape;
        }
    }
}
