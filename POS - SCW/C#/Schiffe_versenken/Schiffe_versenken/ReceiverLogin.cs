using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Schiffe_versenken
{
    public class ReceiverLogin : Receiver
    {
        public void ReceiveMessage(MSG m, Transfer t)
        {
            //MessageBox.Show(m.Config.StartPlayer.ToString());
        }
        public void TransferDisconnected(Transfer t)
        {

        }

        public void AddDebugInfo(Transfer t, String m, bool sent)
        {

        }
    }
}
