using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Schiffeversenken_Multiplayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public class Field
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        public class PlayingField : INotifyPropertyChanged
        {

            public event PropertyChangedEventHandler? PropertyChanged;
        }

        public ObservableCollection<Field> Fields { get; set; } = new ObservableCollection<Field>();

        public int Rows { get; set; } = 10;
        public int Columns { get; set; } = 10;
        public bool NotConfirmed { get; set; } = true;

        public bool PlayerSelection { get; set; } = true;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Clicked");
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox? listBox = sender as ListBox;
            if (listBox != null && listBox.SelectedItem != null)
            {
                // Get the container element for the selected item
                ListBoxItem? selectedItemContainer = listBox.ItemContainerGenerator.ContainerFromItem(listBox.SelectedItem) as ListBoxItem;
                if (selectedItemContainer != null)
                {
                    // Set the background color of the container element
                    selectedItemContainer.Background = Brushes.Green;
                    listBox.UnselectAll();
                }
            }
        }

        private void Confirm_Button(object sender, RoutedEventArgs e)
        {
            Fields.Clear();
            for (int x = 0; x < Rows; x++)
            {
                for (int y = 0; y < Columns; y++)
                {

                    Fields.Add(new Field() { X = x, Y = y });
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PlayerSelection = false;
        }
    }
}
