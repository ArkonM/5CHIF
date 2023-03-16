using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace BSP_Projekt_PA3
{
    public class Field : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _hasHamster;
        private int _hamsterX;
        private int _hamsterY;

        public bool hasHamster
        {
            get { return _hasHamster; }
            set
            {
                _hasHamster = value;
                OnPropertyChanged();
            }
        }

        public int HamsterX
        {
            get { return _hamsterX; }
            set
            {
                _hamsterX = value;
                OnPropertyChanged();
            }
        }

        public int HamsterY
        {
            get { return _hamsterY; }
            set
            {
                _hamsterY = value;
                OnPropertyChanged();
            }
        }

        public int X { set; get; }
        public int Y { set; get; }

        public SolidColorBrush BackgroundColor
        {
            get
            {
                if (hasHamster)
                {
                    return Brushes.Green;
                }
                else
                {
                    return Brushes.White;
                }

            }
        }

        private SolidColorBrush _backgroundColor;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {

            _backgroundColor = null;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BackgroundColor)));
        }
    }


    /// <summary>
    /// Interaction logic for Server.xaml
    /// </summary>
    public partial class Server : Window
    {
        Field f;
        Transfer t;
        public List<Field> Fields { get; set; } = new List<Field>();
        public Server()
        {
            InitializeComponent();
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    Fields.Add(new Field() { X = i, Y = j, hasHamster = false });
                }

            }
            Fields[200].hasHamster = true;
            f = Fields[200];
            this.DataContext = this;
        }
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
                Thread receiverThread = new Thread(() => getMSG(t));
                receiverThread.IsBackground = true;
                receiverThread.Start();
            }
            catch (Exception e)
            {
                netStream.Close();
                client.Close();
                this.Close();
            }
        }

        void getMSG(Transfer t)
        {
            while (true)
            {
                try
                {
                    t.auto.WaitOne();
                    setMSG(t.m);
                    t.m.Messages.Clear();
                }
                catch (Exception ex)
                {
                    t.Stop();
                }
            }
        }

        void setMSG(MSG m)
        {
            string msgText = "";
            for (int i = 0; i < m.Messages.Count; i++)
            {
                ExecuteCommand(m.Messages[i]);
                //msgText += m.Messages[i] + "\n";
            }
            //MessageBox.Show(msgText);
        }


        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            Thread T = new Thread(runServer);
            T.IsBackground = true;
            T.Start();
            
        }

        private void ExecuteCommand(string currentCommand)
        {
            if (currentCommand.Equals("vor()") || currentCommand.Equals("zurueck()") || currentCommand.Equals("links()") || currentCommand.Equals("rechts()"))
            {
                if (currentCommand.Equals("vor()"))
                {
                    for (int i = 0; i < Fields.Count; i++)
                    {
                        if (Fields[i].hasHamster)
                        {
                            Fields[i].hasHamster = false;

                            Fields[i + 1].hasHamster = true;
                            Hamsterkäfig.Dispatcher.Invoke(new Action(() => { Hamsterkäfig.Items.Refresh(); }));

                            currentCommand = "";

                            i = Fields.Count;       //schöners break

                        }
                    }
                }
                else if (currentCommand.Equals("links()"))
                {
                    for (int i = 0; i < Fields.Count; i++)
                    {
                        if (Fields[i].hasHamster)
                        {
                            Fields[i].hasHamster = false;

                            Fields[i - 20].hasHamster = true;
                            Hamsterkäfig.Dispatcher.Invoke(new Action(() => { Hamsterkäfig.Items.Refresh(); }));
                            currentCommand = "";
                            i = Fields.Count;
                        }
                    }

                }
                else if (currentCommand.Equals("rechts()"))
                {
                    for (int i = 0; i < Fields.Count; i++)
                    {
                        if (Fields[i].hasHamster)
                        {
                            Fields[i].hasHamster = false;
                            Fields[i + 20].hasHamster = true;
                            Hamsterkäfig.Dispatcher.Invoke(new Action(() => { Hamsterkäfig.Items.Refresh(); }));
                            currentCommand = "";
                            i = Fields.Count;
                        }
                    }
                }
                else if (currentCommand.Equals("zurueck()"))
                {
                    for (int i = 0; i < Fields.Count; i++)
                    {
                        if (Fields[i].hasHamster)
                        {
                            Fields[i].hasHamster = false;
                            Fields[i - 1].hasHamster = true;
                            Hamsterkäfig.Dispatcher.Invoke(new Action(() => { Hamsterkäfig.Items.Refresh(); }));
                            currentCommand = "";
                            i = Fields.Count;
                        }
                    }

                }
            }

            /*
            switch (command)
            {
                case "vor()":
                    if (f.Y > 0)
                    {
                        var newField = Fields.Single(field => field.X == f.X && field.Y == f.Y - 1);
                        f.hasHamster = false;
                        newField.hasHamster = true;
                        f = newField;
                    }
                    break;
                case "links()":
                    if (f.X > 0)
                    {
                        var newField = Fields.Single(field => field.X == f.X - 1 && field.Y == f.Y);
                        f.hasHamster = false;
                        newField.hasHamster = true;
                        f = newField;
                    }
                    break;
                case "rechts()": 
                    if (f.X < 19)
                    {
                        var newField = Fields.Single(field => field.X == f.X + 1 && field.Y == f.Y);
                        f.hasHamster = false;
                        newField.hasHamster = true;
                        f = newField;
                    }
                    break;
                case "zurück()":
                    if (f.Y < 19)
                    {
                        var newField = Fields.Single(field => field.X == f.X && field.Y == f.Y + 1);
                        f.hasHamster = false;
                        newField.hasHamster = true;
                        f = newField;
                    }
                    break;
                default:
                    break;
            }


            // Aktualisiert die Eigenschaften HamsterX und HamsterY auf der Grundlage der neuen Feldposition
            f.HamsterX = f.X;
            f.HamsterY = f.Y;*/
        }

    }
}
