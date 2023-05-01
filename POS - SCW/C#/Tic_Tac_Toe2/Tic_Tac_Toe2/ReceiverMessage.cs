using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tic_Tac_Toe2
{
    class ReceiverMessage : Receiver
    {
        MainWindow main;
        public ReceiverMessage(MainWindow _main)
        {
            main = _main;
        }

        public void ReceiveMessage(MSG m, Transfer t)
        {
            if (m.Type == MessageType.START)
            {
                main.fillFields();
            } else if (m.Type == MessageType.CLICK)
            {
                main.markEnemyTurn(m.Info.x, m.Info.y);
            } else if (m.Type == MessageType.END)
            {
                main.playingField.IsEnabled = false;
                MessageBox.Show("Du hast leider verloren!", main.ClientOrServer.Content.ToString());
            }
            
        }

        public void TransferDisconnected(Transfer t)
        {
            
        }
    }
}
