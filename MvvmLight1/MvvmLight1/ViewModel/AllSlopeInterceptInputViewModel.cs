﻿using CoreLibrary.DataAccess;
using CoreLibrary.Model;
using CoreLibrary.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight1.ViewModel
{
    public class AllSlopeInterceptInputViewModel : WorkspaceViewModel
    {
        public AllSlopeInterceptInputViewModel(DataRepository dataRepository) : base(dataRepository)
        {
            //this.AllInputs.CollectionChanged += this.OnCollectionChanged;
            //this._dataRepository.ShapeAdded += this.OnShapeAddedToRepository;
            //this._dataRepository.ShapeDeleted += this.OnShapeDeletedFromRepository;
            this.InputType = typeof(LineInputViewModel);
        }
    }
}
