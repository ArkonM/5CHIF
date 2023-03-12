using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Net;
using System.Windows.Interop;

namespace Chat
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Transfer t;
        AutoResetEvent sendMSGEvent = new AutoResetEvent(false);
        public MainWindow()
        {
            InitializeComponent();
        }


        void runServer()
        {
            TcpListener listener = null;
            try
            {
                listener = new TcpListener(IPAddress.Any, 12345);
                listener.Start();
            } catch (SocketException e)
            {
                MessageBox.Show(e.ErrorCode + ": " + e.Message);
                Environment.Exit(e.ErrorCode);
            }

            TcpClient client = null;
            NetworkStream netStream = null;
            client = listener.AcceptTcpClient();

            try
            {
                netStream = client.GetStream();
                t = new Transfer(client);
                t.Start();

                MessageBox.Show("connected server");

                t.auto.WaitOne();
                if (t.m.type == MessageType.HELLO)
                {
                    MSG hello = new MSG() { type = MessageType.HELLO };

                    t.Send(hello);

                    Thread senderThread = new Thread(() => sendMSG(t));
                    senderThread.IsBackground = true;
                    senderThread.Start();

                    Thread receiverThread = new Thread(() => getMSG(t));
                    receiverThread.IsBackground = true;
                    receiverThread.Start();

                    t.auto.WaitOne();
                    if (t.m.type == MessageType.BYE)
                    { 
                        t.Stop();
                        MessageBox.Show("Other side disconnected!");
                        this.Close();
                    }
                }
            } catch (Exception e)
            {
                netStream.Close();
                client.Close();
                this.Close();
            }
        }

        void runClient()
        {
            TcpClient client = null;
            try
            {
                client = new TcpClient("localhost", 12345);
                t = new Transfer(client);

                t.Start();

                MessageBox.Show("connected client");

                MSG hello = new MSG() { type = MessageType.HELLO };

                t.Send(hello);

                t.auto.WaitOne();
                if (t.m.type == MessageType.HELLO)
                {
                    Thread senderThread = new Thread(() => sendMSG(t));
                    senderThread.IsBackground = true;
                    senderThread.Start();

                    Thread receiverThread = new Thread(() => getMSG(t));
                    receiverThread.IsBackground = true;
                    receiverThread.Start();

                    t.auto.WaitOne();
                    if(t.m.type == MessageType.BYE)
                    {
                        t.Stop();
                        MessageBox.Show("Other side disconnected!");
                        this.Close();
                    }
                }
            } catch(Exception ex)
            {
                client.Close();
                this.Close();
            }
        }

        void sendMSG(Transfer t)
        {
            while(true)
            {
                try
                {
                    string input;
                    sendMSGEvent.WaitOne();
                    input = getInput();
                    putMSG(input);
                    MSG m = new MSG() { type = MessageType.MESSAGE, message = new Message() { msg = input } };
                    t.Send(m);
                }
                catch (Exception e)
                {
                    t.Stop();
                }
            }
        }

        void getMSG(Transfer t)
        {
            while(true)
            {
                try
                {
                    t.auto.WaitOne();
                    setMSG(t.m.message.msg);
                } catch (Exception ex)
                {
                    t.Stop();
                }
            }
        }

        void putMSG(string input)
        {
            try
            {
                MSGBox.Dispatcher.BeginInvoke(
                System.Windows.Threading.DispatcherPriority.Normal
                , new System.Windows.Threading.DispatcherOperationCallback(delegate
                {
                    MSGBox.Items.Add(input);
                    //eigentliche Änderungen
                    return null;
                }), null);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

        }


        string getInput()
        {
            string msg = "";
            try
            {
                InputBox.Dispatcher.Invoke(() =>
                {
                    msg = InputBox.Text;
                    InputBox.Text = "";
                });
            }
            catch (Exception ex)
            { 
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            return msg;

        }

        void setMSG(string msg)
        {
            try
            {
                MSGBox.Dispatcher.BeginInvoke(
                System.Windows.Threading.DispatcherPriority.Normal
                , new System.Windows.Threading.DispatcherOperationCallback(delegate
                {
                    MSGBox.Items.Add(msg);
                    //eigentliche Änderungen
                    return null;
                }), null);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }


        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            sendMSGEvent.Set();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ServerClientDialog dialog = new ServerClientDialog();
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                if (dialog.isServer)
                {
                    Thread t = new Thread(runServer);
                    t.IsBackground = true;
                    t.Start();
                }
                else
                {
                    Thread t = new Thread(runClient);
                    t.IsBackground = true;
                    t.Start();
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MSG bye = new MSG() { type = MessageType.BYE };
            t.Send(bye);
            t.Stop();
        }
    }
}
