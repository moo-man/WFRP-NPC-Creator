using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFRP_NPC_Creator
{
    public interface IMsgBoxService
    {
        bool YesNoBox(string message, string caption);
        bool AskForConfirmation(string message, string caption);

        void ShowNotification(string message);
        //... etc
    }
}
