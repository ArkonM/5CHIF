using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography.X509Certificates;
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
using System.IO;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace Schiffe_versenken
{
    //PORT: 12345
    public class Field : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }   
        // Eigenschaften
        private int _X;
        private int _Y;
        private SolidColorBrush _color;
        public int X
        {
            get { return _X; }
            set
            {
                _X = value;
                OnPropertyChanged(new PropertyChangedEventArgs("X"));
            }
        }
        public int Y
        {
            get { return _Y; }
            set
            {
                _Y = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Y"));
            }
        }

        public SolidColorBrush colors
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged(new PropertyChangedEventArgs("colors"));
            }
        }
        // Konstruktor
        public Field(int x, int y)
        {
            X = x;
            Y = y;
            colors = Brushes.Transparent;
        }
    }

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Field> Fields { get; set; } = new List<Field>();
        public int W { get; set; } = 10;
        public int H { get; set; } = 10;

        public int sub { get; set; } = 4;
        public int destroyer { get; set; } = 3;
        public int battleship { get; set; } = 2;
        public int carrier { get; set; } = 1;

        public int shipcount { get; set; } = 30;

        public AutoResetEvent readyEvent = new AutoResetEvent(false);

        public bool playing = false;

        public AutoResetEvent shoot = new AutoResetEvent(false);

        public Field currentField;

        public MainWindow()
        { 
            InitializeComponent();
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var ServerClientDialog = new ServerClient();
            ServerClientDialog.ShowDialog();
            if(ServerClientDialog.DialogResult == true && ServerClientDialog.isServer == true)
            {
                var dialog = new MatchfieldSize();
                dialog.ShowDialog();
                if (dialog.DialogResult == true)
                {
                    int x = 0, y = 0;
                    int.TryParse(dialog.SizeXBox.Text, out x);
                    int.TryParse(dialog.SizeYBox.Text, out y);
                    if (x == 0 || y == 0)
                    {
                        MessageBox.Show("Invalid Size!\nTry again!");
                        this.Close();
                    }
                    W = y;
                    H = x;
                }
                fillField();
                var countdialog = new ShipCount();
                countdialog.ShowDialog();
                if (countdialog.DialogResult == true)
                {
                    int s;
                    int d;
                    int b;
                    int c;

                    int.TryParse(countdialog.UBootBox.Text, out s);
                    int.TryParse(countdialog.DestroyerBox.Text, out d);
                    int.TryParse(countdialog.BattleshipBox.Text, out b);
                    int.TryParse(countdialog.CarrierBox.Text, out c);

                    sub = s;
                    destroyer = d;
                    battleship = b;
                    carrier = c;

                    shipcount = s * 2 + d * 3 + b * 3 + c;
                }
                MessageBox.Show("Sub Count: " + sub + "\n" + "Destroyer count: " + destroyer + "\n" + "Battleship count: " + battleship + "\n" + "Carrier count: " + carrier);
                System.Net.IPAddress[] a = Dns.GetHostByName(Dns.GetHostName()).AddressList;
                string ip = a[0].ToString();
                MessageBox.Show("The servers IP-Address is: " + ip);
                Thread t = new Thread(runServer);
                t.IsBackground = true;
                t.Start();
            } else
            {

                //MessageBox.Show("Sub Count: " + sub + "\n" + "Destroyer count: " + destroyer + "\n" + "Battleship count: " + battleship + "\n" + "Carrier count: " + carrier);
                var ipdialog = new IPDialog();
                ipdialog.ShowDialog();
                String ip = "localhost";
                if (ipdialog.DialogResult == true)
                {
                    ip = ipdialog.IPBox.Text;
                }
                Thread client = new Thread(runClient);
                client.IsBackground = true;
                client.Start(ip);
            }
        }

        void startSecond(bool whoStarts, Transfer t)
        {
            readyEvent.WaitOne();
            switch(whoStarts) 
            {
                case true: MessageBox.Show("Ready to play!\nServer has to do the first shot!"); break;
                case false: MessageBox.Show("Ready to play!\nClient has to do the first shot!"); break;
            }
            MSG ready = new MSG { Type = MessageType.READY };
            t.Send(ready);
            playing = true;
            while (true)
            {
                t.auto.WaitOne();
                if (t.m.Type == MessageType.SHOT)
                {
                    if (Fields[(W * t.m.Shot.Y) + t.m.Shot.X].colors != Brushes.Gray)
                    {
                        MSG miss = new MSG { Type = MessageType.MISS };
                        t.Send(miss);
                    }
                    else
                    {
                        hitmiss(false, (W * t.m.Shot.Y) + t.m.Shot.X);
                        MSG hit = new MSG { Type = MessageType.HIT };
                        t.Send(hit);
                    }
                }
                shoot.WaitOne();
                MSG shot = new MSG { Type = MessageType.SHOT, Shot = new Shot { X = currentField.X, Y = currentField.Y } };
                t.Send(shot);
                t.auto.WaitOne();
                if (t.m.Type == MessageType.HIT)
                {
                    hitmiss(true, (W * currentField.Y) + currentField.X);
                    MessageBox.Show("you hit");
                }
                else
                {
                    hitmiss(false, (W * currentField.Y) + currentField.X);
                    MessageBox.Show("You missed!");
                }
                shoot.Set();
            }
        }

        void startFirst(Transfer t)
        {
            readyEvent.WaitOne();
            MessageBox.Show("Waiting for other side to be ready!\nYou have to do the first shot.");
            t.auto.WaitOne();
            if (t.m.Type == MessageType.READY)
            {
                MessageBox.Show("Other side sent ready!\nYou can place your shot now.");
            }
            playing = true;
            while (true)
            {
                shoot.WaitOne();
                MSG shot = new MSG { Type = MessageType.SHOT, Shot = new Shot { X = currentField.X, Y = currentField.Y } };
                t.Send(shot);
                t.auto.WaitOne();
                if (t.m.Type == MessageType.HIT)
                {
                    hitmiss(true, (W * currentField.Y) + currentField.X);
                    MessageBox.Show("you hit");
                }
                else
                {
                    hitmiss(false, (W * currentField.Y) + currentField.X);
                    MessageBox.Show("You missed!");
                }
                shoot.Set();
                t.auto.WaitOne();
                if (t.m.Type == MessageType.SHOT)
                {
                    if (Fields[(W * t.m.Shot.Y) + t.m.Shot.X].colors != Brushes.Gray)
                    {
                        MSG miss = new MSG { Type = MessageType.MISS };
                        t.Send(miss);
                    }
                    else
                    {
                        hitmiss(false, (W * t.m.Shot.Y) + t.m.Shot.X);
                        MSG hit = new MSG { Type = MessageType.HIT };
                        t.Send(hit);
                    }
                }
            }
        }

        void runClient(object obj)
        {
            string ip = (string)obj;
            try
            {
                TcpClient client = new TcpClient(ip, 12345);
                ReceiverLogin r = new ReceiverLogin();
                Transfer t = new Transfer(client);

                

                t.Start();
                t.auto.WaitOne();
                W = t.m.Config.Width;
                H = t.m.Config.Height;
                fillField();
                sub = t.m.Config.Ships[0];
                destroyer = t.m.Config.Ships[1];
                battleship = t.m.Config.Ships[2];
                carrier = t.m.Config.Ships[3];
                shipcount = sub * 2 + destroyer * 3 + battleship * 3 + carrier;
                MessageBox.Show("Sub Count: " + sub + "\n" + "Destroyer count: " + destroyer + "\n" + "Battleship count: " + battleship + "\n" + "Carrier count: " + carrier);
                if(t.m.Config.StartPlayer == true)
                {
                    startSecond(true, t);
                } else
                {
                    startFirst(t);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                this.Close();
            }

        }

        void runServer()
        {
            TcpListener listener = null;
            try
            {
                // Erzeuge eine TcpListener Instanz um eingehende
                // Verbindungen anzunehmen
                listener = new TcpListener(System.Net.IPAddress.Any, 12345);
                listener.Start();
            }
            catch (SocketException se)
            {
                Console.WriteLine(se.ErrorCode + ": " + se.Message);
                Environment.Exit(se.ErrorCode);
            }
            TcpClient client = null;
            NetworkStream netStream = null;

            client = listener.AcceptTcpClient();

            try
            {
                var random = new Random();
                // Generiere eine Client Verbindung
                netStream = client.GetStream();
                //ReceiverLogin r = new ReceiverLogin();
                Transfer t = new Transfer(client);
                t.Start();

                List<int> s = new List<int>() { sub, destroyer, battleship, carrier };

                MSG config = new MSG { Type = MessageType.CONFIG, Config = new Config { Width = this.W, Height = this.H, Ships = s, StartPlayer = random.Next(2) == 1 } };
                
                t.Send(config);

                if (config.Config.StartPlayer == true)
                {
                    startFirst(t);
                } else
                {
                    startSecond(false, t);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                netStream.Close();
                this.Close();
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
                    for (int i = 0; i < H; i++)
                    {
                        for (int j = 0; j < W; j++)
                        {
                            Fields.Add(new Field(j, i));
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

        void hitmiss(bool h, int shot)
        {
            try
            {
                FieldsBox.Dispatcher.BeginInvoke(
                System.Windows.Threading.DispatcherPriority.Normal
                , new System.Windows.Threading.DispatcherOperationCallback(delegate
                {
                    if(h)
                    {
                        Fields[shot].colors = Brushes.Green;
                    } else
                    {
                        Fields[shot].colors = Brushes.Red;
                    }
                    //eigentliche Änderungen
                    return null;
                }), null);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!playing)
            {
                if (Fields[FieldsBox.SelectedIndex].colors != Brushes.Transparent)
                {
                    Fields[FieldsBox.SelectedIndex].colors = Brushes.Transparent;
                    return;
                }
                int i1 = FieldsBox.SelectedIndex - (W - 1);
                int i2 = FieldsBox.SelectedIndex - (W + 1);
                int i3 = FieldsBox.SelectedIndex + (W - 1);
                int i4 = FieldsBox.SelectedIndex + (W + 1);
                Boolean valid = true;
                if (i1 > 0 && Fields[FieldsBox.SelectedIndex].X != 0)
                {
                    if (Fields[i1].colors != Brushes.Transparent && Fields[i1].Y != Fields[FieldsBox.SelectedIndex].Y)
                    {
                        valid = false;
                    }
                }
                if (i2 > 0 && Fields[FieldsBox.SelectedIndex].X != 0)
                {
                    if (Fields[i2].colors != Brushes.Transparent && Fields[i2].Y != Fields[FieldsBox.SelectedIndex].Y)
                    {
                        valid = false;
                    }
                }
                if (i3 < Fields.Count && Fields[FieldsBox.SelectedIndex].X != 0)
                {
                    if (Fields[i3].colors != Brushes.Transparent && Fields[i3].Y != Fields[FieldsBox.SelectedIndex].Y)
                    {
                        valid = false;
                    }
                }
                if (i4 < Fields.Count && Fields[FieldsBox.SelectedIndex].X != W - 1)
                {
                    if (Fields[i4].colors != Brushes.Transparent && Fields[i4].Y != Fields[FieldsBox.SelectedIndex].Y)
                    {
                        valid = false;
                    }
                }

                if (valid)
                {
                    Fields[FieldsBox.SelectedIndex].colors = Brushes.Gray;
                    int check = checkShips(Brushes.Gray);
                    if (check == -1)
                    {
                        MessageBox.Show("Some ships are not positioned correctly");
                        Fields[FieldsBox.SelectedIndex].colors = Brushes.Transparent;
                        return;
                    }
                    else if (check == 1)
                    {
                        MessageBox.Show("You already positioned all of your ships!");
                        readyEvent.Set();
                        Fields[FieldsBox.SelectedIndex].colors = Brushes.Transparent;
                        return;
                    }
                    else if (check == -2)
                    {
                        MessageBox.Show("You positioned the wrong ships!");
                        Fields[FieldsBox.SelectedIndex].colors = Brushes.Transparent;
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("You can't put a part of the ship in this spot!");
                    return;
                }
            } else
            {
                currentField = Fields[FieldsBox.SelectedIndex];
                shoot.Set();
                shoot.WaitOne();
                int check = checkShips(Brushes.Green);
                if(check == 1)
                {
                    currentField.colors = Brushes.Transparent;
                    MessageBox.Show("You won!");
                }
                check = checkShips(Brushes.Gray);
                if(check == -3)
                {
                    currentField.colors = Brushes.Transparent;
                    MessageBox.Show("You lost!");
                }
            }
        }

        private int checkShips(SolidColorBrush checkcolor) {
            int counter = 0;
            int shipsize = 0;
            int s = 0;
            int d = 0;
            int b = 0;
            int c = 0;

            for (int x = 0; x < W; x++)
            {
                for (int y = x; y < Fields.Count; y += W)
                {
                    if (Fields[y].colors == checkcolor)
                    {
                        counter++;
                        shipsize++;
                    }

                    if (Fields[y].colors == Brushes.Transparent)
                    {
                        switch (shipsize)
                        {
                            case 2: s++; break;
                            case 3: d++; break;
                            case 4: b++; break;
                            case 5: c++; break;
                        }
                        shipsize = 0;
                    }
                    if(shipsize > 5)
                    {
                        return -1;
                    }
                }
            }

            shipsize = 0;

            counter = 0;

            for (int y = 0; y < Fields.Count; y++)
            {
                if (Fields[y].colors == checkcolor)
                {
                    counter++;
                    shipsize++;
                }

                if (Fields[y].colors == Brushes.Transparent)
                {
                    switch (shipsize)
                    {
                        case 2: s++; break;
                        case 3: d++; break;
                        case 4: b++; break;
                        case 5: c++; break;
                    }
                    shipsize = 0;
                }

                if (shipsize > 5)
                {
                    return -1;
                }
            }

            if ((s != sub || d != destroyer || b != battleship || c != carrier) && counter == shipcount)
            {
                return -2;
            }
            
            if (counter > shipcount)
            {
                return 1;
            }

            if(counter == shipcount && checkcolor != Brushes.Gray)
            {
                return 1;
            }

            if(playing && counter == 0 && checkcolor == Brushes.Gray)
            {
                return -3;
            }

            return 0;
        }
    }
}
