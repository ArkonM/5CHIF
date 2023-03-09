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

        public class PlayingField
        {
            public int Rows { get; set; }
            public int Columns {  get; set; }

            public bool NotConfirmed { get; set; }

            public bool PlayerSelection { get; set; }
        }

        public ObservableCollection<Field> Fields { get; set; } = new ObservableCollection<Field>();

        public PlayingField Config = new PlayingField();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Config.PlayerSelection = false;
            Config.Rows = 10;
            Config.Columns = 10;
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
            for (int x = 0; x < Config.Rows; x++)
            {
                for (int y = 0; y < Config.Columns; y++)
                {

                    Fields.Add(new Field() { X = x, Y = y });
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Config.PlayerSelection = false;
        }
    }
}
