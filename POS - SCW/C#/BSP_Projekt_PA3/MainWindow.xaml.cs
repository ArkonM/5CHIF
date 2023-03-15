using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading;
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

namespace BSP_Projekt_PA3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool isServer;
        public MainWindow()
        {

            InitializeComponent();
        }

        //Wenn auf den Server Button geclicked wird, wird diese Instanz als Server behandelt 
        private void Server_Click(object sender, RoutedEventArgs e)
        {
            isServer = true;
            Server server = new Server();
            server.Show();
            //Thread t = new Thread(server.Show);
            //t.IsBackground = true;
            //t.Start();
            this.Close();
        }
        //Wenn auf den Client Button geclicked wird, wird diese Instanz als Client behandelt 
        private void Client_Click(object sender, RoutedEventArgs e)
        {
            isServer = false;
            Client client = new Client();
            client.Show();
            //Thread t = new Thread(client.Show);
            //t.IsBackground = true;
            //t.Start();
            this.Close();
        }
    }
}
