﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BaseViewModels.ViewModel
{
    public class CommandViewModel : ViewModelBase
    {
        public ICommand Command { get; private set; }
        public CommandViewModel(string displayName, ICommand command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            this.DisplayName = displayName;
            this.Command = command;
        }
    }
}
