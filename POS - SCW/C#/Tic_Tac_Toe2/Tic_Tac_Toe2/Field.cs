using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe2
{
    public class Field : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        private int _X;
        private int _Y;
        private string _Player;

        public int X
        {
            get { return _X; }
            set
            {
                _X = value;
                OnPropertyChanged(nameof(X));
            }
        }

        public int Y
        {
            get { return _Y; }
            set
            {
                _Y = value;
                OnPropertyChanged(nameof(Y));
            }
        }

        public string Player
        {
            get { return _Player; }
            set
            {
                _Player = value;
                OnPropertyChanged(nameof(Player));
            }
        }

        public Field(int x, int y)
        {
            X = x;
            Y = y;
            Player = x + y.ToString();
        }
    }
}
