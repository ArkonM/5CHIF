using DataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PA3Z_2022_23
{
    /// <summary>
    /// Interaction logic for MapRoute.xaml
    /// </summary>
    public partial class MapRoute : Window
    {
        public class point
        {
            public int x { get; set; }
            public int y { get; set; }

            public point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public List<Airport> airports = new List<Airport>();
        public List<point> points = new List<point>();
        public MapRoute(List<Airport> airports)
        {
            InitializeComponent();
            this.airports = airports;
        }

        public void showRoute(List<Airport> airports)
        {
            foreach (var ariport in airports)
            {
                drawAirport(ariport);
            }
            drawLines();
        }

        public void drawLines()
        {
            for (int i = 0; i < (points.Count - 1); i++)
            {
                Line l = new Line();
                l.X1 = points[i].x;
                l.X2 = points[i + 1].x;
                l.Y1 = points[i].y;
                l.Y2 = points[i + 1].y;
                l.Stroke = Brushes.Red;

                map.Children.Add(l);
                Debug.WriteLine("Linie ahoi");
            }
        }

        public void drawAirport(Airport airport)
        {
            double canvasWidth = map.ActualWidth;
            double canvasHeight = map.ActualHeight;

            double minLongitude = -180; // Minimaler Wert der Längengrade
            double maxLongitude = 180; // Maximaler Wert der Längengrade
            double minLatitude = -90; // Minimaler Wert der Breitengrade
            double maxLatitude = 90; // Maximaler Wert der Breitengrade


            double? longitude = airport.Longitude; // Beispielwert für Längengrad
            double? latitude = airport.Latitude; // Beispielwert für Breitengrad
            Debug.WriteLine("logn: " + longitude.ToString() + " lat: " + latitude.ToString());
            // Berechne die X-Koordinate in Pixeln
            int x = (int)(canvasWidth * (longitude - minLongitude) / (maxLongitude - minLongitude));

            // Berechne die Y-Koordinate in Pixeln
            int y = (int)(canvasHeight * (maxLatitude - latitude) / (maxLatitude - minLatitude));
            Debug.WriteLine("x: " + x.ToString() + " y: " + y.ToString());
            Ellipse ellipse = new Ellipse();
            ellipse.Width = 8;
            ellipse.Height = 8;
            ellipse.Fill = Brushes.Red;
            Canvas.SetLeft(ellipse, x-4);
            Canvas.SetTop(ellipse, y-4);
            points.Add(new point(x, y));
            map.Children.Add(ellipse);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            showRoute(airports);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                map.Children.Clear();
                points.Clear();
                showRoute(airports);
            }
        }
    }
}
