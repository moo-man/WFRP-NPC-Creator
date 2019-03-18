using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WFRP_NPC_Creator
{
    public class SelectionChangedCommand : ICommand
    {
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        private Action Change;

        public SelectionChangedCommand(Action change)
        {
            Change = change;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Change.Invoke();
        }
    }
}
