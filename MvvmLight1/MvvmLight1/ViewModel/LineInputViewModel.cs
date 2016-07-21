using BaseViewModels.ViewModel;
using CoreLibrary.DataAccess;
using CoreLibrary.Model;
using GalaSoft.MvvmLight.Messaging;
using Message.Message;
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
        private bool _isSelected;

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
            }
        }

        public string LineEquation
        {
            get
            {
                return String.Format("Y = {0} x + {1}", Slope, YIntercept);
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
                //if (this.ShapeEdit != null)
                //    this.ShapeEdit(this, new ShapeEditEventArgs());
                if (value != _isSelected)
                {
                    _isSelected = value;
                    if (_isSelected)
                        Messenger.Default.Send<EditMessage>(new EditMessage { ViewModel = this });
                }
            }
        }
    }
}
