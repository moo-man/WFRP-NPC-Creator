using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WFRP_NPC_Creator
{
    public class DragCommand : ICommand
    {
        public event EventHandler CanExecuteChanged = (sender, e) => {};

        private Action _action;

        public DragCommand(Action drag)
        {
            _action = drag;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action.Invoke();
        }
    }
}
