using DataModel;
using LinqToDB;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace UnglaublicherRestaurantFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public DatabaseDb connection;

        public ObservableCollection<Restaurant> restaurants { get; set; } = new ObservableCollection<Restaurant>();
        public ObservableCollection<Kategorie> categories { get; set; } = new ObservableCollection<Kategorie>();

        public MainWindow()
        {
            InitializeComponent();
            connection = new DatabaseDb("SQLite", "Data Source=./database.db");
            this.DataContext = this;
        }

        private void getCategories()
        {
            var KatQuery = from x in connection.Kategories select x;

            categories.Clear();

            Kategorie alle = new Kategorie();
            alle.Name = "Alle Anzeigen";
            alle.KatId = 0;

            categories.Add(alle);

            Combo_categories.SelectedIndex = 0;

            KatQuery.ToList().ForEach(x => { categories.Add(x); });
        }

        private void getRestaurants()
        {
            var ResQuery = from x in connection.Restaurants select x;

            restaurants.Clear();
            ResQuery.ToList().ForEach(x => { restaurants.Add(x); });

            paint_Restaurant();
        }

        private void Open_AddDataPage(object sender, RoutedEventArgs e)
        {
            AddRestaurant dialog = new AddRestaurant();
            dialog.main = this;
            dialog.ShowDialog();
            if (dialog.DialogResult == true) 
            {
                getRestaurants();
            }
        }

        private void Add_Category(object sender, RoutedEventArgs e)
        {
            Kategorie Kat = new Kategorie();
            Kat.Name = Add_Category_Name.Text;
            Add_Category_Name.Text = "";

            connection.Insert(Kat);

            getCategories();
        }

        private void paint_Restaurant()
        {
            canvas.Children.Clear();

            double canvasWidth = canvas.ActualWidth;
            double canvasHeight = canvas.ActualHeight;
            
            double minLongitude = 16.209652; // Minimaler Wert der Längengrade
            double maxLongitude = 16.281017; // Maximaler Wert der Längengrade
            double minLatitude = 47.786898; // Minimaler Wert der Breitengrade
            double maxLatitude = 47.846533; // Maximaler Wert der Breitengrade

            foreach (Restaurant res in restaurants)
            {

                double longitude = (double)res.Longitude; // Beispielwert für Längengrad
                double latitude = (double)res.Latitude; // Beispielwert für Breitengrad

                // Berechne die X-Koordinate in Pixeln
                int x = (int)(canvasWidth * (longitude - minLongitude) / (maxLongitude - minLongitude));

                // Berechne die Y-Koordinate in Pixeln
                int y = (int)(canvasHeight * (maxLatitude - latitude) / (maxLatitude - minLatitude));

                Ellipse ellipse = new Ellipse();
                ellipse.Width = 8;
                ellipse.Height = 8;
                ellipse.Fill = Brushes.Red;
                Canvas.SetLeft(ellipse, x-4);
                Canvas.SetTop(ellipse, y-4);
                canvas.Children.Add(ellipse);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            getRestaurants();
            getCategories();
        }

        private void Combo_categories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Combo_categories.SelectedItem != null && ((Kategorie)Combo_categories.SelectedItem).KatId != 0)
            {
                var query = from res in connection.Restaurants where res.KatNr == ((Kategorie)Combo_categories.SelectedItem).KatId select res;

                restaurants.Clear();
                query.ToList().ForEach(c => { restaurants.Add(c); });
            } else
            {
                getRestaurants();
            }
        }
    }
}
