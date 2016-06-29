using BaseViewModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlot.ViewModel
{
    public class AllInputViewModel : WorkspaceViewModel
    {
        public ObservableCollection<InputViewModel> _inputWorkspaces { get; set; }
        public ObservableCollection<CommandViewModel> _commands { get; set; }

    }
}
