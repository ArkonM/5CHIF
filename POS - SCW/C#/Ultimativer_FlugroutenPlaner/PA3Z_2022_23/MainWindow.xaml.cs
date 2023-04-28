using DataModel;
using LinqToDB;
using LinqToDB.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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
using static LinqToDB.Common.Configuration;

namespace PA3Z_2022_23
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class planningRoute
        {

            public String type { get; set; }
            public String IATA { get; set; }
        }

        AirlineRoutesDb connection;

        public ObservableCollection<planningRoute> planningRoutes { get; set; } = new ObservableCollection<planningRoute>();
        public ObservableCollection<Airport> airports { get; set; } = new ObservableCollection<Airport>();

        public List<Airport> planningAirports { get; set; } = new List<Airport>();


        public MainWindow()
        {
            connection = new AirlineRoutesDb("SQLite", "Data Source=AirlineRoutes.db");
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Search Button
            //check if iata search string isnt empty
            if (String.IsNullOrEmpty(inputIata.Text))
            {
                MessageBox.Show("Input String is empty", "Error");
                return;
            }

            var searchString = inputIata.Text;
            Debug.WriteLine(searchString);
            airports.Clear();
            foreach ( var a in connection.Airports.Where(a => a.Iata.StartsWith(searchString)).ToList())
            {
                airports.Add(a);
            }
            Debug.WriteLine(airports.Count);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Check if Route is Valid

            //Check Gramma
            var exp = Expression.Parse(planningRoutes.ToList());

            if (exp == null)
            {
                return;
            }

            MessageBox.Show("Route is valid");
            for (int i = 0; i < planningRoutes.Count() - 1; i++)
            {
                var query = connection.Routes.LoadWith(a => a.FkRoute00).LoadWith(b => b.FkRoute20).Where(a => a.FkRoute00.Iata.Equals(planningRoutes[i].IATA) && a.FkRoute20.Iata.Equals(planningRoutes[i + 1].IATA)).ToList();

                List<Route> result = query;
                
                if (result.Count <= 0)
                {
                    SearchRoute.IsEnabled = false;
                    MessageBox.Show("Route is not possible!");
                    return;
                }
                
            }

            MessageBox.Show("Route is possible");
            SearchRoute.IsEnabled = true;


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Window Loaded
            //airports = new ObservableCollection<Airport>(connection.Airports.ToList());
            DataContext = this;
            routeType.SelectedIndex = 0;
        }


        private void SearchRoute_Click(object sender, RoutedEventArgs e)
        {
            //Show Search Route
            
            MapRoute map = new MapRoute(planningAirports);
            map.Show();
        }


        private void showAirports_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (showAirports.SelectedIndex == -1)
            {
                return;
            }
            if (routeType.SelectedIndex == -1)
            {
                MessageBox.Show("Wähl was aus du hs");
                return;
            }
            Debug.WriteLine(routeType.SelectedIndex);
            //On IATA Code Clicked

            var selectedAirport = showAirports.SelectedItem as Airport;
            //Debug.WriteLine(selectedAirport.Iata);
            planningRoutes.Add(new planningRoute() { IATA = selectedAirport.Iata, type = routeType.Text });
            planningAirports.Add(selectedAirport);
        }
    }
}
