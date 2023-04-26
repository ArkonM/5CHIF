using DataModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
namespace UnglaublicherRestaurantFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public DatabaseDb connection;

        ObservableCollection<Restaurant> restaurants { get; set; } = new ObservableCollection<Restaurant>();

        public MainWindow()
        {
            InitializeComponent();
            connection = new DatabaseDb("SQLite", "Data Source=./database.db");
            this.DataContext = this;

            var query = from x in connection.Kategories select x;

        }

        private void Open_AddDataPage(object sender, RoutedEventArgs e)
        {
            AddRestaurant dialog = new AddRestaurant();
            dialog.main = this;
            dialog.ShowDialog();
            if (dialog.DialogResult == true) 
            {

            }
        }
    }
}
