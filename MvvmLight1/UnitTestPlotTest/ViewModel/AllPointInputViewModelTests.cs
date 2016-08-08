using CoreLibrary.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmLight1.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight1.ViewModel.Tests
{
    [TestClass()]
    public class AllPointInputViewModelTests
    {
        private static TestContext testContextInstance;
        private AllPointInputViewModel viewModel;

        public static TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [ClassInitialize()]
        public static void AllPointInputViewModelInitialize(TestContext context)
        {
            TestContext = context;
        }

        [TestInitialize()]
        public void Setup()
        {
        }

        [TestMethod()]
        public void AddPointTest()
        {
            DataRepository _data = new DataRepository();

            viewModel = new AllPointInputViewModel(_data);

            Assert.IsTrue(viewModel.AllInputs != null, "AllInputsNull");
        }
    }
}