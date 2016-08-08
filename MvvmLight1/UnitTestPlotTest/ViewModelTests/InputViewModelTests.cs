using CoreLibrary.DataAccess;
using CoreLibrary.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlotTestUnitTests.ViewModelTests
{
    [TestClass]
    public class InputViewModelTests
    {
        protected WorkspaceViewModel workspaceViewModel;
        protected InputViewModel inputViewModel;
        protected DataRepository data;

        [TestInitialize]
        public void TestInitialize()
        {
            data = new DataRepository();
        }

    }
}
