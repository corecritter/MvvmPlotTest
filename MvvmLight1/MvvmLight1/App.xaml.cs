using System.Windows;
using GalaSoft.MvvmLight.Threading;
using MvvmLight1.View;

namespace MvvmLight1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }

        void StartApp(object sender, StartupEventArgs e)
        {
            var mainWindow = new MainView();
            //var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
