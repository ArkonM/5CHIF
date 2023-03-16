using Comms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ServerReceiver : Receiver
    {
        public MainWindow window;

        public ServerReceiver(MainWindow window)
        {
            this.window = window;
        }

        public void AddDebugInfo(Transfer t, string m, bool sent)
        {
            Console.WriteLine("ServerReceiver: " + t.GetIP() + " (" + (sent ? "sent" : "received") + ") " + m);
        }

        public void ReceiveMessage(Message m, Transfer t)
        {
            Console.WriteLine("ServerReceiver: Message received.");
            window.Rows = m.FieldSize.Y;
            window.Columns = m.FieldSize.X;
            window.Player = m.Player;
            window.Speed = m.Speed;
            window.UpdateCellsOnDemand();
            window.ExecuteProgram(m.Program);
        }

        public void TransferDisconnected(Transfer t)
        {
            Console.WriteLine("ServerReceiver: Transfer disconnected");
            window.AcceptTcpClient();
        }
    }
}
