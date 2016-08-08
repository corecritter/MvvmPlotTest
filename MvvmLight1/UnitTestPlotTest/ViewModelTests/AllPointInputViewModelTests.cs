using CoreLibrary.DataAccess;
using CoreLibrary.Model;
using CoreLibrary.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmLight1.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PlotTestUnitTests.ViewModelTests
{
    [TestClass()]
    public class AllPointInputViewModelTests : InputViewModelTests
    {
        //private static TestContext testContextInstance;

        //public static TestContext TestContext
        //{
        //    get
        //    {
        //        return testContextInstance;
        //    }
        //    set
        //    {
        //        testContextInstance = value;
        //    }
        //}

        //[ClassInitialize()]
        //public static void AllPointInputViewModelInitialize(TestContext context)
        //{
        //    TestContext = context;
        //}

        [TestInitialize()]
        public void Setup()
        {
            //data = new DataRepository();
            workspaceViewModel = new AllPointInputViewModel(data);
            //Create a new test PointSetShape
            PointSetShape point = new PointSetShape();
            point.x1 = 0;
            point.y1 = 0;
            point.x2 = 0;
            point.y2 = 0;
            inputViewModel = new PointInputViewModel(data, point);
        }

        [TestMethod()]
        public void AllInputsNullTest()
        {
            Assert.IsTrue(workspaceViewModel.AllInputs != null, "AllInputsNull");
        }

        [TestMethod()]
        public void AddPointTest()
        {
            //Reference to the SaveCommand Property
            ICommand saveCommand = inputViewModel.SaveCommand;

            //Make sure command is returned
            Assert.IsNotNull(saveCommand, "Save Command is NULL");

            //Test to make sure shape added event was fired
            bool addedEvent = false;
            EventHandler<ShapeAddedEventArgs> shapeAddedHandler = (sender, e) =>
            {
                if(e.Sender.GetType().Equals(workspaceViewModel.InputType))
                    addedEvent = true;
            };
            data.ShapeAdded += shapeAddedHandler;

            //Test to make sure collection changed event was fired
            bool collChangedEvent = false;
            NotifyCollectionChangedEventHandler collChangeHandler = (sender, e) =>
            {
                collChangedEvent = true;
            };
            workspaceViewModel.AllInputs.CollectionChanged += collChangeHandler;

            //Execute the save command
            saveCommand.Execute(null);

            //Make sure added event is fired
            Assert.IsTrue(addedEvent, "Shape Added Event not fired");

            //Make sure collection changed event was fired
            Assert.IsTrue(collChangedEvent, "Collection Changed Event Not Fired");

            //Make sure shape was added to collection
            Assert.IsTrue(workspaceViewModel.AllInputs.Count != 0, "Shape not added to collection");
        }

        [TestMethod()]
        public void RemovePointTest()
        {
            //Add the test shape
            inputViewModel.SaveCommand.Execute(null);

            ICommand deleteCommand = inputViewModel.DeleteCommand;
            Assert.IsNotNull(deleteCommand, "Delete Command is NULL");

            bool deletedEvent = false;
            EventHandler<ShapeAddedEventArgs> shapeDeletedHandler = (sender, e) =>
            {
                if (e.Sender.GetType().Equals(workspaceViewModel.InputType))
                    deletedEvent = true;
            };
            data.ShapeDeleted += shapeDeletedHandler;
            
            bool collChangedEvent = false;
            NotifyCollectionChangedEventHandler collChangeHandler = (sender, e) =>
            {
                collChangedEvent = true;
            };
            workspaceViewModel.AllInputs.CollectionChanged += collChangeHandler;

            deleteCommand.Execute(null);

            //Make sure events were fired
            Assert.IsTrue(deletedEvent, "Shape deleted event not fired");
            Assert.IsTrue(collChangedEvent, "Collection changed event not fired");
            //Makse sure shape removed from collection
            Assert.IsTrue(workspaceViewModel.AllInputs.Count == 0, "Shape not removed from collection");
        }

        [TestMethod()]
        public void PropertyChangedTest()
        {
            //Add the test shape
            inputViewModel.SaveCommand.Execute(null);

            List<string> propertiesChanged = new List<string>();
            inputViewModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                propertiesChanged.Add(e.PropertyName);
            };

            //Set Properties, confirm Property Changed Event being raised
            ((PointInputViewModel)inputViewModel).X1 = 1;
            Assert.AreEqual(2, propertiesChanged.Count, "Missing Property Changed Event");
            Assert.IsTrue(propertiesChanged[0].Equals("X1"), "X1 Property Changed");
            Assert.IsTrue(propertiesChanged[1].Equals("CoordPair1"), "CoordPair1 Property Changed");
            propertiesChanged.Clear();

            ((PointInputViewModel)inputViewModel).Y1 = 1;
            Assert.AreEqual(2, propertiesChanged.Count, "Missing Property Changed Event");
            Assert.IsTrue(propertiesChanged[0].Equals("Y1"), "Y1 Property Changed");
            Assert.IsTrue(propertiesChanged[1].Equals("CoordPair1"), "CoordPair1 Property Changed");
            propertiesChanged.Clear();

            ((PointInputViewModel)inputViewModel).X2 = 1;
            Assert.AreEqual(2, propertiesChanged.Count, "Missing Property Changed Event");
            Assert.IsTrue(propertiesChanged[0].Equals("X2"), "X2 Property Changed");
            Assert.IsTrue(propertiesChanged[1].Equals("CoordPair2"), "CoordPair2 Property Changed");
            propertiesChanged.Clear();

            ((PointInputViewModel)inputViewModel).Y2 = 1;
            Assert.AreEqual(2, propertiesChanged.Count, "Missing Property Changed Event");
            Assert.IsTrue(propertiesChanged[0].Equals("Y2"), "Y2 Property Changed");
            Assert.IsTrue(propertiesChanged[1].Equals("CoordPair2"), "CoordPair2 Property Changed");
            propertiesChanged.Clear();

            //Set Properties to same value, confirm Property Changed Events are not raised
            ((PointInputViewModel)inputViewModel).X1 = 1;
            Assert.AreEqual(0, propertiesChanged.Count, "Property Changed Event Fired when it should not have");
            ((PointInputViewModel)inputViewModel).Y1 = 1;
            Assert.AreEqual(0, propertiesChanged.Count, "Property Changed Event Fired when it should not have");
            ((PointInputViewModel)inputViewModel).X2 = 1;
            Assert.AreEqual(0, propertiesChanged.Count, "Property Changed Event Fired when it should not have");
            ((PointInputViewModel)inputViewModel).Y2 = 1;
            Assert.AreEqual(0, propertiesChanged.Count, "Property Changed Event Fired when it should not have");
        }

        [TestCleanup]
        public void Cleanup()
        {
            
        }
    }
}