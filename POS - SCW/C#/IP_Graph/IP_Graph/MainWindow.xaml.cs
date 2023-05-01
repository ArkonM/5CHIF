using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
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
using System.Xml.Linq;
using DataModel;
using GraphControl;
using LinqToDB;

namespace IP_Graph
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RoutingDb connection = new RoutingDb("SQLite","Data Source=Routing.db");
        List<Node> nodes = new List<Node>();
        int routeStart = -1;
        int routeEnd = -1;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void test_StartChanged(object sender, RoutedEventArgs e)
        {
            List<Router> rl = connection.Routers.ToList();
            Node temp = (sender as Graph).Start;
            for (int i = 0; i < rl.Count(); i++)
            {
                if (rl[i].City == temp.Text)
                {
                    routeStart = i;
                }
            }
        }

        private void test_EndChanged(object sender, RoutedEventArgs e)
        {
            List<Router> rl = connection.Routers.ToList();
            Node temp = (sender as Graph).End;
            for (int i = 0; i < rl.Count(); i++)
            {
                if (rl[i].City == temp.Text)
                {
                    routeEnd = i;
                }
            }
            addRoute();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            foreach(var router in connection.Routers.ToList())
            {
                Node node = new Node();
                node.Text = router.City;
                node.Colors.Add(Colors.Blue);
                test.Nodes.Add(node);
                nodes.Add(node);
            }

            foreach (var conn in connection.Connections.ToList())
            {
                Router router1 = (from x in connection.Routers where x.Id == conn.Endpoint1 select x).ToList().First();
                Router router2 = (from x in connection.Routers where x.Id == conn.Endpoint2 select x).ToList().First();

                Edge edge = new Edge();
                edge.First = test.Nodes.FirstOrDefault(a => a.Text == router1.City);
                edge.Second = test.Nodes.FirstOrDefault(a => a.Text == router2.City);
                edge.Text = conn.TransmissionTime.ToString();
                test.Edges.Add(edge);
            }
        }

        void addRoute()
        {
            if (routeStart != -1 && routeEnd != -1)
            {
                List<Router> routers = connection.Routers.ToList();
                List<int> path = Dijkstra(routeStart, routeEnd);
                Random r = new Random();
                Color c = Color.FromRgb((byte)r.Next(256), (byte)r.Next(256), (byte)r.Next(256));

                foreach (int i in path)
                {
                    Console.WriteLine(i);
                    Node n = (from a in nodes where a.Text == routers[i].City select a).First();
                    n.Colors.Add(c);
                }

                routeStart = -1;
                routeEnd = -1;
            }
        }

        List<int> Dijkstra(int src, int target)
        {
            Debug.WriteLine("algo");
            List<List<int>> path = new List<List<int>>();
            List<Router> routers = connection.Routers.ToList();
            double[] dist = new double[routers.Count()];
            bool[] inShortestSet = new bool[routers.Count()];

            for (int i = 0; i < routers.Count(); i++)
            {
                dist[i] = double.MaxValue;
                inShortestSet[i] = false;
                path.Add(new List<int>());
            }

            dist[src] = 0;

            for (int c = 0; c < routers.Count() - 1; c++)
            {
                int shortdis = minDist(dist, inShortestSet);

                inShortestSet[shortdis] = true;

                for (int v = 0; v < routers.Count(); v++)
                {
                    Connection con = (from j in connection.Connections where ((j.Endpoint1 == routers[shortdis].Id && j.Endpoint2 == routers[v].Id) || j.Endpoint1 == routers[v].Id && j.Endpoint2 == routers[shortdis].Id) select j).FirstOrDefault();
                    if (!inShortestSet[v] &&
                        con != null
                        && dist[shortdis] != int.MaxValue
                        && dist[shortdis] + con.TransmissionTime < dist[v]
                    )
                    {
                        dist[v] = dist[shortdis] + con.TransmissionTime;
                        List<int> temp = new List<int>();
                        temp.Add(shortdis);
                        temp.AddRange(path[shortdis]);
                        path[v] = temp;

                        if (v == target)
                        {
                            path[v].Insert(0, v);
                            foreach (int i in path[v])
                            {
                                Debug.WriteLine(i);
                            }
                            Debug.WriteLine("----------");
                            Debug.WriteLine(path[v].Count);
                            return path[v];
                        }
                    }
                }
            }
            return null;
        }

        int minDist(double[] dist, bool[] inShortestSet)
        {
            double min = double.MaxValue;
            int min_index = -1;

            for (int i = 0; i < dist.Length; i++)
            {
                if (inShortestSet[i] == false && dist[i] < min)
                {
                    min = dist[i];
                    min_index = i;
                }
            }

            return min_index;
        }
    }
}
