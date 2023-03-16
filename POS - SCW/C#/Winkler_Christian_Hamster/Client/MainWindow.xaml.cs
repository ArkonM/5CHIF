using Comms;
using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;

/*
Hamster by Winkler Christian, 5CHIF, 2022/2023
*/

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public int Rows { get; set; } = 6;
        public int Columns { get; set; } = 8;
        public int PlayerX { get; set; } = 0;
        public int PlayerY { get; set; } = 0;
        public Direction PlayerFace { get; set; } = Direction.Right;
        public double Speed { get; set; } = 1.0;
        public string Host { get; set; } = "localhost";
        public int Port { get; set; } = 12345;

        private bool _Connected = false;
        public bool Connected
        {
            get => _Connected;
            set
            {
                if (value != _Connected)
                {
                    _Connected = value;
                    OnPropertyChanged("Connected");
                }
            }
        }

        public string Program { get; set; } = "";

        private string _Error = null;
        public string Error
        {
            get => _Error;
            set
            {
                if (value != _Error)
                {
                    _Error = value;
                    OnPropertyChanged("Error");
                }
            }
        }

        private Transfer transfer;
        private Receiver receiver;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            receiver = new ClientReceiver(this);

            ConnectToServer();
        }

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void ConnectToServer()
        {
            new Thread(new ThreadStart(delegate
            {
                try
                {
                    TcpClient client = new TcpClient(Host, Port);
                    transfer = new Transfer(client, receiver);
                    transfer.Start();
                    Connected = true;
                }
                catch (Exception)
                {
                    Connected = false;
                }
            }))
            {
                IsBackground = true
            }.Start();
        }

        private void BtnExecute_Click(object sender, RoutedEventArgs e)
        {
            Message m = new Message()
            {
                FieldSize = new Comms.Point()
                {
                    X = Columns,
                    Y = Rows
                },
                Player = new Player()
                {
                    Position = new Comms.Point()
                    {
                        X = PlayerX,
                        Y = PlayerY
                    },
                    Face = PlayerFace
                },
                Program = Program,
                Speed = Speed
            };
            transfer.Send(m);
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            if (Connected)
            {
                transfer.Stop();
            }
            else
            {
                ConnectToServer();
            }
        }

        private void btnCloseError_Click(object sender, RoutedEventArgs e)
        {
            Error = null;
        }
    }
}
