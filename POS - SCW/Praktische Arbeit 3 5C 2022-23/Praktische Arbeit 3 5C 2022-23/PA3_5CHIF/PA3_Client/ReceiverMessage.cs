using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PA3_Client
{
    public class ReceiverMessage : Receiver
    {

        private MainWindow window;
        public ReceiverMessage(MainWindow window)
        {
            this.window = window;
        }

        public void ReceiveMessage(MSG m, Transfer t)
        {
            if (m.Type == MessageType.SAVE)
            {
                window.revealFields(m);
            }
            else if (m.Type == MessageType.EXPLODE)
            {
                MessageBox.Show("Sie sind leider Explodiert!", "EXPLOSION");
            } else
            {
                MessageBox.Show("Komisches Zeugs!");
            }
        }

        public void TransferDisconnected(Transfer t)
        {

        }
    }
}
