using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PA3_Client
{

    public class Cell : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _X;
        public int X
        {
            get
            {
                return _X;
            }
            set
            {
                _X = value;
                OnPropertyChanged("X");
            }
        }
        private int _Y;
        public int Y
        {
            get
            {
                return _Y;
            }
            set
            {
                _Y = value;
                OnPropertyChanged("Y");
            }
        }

        private int _value = -1;
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }
        private bool _isBomb = false;
        public bool IsBomb
        {
            get
            {
                return _isBomb;
            }
            set
            {
                _isBomb = value;
                OnPropertyChanged("IsBomb");
            }
        }
    }



    public class playingFieldConfig : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        int _height;
        int _width;
        int _mines;

        public int Height
        {
            get { return _height; } 
            set { 
                _height = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Height"));
            }
        }


        public int Width
        {
            get { return _width; }
            set
            {
                _width = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Width"));
            }
        }


        public int Mines
        {
            get { return _mines; }
            set
            {
                _mines = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Mines"));
            }
        }


        public playingFieldConfig() 
        {
            _height = 10;
            _width = 10;
            _mines = 10;
        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public playingFieldConfig Config { get; set; } = new playingFieldConfig();

        public event PropertyChangedEventHandler PropertyChanged;

        Transfer t;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Cell> cells { get; set;} = new ObservableCollection<Cell>();

        


        public MainWindow()
        {
            InitializeComponent();
        }


        private void PlayingField_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = PlayingField.SelectedIndex;
            if (index != -1 && cells[index].Value == -1)
            {
                MSG click = new MSG { Type = MessageType.PICK, Pick = new Pick { X = cells[index].X, Y = cells[index].Y} };

                t.Send(click);
            }
        }


        void fillPlayingField()
        {
            for(int i  = 0; i < Config.Height; i++)
            {
                for(int j = 0; j < Config.Width; j++)
                {
                    cells.Add(new Cell { X = j, Y = i});
                }
            }

            this.DataContext = this;
        }


        public void revealFields(MSG m)
        {
            foreach(var cell in m.Nearby)
            {
                for (int i = 0; i < Config.Height * Config.Width; i++)
                {
                    if (cells[i].X == cell.X && cells[i].Y == cell.Y) cells[i].Value = cell.Count;
                }
            }
        }


        void Button_Click(object sender, RoutedEventArgs e)
        {
            TcpClient client = null;
            Receiver receiver = new ReceiverMessage(this);
            try
            {
                client = new TcpClient("localhost", 12345);
                t = new Transfer(client, receiver);

                t.Start();

                MSG config = new MSG { Type = MessageType.NEW, Config = new Config { Height = Config.Height, Width = Config.Width, Mines = Config.Mines } };

                t.Send(config);



                fillPlayingField();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
