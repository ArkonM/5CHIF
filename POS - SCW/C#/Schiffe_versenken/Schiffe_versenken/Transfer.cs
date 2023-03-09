using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using System.Xml.Serialization;

namespace Schiffe_versenken
{
    public class Transfer
    {
        private TcpClient client;
        //private Receiver receiver;
        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;
        private XmlSerializer xml = new XmlSerializer(typeof(MSG));
        private bool running = true;
        public AutoResetEvent auto = new AutoResetEvent(false);
        public MSG m;

        public Transfer(TcpClient c/*, Receiver r*/)
        {
            client = c;
            //receiver = r;
            stream = client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
        }

        private Thread t;

        public String getIP()
        {
            if (client.Connected)
                return client.Client.RemoteEndPoint.ToString();
            return null;
        }

        public void Start()
        {
            t = new Thread(Run);
            t.IsBackground = true;
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

            String msg = "";
            while (running && client.Connected)
            {
                try
                {
                    String line = reader.ReadLine();
                    if (line == null)
                        break;
                    if (line.Length > 0)
                    {
                        msg += line + "\n"; //\n wird durch die readline gelöscht
                        if (msg.Contains("</MSG>"))
                        {
                            StringReader sr = new StringReader(msg);
                            m = (MSG)xml.Deserialize(sr);
                            //receiver.ReceiveMessage(m, this);
                            //MessageBox.Show(m.Config.StartPlayer.ToString());
                            auto.Set();
                            msg = "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }
            //receiver.TransferDisconnected(this);
            stream.Close();
            client.Close();
            reader.Close();
            writer.Close();

        }

        public void Send(MSG m)
        {
            StringWriter sw = new StringWriter();
            xml.Serialize(sw, m);
            String msg = sw.ToString();
            writer.WriteLine(msg);
            writer.Flush();
        }


    }


}
