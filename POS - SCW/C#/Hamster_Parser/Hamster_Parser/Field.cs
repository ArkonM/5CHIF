using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Hamster_Parser
{
    public class Field : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public int _X;
        public int _Y;
        public int _CC;
        public SolidColorBrush _Color;

        public int X
        {
            get { return _X; }
            set
            {
                _X = value;
                OnPropertyChanged("X");
            }
        }

        public int Y
        {
            get { return _Y; }
            set
            {
                _Y = value;
                OnPropertyChanged("Y");
            }
        }

        public int CornCount
        {
            get { return _CC; }
            set
            {
                _CC = value;
                OnPropertyChanged("CornCount");
            }
        }

        public SolidColorBrush Color
        {
            get { return _Color; }
            set
            {
                _Color = value;
                OnPropertyChanged("Color");
            }
        }

        public Field(int x, int y)
        {
            X = x;
            Y = y;
            Random r = new Random();
            CornCount = r.Next(100);
            Color = Brushes.Transparent;
        }
    }
}
