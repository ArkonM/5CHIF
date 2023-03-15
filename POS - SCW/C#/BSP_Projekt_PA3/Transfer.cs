using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Xml.Serialization;

namespace BSP_Projekt_PA3
{
    public class Transfer
    {
        private TcpClient client;
        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;
        private XmlSerializer xml = new XmlSerializer(typeof(MSG));
        private bool running = true;
        public AutoResetEvent auto = new AutoResetEvent(false);
        public MSG m;                                                       //MSG auf Public setzen 

        public Transfer(TcpClient c)
        {
            client = c;
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
                        msg += line + "\n";
                        if (msg.Contains("</MSG>"))
                        {
                            StringReader sr = new StringReader(msg);
                            m = (MSG)xml.Deserialize(sr);
                            auto.Set();
                            msg = "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Stop();
                }
            }
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
