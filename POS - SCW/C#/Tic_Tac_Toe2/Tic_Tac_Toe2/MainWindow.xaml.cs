using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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

namespace Tic_Tac_Toe2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Field> Fields { get; set; } = new ObservableCollection<Field>();

        Transfer t;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void fillFields()
        {

            playingField.IsEnabled = ClientOrServer.Content == "Server" ? false : true;

            Fields.Clear();
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    Fields.Add(new Field(x, y));
                }
            }
            playingField.ItemsSource = Fields;
        }

        public void markEnemyTurn(int x, int y)
        {
            var f = Fields.FirstOrDefault(field => field.Y == y && field.X == x);
            f.Player = ClientOrServer.Content == "Server" ? "Client" : "Server";

            playingField.IsEnabled = true;
        }

        void startField()
        {
            MSG message = new MSG() { Type = MessageType.START };
            t.Send(message);
            
            fillFields();
        }

        public bool winChecker()
        {
            if (Fields[0].Player == Fields[1].Player && Fields[1].Player == Fields[2].Player ||
                Fields[3].Player == Fields[4].Player && Fields[4].Player == Fields[5].Player ||
                Fields[6].Player == Fields[7].Player && Fields[7].Player == Fields[8].Player ||

                Fields[0].Player == Fields[3].Player && Fields[3].Player == Fields[6].Player ||
                Fields[1].Player == Fields[4].Player && Fields[4].Player == Fields[7].Player ||
                Fields[2].Player == Fields[5].Player && Fields[5].Player == Fields[8].Player ||

                Fields[0].Player == Fields[4].Player && Fields[4].Player == Fields[8].Player ||
                Fields[2].Player == Fields[4].Player && Fields[4].Player == Fields[6].Player
                )
            {
                return true;
            }

            return false;
        }

        private void Start_Server(object sender, RoutedEventArgs e)
        {
            ClientOrServer.Content = "Server";

            TcpListener listener = new TcpListener(IPAddress.Any, 1113);
            listener.Start();

            TcpClient client = new TcpClient();
            Receiver receiver = new ReceiverMessage(this);
            client = listener.AcceptTcpClient();

            t = new Transfer(client, receiver);
            t.Start();

            startField();
            
        }

        private void Start_Client(object sender, RoutedEventArgs e)
        {
            ClientOrServer.Content = "Client";

            TcpClient client = new TcpClient("localhost", 1113);
            Receiver receiver = new ReceiverMessage(this);

            t = new Transfer(client, receiver);

            t.Start();

            startField();

        }

        private void playingField_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = playingField.SelectedIndex;

            if (index == -1 || Fields[index].Player == "Server" || Fields[index].Player == "Client") 
            {
                return;
            }

            playingField.IsEnabled = false;

            Fields[index].Player = ClientOrServer.Content.ToString();
            playingField.UnselectAll();

            MSG message;

            if (!winChecker())
            {
                message = new MSG() { Type = MessageType.CLICK, Info = new CLICKINFO() { x = Fields[index].X, y = Fields[index].Y } };
                t.Send(message);
            } else
            {
                message = new MSG() { Type = MessageType.CLICK, Info = new CLICKINFO() { x = Fields[index].X, y = Fields[index].Y } };
                t.Send(message);
                message = new MSG() { Type = MessageType.END };
                t.Send(message);
                MessageBox.Show("Du hast gewonnen!", ClientOrServer.Content.ToString());
                
            }
            
            
        }

        private void Restart(object sender, RoutedEventArgs e)
        {
            startField();
        }
    }
}
