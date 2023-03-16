using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Xml.Serialization;

namespace Comms
{
    public class Transfer
    {
        private TcpClient client;
        private Receiver receiver;
        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;
        private XmlSerializer xml = new XmlSerializer(typeof(Message));
        private bool running = true;

        public Transfer(TcpClient c, Receiver r)
        {
            client = c;
            receiver = r;
            stream = client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
        }

        private Thread t;

        public string GetIP()
        {
            return client.Connected ? client.Client.RemoteEndPoint.ToString() : null;
        }

        public void Start()
        {
            t = new Thread(Run)
            {
                IsBackground = true
            };
            t.Start();
        }

        public void Stop()
        {
            running = false;
            reader.Close();
            writer.Close();
            stream.Close();
            client.Close();
        }

        private void Run()
        {
            string msg = "";
            while (running && client.Connected)
            {
                try
                {
                    string line = reader.ReadLine();
                    if (line == null)
                        break;
                    if (line.Length > 0)
                    {
                        msg += line + "\n"; //\n wird durch die readline gelöscht
                        if (msg.Contains("</Message>"))
                        {
                            StringReader sr = new StringReader(msg);
                            Message m = (Message)xml.Deserialize(sr);
                            receiver.ReceiveMessage(m, this);
                            receiver.AddDebugInfo(this, msg, false);
                            msg = "";
                        }
                    }
                }
                catch (Exception)
                { }
            }
            receiver.TransferDisconnected(this);
            stream.Close();
            client.Close();
            reader.Close();
            writer.Close();
        }

        public void Send(Message m)
        {
            StringWriter sw = new StringWriter();
            xml.Serialize(sw, m);
            string msg = sw.ToString();
            receiver.AddDebugInfo(this, msg, true);
            writer.WriteLine(msg);
            writer.Flush();
        }
    }
}
