using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Windows;
using System.Xaml;

namespace PA3_Client
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
        public MSG m;

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
                    //MessageBox.Show(ex.Message);
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
