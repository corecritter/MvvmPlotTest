using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TestPlot.View;
using TestPlot.ViewModel;

namespace TestPlot
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainView mainView = new MainView();

            var viewModel = new MainWindowViewModel();

            mainView.DataContext = viewModel;
            mainView.Show();
        }
    }
}
