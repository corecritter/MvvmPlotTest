using CoreLibrary.DataAccess;
using CoreLibrary.Model;
using CoreLibrary.ViewModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmLight1.ViewModel
{
    public class AllPointInputViewModel : WorkspaceViewModel
    {
        public AllPointInputViewModel(DataRepository dataRepository) : base(dataRepository)
        {
            //this.AllInputs.CollectionChanged += this.OnCollectionChanged;
            //this._dataRepository.ShapeAdded += this.OnShapeAddedToRepository;
            //this._dataRepository.ShapeDeleted += this.OnShapeDeletedFromRepository;
            this.InputType = typeof(PointInputViewModel);
        }
    }
}
