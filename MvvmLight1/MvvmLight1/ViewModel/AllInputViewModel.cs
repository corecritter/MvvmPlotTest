﻿using CoreLibrary.DataAccess;
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
        //public ObservableCollection<InputViewModel> AllInputs { get; set; }
        //public ObservableCollection<CommandViewModel> _commands { get; set; }

        //readonly DataRepository _dataRepository;

        public AllInputViewModel(DataRepository dataRepository) : base(dataRepository)
        {
            //this.AllInputs = new ObservableCollection<InputViewModel>();
            this.AllInputs.CollectionChanged += this.OnCollectionChanged;

            //this._dataRepository = dataRepository;
            this._dataRepository.ShapeAdded += this.OnShapeAddedToRepository;
            for (int i = 0; i < 5; i++)
            {
                PointInputViewModel temp = new PointInputViewModel(null, null);
                PointSetShape testPoint = new PointSetShape();
                testPoint.x1 = i;
                testPoint.x2 = i + 1;
                testPoint.y1 = i + 2;
                testPoint.y2 = i + 3;
                this._dataRepository.AddShape(temp.GetType(), testPoint);
            }
            PointInputViewModel temp2 = new PointInputViewModel(null, null);
            PointSetShape testPoint2 = new PointSetShape();
            testPoint2.x1 = -1;
            testPoint2.x2 = 1;
            testPoint2.y1 = -1;
            testPoint2.y2 = 1;
            this._dataRepository.AddShape(temp2.GetType(), testPoint2);
            //base.DisplayName = "All Inputs";
        }


        void OnShapeAddedToRepository(object sender, ShapeAddedEventArgs e)
        {
            if (e.SenderType == typeof(PointInputViewModel))
            {
                var viewModel = Activator.CreateInstance(e.SenderType, new object[] { _dataRepository, e.NewShape });
                //var viewModel = new PointInputViewModel(_dataRepository, (IPointSet)e.NewShape);
                this.AllInputs.Add((InputViewModel)viewModel);
            }
        }
    }
}
