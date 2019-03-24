using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WFRP_NPC_Creator
{
    public class MsgBoxService : IMsgBoxService
    {
        public bool YesNoBox(string message, string caption)
        {
            MessageBoxResult result = MessageBox.Show(message, caption, MessageBoxButton.YesNoCancel);
            return result.HasFlag(MessageBoxResult.Yes);
        }

        public void ShowNotification(string message)
        {
            MessageBox.Show(message, "Notification", MessageBoxButton.OK);

        }

        public bool AskForConfirmation(string message, string caption)
        {
            throw new NotImplementedException();
        }
    }
}
