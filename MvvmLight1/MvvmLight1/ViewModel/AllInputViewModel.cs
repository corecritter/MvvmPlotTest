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
    public class AllInputViewModel : WorkspaceViewModel
    {
        public AllInputViewModel(DataRepository dataRepository) : base(dataRepository)
        {
            this.AllInputs.CollectionChanged += this.OnCollectionChanged;
            this._dataRepository.ShapeAdded += this.OnShapeAddedToRepository;
            this.InputType = typeof(PointInputViewModel);
            //Test Data
            //for (int i = 0; i < 5; i++)
            //{
            //    PointInputViewModel temp = new PointInputViewModel(null, null);
            //    PointSetShape testPoint = new PointSetShape();
            //    testPoint.x1 = i;
            //    testPoint.x2 = i + 1;
            //    testPoint.y1 = i + 2;
            //    testPoint.y2 = i + 3;
            //    this._dataRepository.AddShape(temp.GetType(), testPoint);
            //}
            //PointInputViewModel temp2 = new PointInputViewModel(null, null);
            //PointSetShape testPoint2 = new PointSetShape();
            //testPoint2.x1 = -1;
            //testPoint2.x2 = 1;
            //testPoint2.y1 = -1;
            //testPoint2.y2 = 1;
            //this._dataRepository.AddShape(temp2.GetType(), testPoint2);

            //PointInputViewModel temp3 = new PointInputViewModel(null, null);
            //PointSetShape testPoint3 = new PointSetShape();
            //testPoint3.x1 = -1;
            //testPoint3.x2 = 1;
            //testPoint3.y1 = 1;
            //testPoint3.y2 = -1;
            //this._dataRepository.AddShape(temp3.GetType(), testPoint3);

            //PointInputViewModel temp4 = new PointInputViewModel(null, null);
            //PointSetShape testPoint4 = new PointSetShape();
            //testPoint4.x1 = 0;
            //testPoint4.x2 = 0;
            //testPoint4.y1 = 1;
            //testPoint4.y2 = -1;
            //this._dataRepository.AddShape(temp4.GetType(), testPoint4);
            //base.DisplayName = "All Inputs";
        }
    }
}
