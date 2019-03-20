using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WFRP_NPC_Creator
{
    public class RerollCommand : ICommand
    {
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        private Action<RowAction> Reroll;

        public RerollCommand(Action<RowAction> reroll)
        {
            Reroll = reroll;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Reroll.Invoke((RowAction)parameter);
        }
    }
}
