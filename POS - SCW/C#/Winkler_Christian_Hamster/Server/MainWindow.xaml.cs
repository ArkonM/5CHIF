using Comms;
using Server.Grammar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;

/*
Hamster by Winkler Christian, 5CHIF, 2022/2023
*/

namespace Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private int _Rows = 6;
        public int Rows
        {
            get => _Rows;
            set
            {
                if (value != _Rows)
                {
                    _Rows = value;
                    OnPropertyChanged("Rows");
                }
            }
        }

        private int _Columns = 8;
        public int Columns
        {
            get => _Columns;
            set
            {
                if (value != _Columns)
                {
                    _Columns = value;
                    OnPropertyChanged("Columns");
                }
            }
        }

        public Player Player { get; set; } = null;
        public double Speed { get; set; } = 1.0;

        private List<Cell> _Cells = new List<Cell>();
        public List<Cell> Cells
        {
            get => _Cells;
            set
            {
                if (value != _Cells)
                {
                    _Cells = value;
                    OnPropertyChanged("Cells");
                }
            }
        }

        private TcpListener listener;
        private Transfer transfer;
        private Receiver receiver;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Player = new Player()
            {
                Position = new Comms.Point()
                {
                    X = 0,
                    Y = Rows - 1
                },
                Face = Direction.Right
            };
            UpdateCellsOnDemand();
            InitTcpServer();
        }

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void InitTcpServer()
        {
            new Thread(new ThreadStart(delegate
            {
                try
                {
                    listener = new TcpListener(IPAddress.Any, 12345);
                    listener.Start();
                    receiver = new ServerReceiver(this);

                    AcceptTcpClient();
                }
                catch (Exception)
                {
                    Console.WriteLine("Unable to bind to port 12345.");
                }
            }))
            {
                IsBackground = true
            }.Start();
        }

        public void AcceptTcpClient()
        {
            TcpClient client = listener.AcceptTcpClient();
            transfer = new Transfer(client, receiver);
            transfer.Start();
            Console.WriteLine("Accepted Client " + transfer.GetIP());
        }

        public void UpdateCellsOnDemand()
        {
            if (Cells.Count != Rows * Columns)
            {
                Cells = new List<Cell>();
                for (int i = 0; i < Rows * Columns; i++)
                {
                    Cells.Add(new Cell());
                }
            }
            RefreshField();
        }

        public void ExecuteProgram(string program)
        {
            List<Token> tokens;
            try
            {
                tokens = Token.Tokenize(program);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to execute program: " + e.Message);
                transfer.Send(new Message() { Error = e.Message });
                return;
            }
            try
            {
                if (Player.Position.X < 0 || Player.Position.X >= Columns || Player.Position.Y < 0 || Player.Position.Y >= Rows)
                {
                    throw new Exception("Unable to execute program. Player is positioned out of bounds. (X = " + Player.Position.X + ", Y = " + Player.Position.Y + " )");
                }
                RefreshField();
                int sleepTime = Speed > 0 ? (int)(1000 / Speed) : 1000;
                Thread.Sleep(250);
                for (int i = 0; i < tokens.Count; i++)
                {
                    Token token = tokens[i];
                    Thread.Sleep(sleepTime);
                    switch (token.Statement)
                    {
                        case Statement.Vor:
                            string errorMsg = "Program crashed at statement " + i + ". Unable to leave the grid.";
                            switch (Player.Face)
                            {
                                case Direction.Left:
                                    if (Player.Position.X == 0)
                                    {
                                        throw new Exception(errorMsg);
                                    }
                                    Player.Position.X--;
                                    break;
                                case Direction.Up:
                                    if (Player.Position.Y == 0)
                                    {
                                        throw new Exception(errorMsg);
                                    }
                                    Player.Position.Y--;
                                    break;
                                case Direction.Right:
                                    if (Player.Position.X == Columns - 1)
                                    {
                                        throw new Exception(errorMsg);
                                    }
                                    Player.Position.X++;
                                    break;
                                case Direction.Down:
                                    if (Player.Position.Y == Rows - 1)
                                    {
                                        throw new Exception(errorMsg);
                                    }
                                    Player.Position.Y++;
                                    break;
                            }
                            break;
                        case Statement.LinksUm:
                            switch (Player.Face)
                            {
                                case Direction.Left:
                                    Player.Face = Direction.Down;
                                    break;
                                case Direction.Up:
                                    Player.Face = Direction.Left;
                                    break;
                                case Direction.Right:
                                    Player.Face = Direction.Up;
                                    break;
                                case Direction.Down:
                                    Player.Face = Direction.Right;
                                    break;
                            }
                            break;
                        case Statement.RechtsUm:
                            switch (Player.Face)
                            {
                                case Direction.Left:
                                    Player.Face = Direction.Up;
                                    break;
                                case Direction.Up:
                                    Player.Face = Direction.Right;
                                    break;
                                case Direction.Right:
                                    Player.Face = Direction.Down;
                                    break;
                                case Direction.Down:
                                    Player.Face = Direction.Left;
                                    break;
                            }
                            break;
                    }
                    RefreshField();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                transfer.Send(new Message() { Error = e.Message });
            }
        }

        private void RefreshField()
        {
            foreach (Cell c in Cells)
            {
                c.Colored = false;
            }
            int idx = (Player.Position.Y * Columns) + Player.Position.X;
            if (idx >= 0 && idx < Cells.Count)
            {
                Cell cell = Cells[(Player.Position.Y * Columns) + Player.Position.X];
                cell.Face = Player.Face;
                cell.Colored = true;
                lvCells.Dispatcher.Invoke(delegate
                {
                    lvCells.Items.Refresh();
                });
            }
        }
    }
}
