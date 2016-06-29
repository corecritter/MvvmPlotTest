﻿using BaseViewModels.ViewModel;
using Controls.DataAccess;
using Controls.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.ViewModel
{
    public class PointInputViewModel : InputViewModel, IDataErrorInfo
    {
        readonly IShape _shape;
        readonly DataRepository _dataRepository;
        RelayCommand _addCommand;

        public PointInputViewModel(DataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        #region Properties
        public double X1
        {
            get { return _shape.x1; }
            set
            {
                if (value == _shape.x1)
                    return;
                _shape.x1 = value;

                base.OnPropertyChanged("X1");
            }
        }

        public double Y1
        {
            get { return _shape.y1; }
            set
            {
                if (value == _shape.y1)
                    return;
                _shape.y1 = value;

                base.OnPropertyChanged("Y1");
            }
        }

        public double X2
        {
            get { return _shape.x2; }
            set
            {
                if (value == _shape.x2)
                    return;
                _shape.x2 = value;

                base.OnPropertyChanged("X2");
            }
        }

        public double Y2
        {
            get { return _shape.y2; }
            set
            {
                if (value == _shape.y2)
                    return;
                _shape.y2 = value;

                base.OnPropertyChanged("Y2");
            }
        }

        public string CoordPair1
        {
            get { return String.Format("( {0} , {1} )", _shape.x1, _shape.y1); }
        }

        public string CoordPair2
        {
            get { return String.Format("( {0} , {1} )", _shape.x2, _shape.y2); }
        }
        #endregion

        #region IDataErrorInfo
        public string this[string columnName]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        #endregion
    }
}
