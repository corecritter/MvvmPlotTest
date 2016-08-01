using CoreLibrary.DataAccess;
using CoreLibrary.Model;
using CoreLibrary.ViewModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmLight1.ViewModel
{
    public class PointInputViewModel : InputViewModel, IDataErrorInfo
    {
        //readonly IPointSet _shape;
        //readonly DataRepository _dataRepository;
        //RelayCommand _saveCommand;
        public RelayCommand _editCommand;

        public PointInputViewModel(DataRepository dataRepository, IPointSet shape) : base(dataRepository)
        {
            //this._dataRepository = dataRepository;
            _shape = shape == null ? new PointSetShape() : shape;
        }
        #region Properties
        public double X1
        {
            get { return ((IPointSet)_shape).x1; }
            set
            {
                if (value == ((IPointSet)_shape).x1)
                    return;
                ((IPointSet)_shape).x1 = value;

                base.RaisePropertyChanged("X1");
                base.RaisePropertyChanged("CoordPair1");
            }
        }

        public double Y1
        {
            get { return ((IPointSet)_shape).y1; }
            set
            {
                if (value == ((IPointSet)_shape).y1)
                    return;
                ((IPointSet)_shape).y1 = value;

                base.RaisePropertyChanged("Y1");
                base.RaisePropertyChanged("CoordPair1");
            }
        }

        public double X2
        {
            get { return ((IPointSet)_shape).x2; }
            set
            {
                if (value == ((IPointSet)_shape).x2)
                    return;
                ((IPointSet)_shape).x2 = value;

                base.RaisePropertyChanged("X2");
                base.RaisePropertyChanged("CoordPair2");
            }
        }

        public double Y2
        {
            get { return ((IPointSet)_shape).y2; }
            set
            {
                if (value == ((IPointSet)_shape).y2)
                    return;
                ((IPointSet)_shape).y2 = value;
                base.RaisePropertyChanged("Y2");
                base.RaisePropertyChanged("CoordPair2");
            }
        }

        public string CoordPair1
        {
            get { return String.Format("( {0} , {1} )", ((IPointSet)_shape).x1, ((IPointSet)_shape).y1); }
        }

        public string CoordPair2
        {
            get { return String.Format("( {0} , {1} )", ((IPointSet)_shape).x2, ((IPointSet)_shape).y2); }
        }
        #endregion

        #region Commands

        #endregion

        #region IDataErrorInfo
        public string this[string columnName]
        {
            get
            {
                return String.Empty;
                //throw new NotImplementedException();
            }
        }

        public string Error
        {
            get
            {
                return String.Empty;
                //throw new NotImplementedException();
            }
        }
        #endregion
    }
}
