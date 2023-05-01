using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Security.Cryptography.Xml;
using System.Windows.Interop;

namespace TicTacToe
{
    
    public class Field : INotifyPropertyChanged
    {

        SolidColorBrush _color;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        public SolidColorBrush Color
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Color"));
            }
        }

        public Field()
        {
            Color = Brushes.Transparent;
        }
    }





    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Transfer t;
        AutoResetEvent clicked = new AutoResetEvent(false);
        private int _selectedIndex;

        public MainWindow()
        {
            InitializeComponent();
        }

        public List<Field> Fields { get; set; } = new List<Field>();
        public int Size { get; set; } = 3;



        void runServer()
        {
            TcpListener listener = null;
            try
            {
                listener = new TcpListener(IPAddress.Any, 12345);
                listener.Start();
            }
            catch (SocketException e)
            {
                MessageBox.Show(e.ErrorCode + ": " + e.Message);
                Environment.Exit(e.ErrorCode);
            }

            TcpClient client = listener.AcceptTcpClient();

            try
            {
                var random = new Random();
                //ReceiverLogin r = new ReceiverLogin();
                Transfer t = new Transfer(client);
                t.Start();


                Play config = new Play { type = MessageType.CONFIG, Config = new Config { fieldSize = Size, startPlayer = random.Next(2) == 1 } };

                t.Send(config);

                fillField();
                MessageBox.Show("connected client");

                if (config.Config.startPlayer == true)
                {
                    startFirst(t);
                }
                else
                {
                    startSecond(false, t);
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
                t.auto.WaitOne();

                Size = t.m.Config.fieldSize;

                fillField();
                MessageBox.Show("connected server");

                if (t.m.Config.startPlayer == true)
                {
                    startSecond(true, t);
                }
                else
                {
                    startFirst(t);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



        void startFirst(Transfer t)
        {
            while (true)
            {
                clicked.WaitOne();

                this.Dispatcher.BeginInvoke(
                System.Windows.Threading.DispatcherPriority.Normal
                , new System.Windows.Threading.DispatcherOperationCallback(delegate
                {
                    PlayingField.IsEnabled = false;
                    return null;
                }), null);

                try
                {
                    Play m = new Play() { type = MessageType.PLAYFIELD, index = new PlayfieldIndex() { indx = _selectedIndex } };
                    t.Send(m);
                }
                catch (Exception ex)
                {
                    t.Stop();
                    MessageBox.Show(ex.ToString());
                }


                if (winChecker())
                {
                    t.Stop();
                    return;
                }


                t.auto.WaitOne();

                Fields[t.m.index.indx].Color = Brushes.Pink;


                if (winChecker())
                {
                    t.Stop();
                    return;
                }


                this.Dispatcher.BeginInvoke(
                System.Windows.Threading.DispatcherPriority.Normal
                , new System.Windows.Threading.DispatcherOperationCallback(delegate
                {
                    PlayingField.IsEnabled = true;
                    return null;
                }), null);
            }
            
        }


        void startSecond(bool whoStarts, Transfer t)
        {
            switch (whoStarts)
            {
                case true: MessageBox.Show("Ready to play!\nServer has to do the first shot!"); break;
                case false: MessageBox.Show("Ready to play!\nClient has to do the first shot!"); break;
            }

            this.Dispatcher.BeginInvoke(
                System.Windows.Threading.DispatcherPriority.Normal
                , new System.Windows.Threading.DispatcherOperationCallback(delegate
                {
                    PlayingField.IsEnabled = false;
                    return null;
                }), null);

            while (true)
            {
                t.auto.WaitOne();

                Fields[t.m.index.indx].Color = Brushes.Pink;


                if (winChecker())
                {
                    t.Stop();
                    return;
                }


                this.Dispatcher.BeginInvoke(
                System.Windows.Threading.DispatcherPriority.Normal
                , new System.Windows.Threading.DispatcherOperationCallback(delegate
                {
                    PlayingField.IsEnabled = true;
                    return null;
                }), null);


                clicked.WaitOne();

                this.Dispatcher.BeginInvoke(
                System.Windows.Threading.DispatcherPriority.Normal
                , new System.Windows.Threading.DispatcherOperationCallback(delegate
                {
                    PlayingField.IsEnabled = false;
                    return null;
                }), null);

                try
                {
                    Play m = new Play() { type = MessageType.PLAYFIELD, index = new PlayfieldIndex() { indx = _selectedIndex } };
                    t.Send(m);
                }
                catch (Exception ex)
                {
                    t.Stop();
                    MessageBox.Show(ex.ToString());
                }

                if (winChecker())
                {
                    t.Stop();
                    return;
                }
            }
        }


        bool winChecker()
        {
            int greenCounter = 0;
            int pinkCounter = 0;
            bool greenWin = false;
            bool pinkWin = false;
            bool noWin = true;
            for (int i = 0; i < Size; i++)
            {
                greenCounter = 0;
                pinkCounter = 0;

                for (int y = 0; y < Size && !greenWin && !pinkWin; y++)
                {
                    if(Fields[(Size * i) + y].Color == Brushes.LightGreen)
                    {
                        greenCounter++;
                        pinkCounter = 0;
                    } else if (Fields[(Size * i) + y].Color == Brushes.Pink)
                    {
                        pinkCounter++;
                        greenCounter = 0;
                    } else
                    {
                        pinkCounter = 0;
                        greenCounter = 0;
                        noWin = false;
                    }
                    if (greenCounter == 3)
                    {
                        greenWin = true;
                    }
                    else if (pinkCounter == 3)
                    {
                        pinkWin = true;
                    }
                }
            }

            for (int i = 0; i < Size && !greenWin && !pinkWin; i++)
            {
                greenCounter = 0;
                pinkCounter = 0;
                for (int y = i; y < Size*Size; y += Size)
                {
                    if (Fields[y].Color == Brushes.LightGreen)
                    {
                        greenCounter++;
                        pinkCounter = 0;
                    }
                    else if (Fields[y].Color == Brushes.Pink)
                    {
                        pinkCounter++;
                        greenCounter = 0;
                    }
                    else
                    {
                        pinkCounter = 0;
                        greenCounter = 0;
                        noWin = false;
                    }
                    if (greenCounter == 3)
                    {
                        greenWin = true;
                    }
                    else if (pinkCounter == 3)
                    {
                        pinkWin = true;
                    }
                }
            }

            if(!greenWin  && !pinkWin)
            {
                greenCounter = 0;
                pinkCounter = 0;
            }
            
            for (int i = 0; i < Size*Size && !greenWin && !pinkWin; i += Size+1)
            {

                if (Fields[i].Color == Brushes.LightGreen)
                {
                    greenCounter++;
                    pinkCounter = 0;
                }
                else if (Fields[i].Color == Brushes.Pink)
                {
                    pinkCounter++;
                    greenCounter = 0;
                }
                else
                {
                    pinkCounter = 0;
                    greenCounter = 0;
                    noWin = false;
                }
                if (greenCounter == 3)
                {
                    greenWin = true;
                }
                else if (pinkCounter == 3)
                {
                    pinkWin = true;
                }   
            }

            greenCounter = 0;
            pinkCounter = 0;
            for (int i = Size-1; i <= Size * Size - Size && !greenWin && !pinkWin; i += Size - 1)
            {

                if (Fields[i].Color == Brushes.LightGreen)
                {
                    greenCounter++;
                    pinkCounter = 0;
                }
                else if (Fields[i].Color == Brushes.Pink)
                {
                    pinkCounter++;
                    greenCounter = 0;
                }
                else
                {
                    pinkCounter = 0;
                    greenCounter = 0;
                    noWin = false;
                }
                if (greenCounter == 3)
                {
                    greenWin = true;
                }
                else if (pinkCounter == 3)
                {
                    pinkWin = true;
                }
            }
            if (pinkWin)
            {
                MessageBox.Show("Dein Gegner hat gewonnen!", "Verloren");
                return true;
            } else if (greenWin)
            {
                MessageBox.Show("Du hast das Spiel gewonnen!", "Gewonnen");
                return true;
            } else if (noWin)
            {
                MessageBox.Show("Keiner hat das Spiel gewonnen!", "Loser");
                return true;
            } else
            {
                return false;
            }
        }

        void fillField()
        {
            try
            {
                this.Dispatcher.BeginInvoke(
                System.Windows.Threading.DispatcherPriority.Normal
                , new System.Windows.Threading.DispatcherOperationCallback(delegate
                {
                    //eigentliche Änderungen
                    for (int i = 0; i < Size; i++)
                    {
                        for (int j = 0; j < Size; j++)
                        {
                            Fields.Add(new Field());
                        }
                    }
                    this.DataContext = this;
                    return null;
                }), null);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ServerClientDialog dialog1 = new ServerClientDialog();
            dialog1.ShowDialog();
            if (dialog1.DialogResult == true)
            {
                if (dialog1.isServer)
                {
                    Title.Content = "TicTacToe - Server";

                    MatchfieldSize dialog2 = new MatchfieldSize();
                    dialog2.ShowDialog();
                    if (dialog2.DialogResult == true)
                    {
                        int.TryParse(dialog2.SizeBox.Text, out int result);
                        Size = result;
                    }
                    dialog2.Close();

                    /* IP des Servers zeigen
                    System.Net.IPAddress[] a = Dns.GetHostByName(Dns.GetHostName()).AddressList;
                    string ip = a[0].ToString();
                    MessageBox.Show("The servers IP-Address is: " + ip);
                    */

                    Thread t = new Thread(runServer);
                    t.IsBackground = true;
                    t.Start();

                }
                else
                {
                    Title.Content = "TicTacToe - Client";

                    /* Eingabefeld für die IP Adresse
                    IPDialog dialog2 = new IPDialog();
                    dialog2.ShowDialog();
                    if (dialog2.DialogResult == true)
                    {
                        //int.TryParse(dialog2.IPBox.Text, out int result);
                        //Size = result;
                    }
                    dialog2.Close();
                    */

                    Thread t = new Thread(runClient);
                    t.IsBackground = true;
                    t.Start();

                }
            }
            dialog1.Close();

            
        }

        private void PlayingField_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PlayingField.SelectedIndex != -1)
            {
                if (Fields[PlayingField.SelectedIndex].Color != Brushes.LightGreen && Fields[PlayingField.SelectedIndex].Color != Brushes.Pink)
                {
                    // Set the background color of the container element
                    Fields[PlayingField.SelectedIndex].Color = Brushes.LightGreen;
                    _selectedIndex = PlayingField.SelectedIndex;
                    PlayingField.UnselectAll();
                    clicked.Set();
                }
            }
        }
    }
}
