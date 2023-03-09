using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Schiffe_versenken
{
    /// <summary>
    /// Interaktionslogik für ServerClient.xaml
    /// </summary>
    public partial class ServerClient : Window
    {
        public bool isServer { get; set; }
        public ServerClient()
        {
            InitializeComponent();
        }

        private void ServerButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            isServer = true;
            this.Close();
        }

        private void ClientButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            isServer = false;
            this.Close();
        }
    }
}
