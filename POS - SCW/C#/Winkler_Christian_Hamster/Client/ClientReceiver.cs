using Comms;
using System;

namespace Client
{
    public class ClientReceiver : Receiver
    {
        private MainWindow window;

        public ClientReceiver(MainWindow window)
        {
            this.window = window;
        }

        public void AddDebugInfo(Transfer t, string m, bool sent)
        {
            Console.WriteLine("ClientReceiver: " + t.GetIP() + " (" + (sent ? "sent" : "received") + ") " + m);
        }

        public void ReceiveMessage(Message m, Transfer t)
        {
            Console.WriteLine("ClientReceiver: Received message");
            if (m.Error != null)
            {
                window.Error = m.Error;
            }
        }

        public void TransferDisconnected(Transfer t)
        {
            Console.WriteLine("ClientReceiver: Transfer disconnected");
            window.Connected = false;
        }
    }
}
