using CoreLibrary.DataAccess;
using CoreLibrary.Message;
using CoreLibrary.Model;
using CoreLibrary.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight1.ViewModel
{
    public class LineInputViewModel : InputViewModel
    {
        //readonly ILine _shape;
        //readonly DataRepository _dataRepository;

        public LineInputViewModel(DataRepository dataRepository, ILine shape) : base(dataRepository)
        {
            //this._dataRepository = dataRepository;
            _shape = shape == null ? new SlopeInterceptShape() : shape;
        }

        public double Slope
        {
            get
            {
                return ((ILine)_shape).slope;
            }
            set
            {
                if (value == ((ILine)_shape).slope)
                    return;
                ((ILine)_shape).slope = value;

                base.RaisePropertyChanged("Slope");
                base.RaisePropertyChanged("LineEquation");
            }
        }

        public double YIntercept
        {
            get
            {
                return ((ILine)_shape).yIntercept;
            }
            set
            {
                if (value == ((ILine)_shape).yIntercept)
                    return;
                ((ILine)_shape).yIntercept = value;
                base.RaisePropertyChanged("YIntercept");
                base.RaisePropertyChanged("LineEquation");
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
