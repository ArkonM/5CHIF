using PA3_Client;
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

namespace PA3_Client
{
    public class Transfer
    {
        private TcpClient client;
        private Receiver receiver;
        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;
        private XmlSerializer xml = new XmlSerializer(typeof(MSG));
        private bool running = true;
        //byte[] buffer = new byte[1000];

        public Transfer(TcpClient c, Receiver r)
        {
            client = c;
            //client.ReceiveTimeout = 2000;
            receiver = r;
            stream = client.GetStream();
            //stream.ReadTimeout = 5000;
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
            //t.Interrupt();
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
                            Application.Current.Dispatcher.Invoke((Action)(delegate
                            {
                                MSG m = (MSG)xml.Deserialize(sr);
                                receiver.ReceiveMessage(m, this);
                            }));
                            msg = "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);

                }
            }
            Application.Current.Dispatcher.Invoke((Action)(delegate
            {
                receiver.TransferDisconnected(this);
            }));
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

        /*
            private void Run()
            {
                try
                {
                    while (running)
                    {
                        String length = reader.ReadLine();
                        int l = int.Parse(length);
                        char [] ca = new char[l];
                        reader.ReadBlock(ca, 0, l);
                        String msg = new String(ca);
                        StringReader sr = new StringReader(msg);
                        MSG m = (MSG)xml.Deserialize(sr);
                        receiver.ReceiveMessage(m, this);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            public void Send(MSG m)
            {
                StringWriter sw = new StringWriter();
                xml.Serialize(sw, m);
                char [] ca = sw.ToString().ToCharArray();
                writer.WriteLine("" + ca.Length);
                writer.Flush();
                writer.Write(ca);
                writer.Flush();
            }
        */
    }
}
