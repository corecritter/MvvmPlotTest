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
    public class LineInputViewModel : InputViewModel
    {
        readonly ILine _shape;
        readonly DataRepository _dataRepository;
        private bool _isSelected;

        public LineInputViewModel(DataRepository dataRepository, ILine shape)
        {
            this._dataRepository = dataRepository;
            _shape = shape == null ? new SlopeInterceptShape() : shape;
        }

        public double Slope
        {
            get
            {
                return _shape.slope;
            }
            set
            {
                if (value == _shape.slope)
                    return;
                _shape.slope = value;

                base.RaisePropertyChanged("Slope");
            }
        }

        public double YIntercept
        {
            get
            {
                return _shape.yIntercept;
            }
            set
            {
                if (value == _shape.yIntercept)
                    return;
                _shape.yIntercept = value;
                base.RaisePropertyChanged("YIntercept");
            }
        }

        public string LineEquation
        {
            get
            {
                return String.Format("Y = {0} x + {1}", Slope, YIntercept);
            }
        }
    }
}
