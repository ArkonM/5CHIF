using System.Net.Sockets;
using System.Net;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Collections.Generic;

namespace Bash
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Content = "Bash";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Token> tokens = new List<Token>();
            Token t = new Token();
            try
            {
                tokens = t.tokenize(InputBox.Text);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to execute program: " + ex.Message);
                return;
            }


            foreach (var token in tokens)
            {
                var temp = OutputBox.Text;
                temp += token.Command.ToString() + " " + token.Name.ToString() + "\n";
                OutputBox.Text = temp;

                switch (token.Command)
                {
                    case Command.DELETE:
                        MessageBox.Show("DELETE");
                        break;
                    case Command.CHANGEDIRECTORY:
                        MessageBox.Show("CHANGEDIRECTORY");
                        break;
                    case Command.CREATEDIRECTORY:
                        MessageBox.Show("CREATEDIRECTORY");
                        break;
                    case Command.LISTITEMS:
                        MessageBox.Show("LISTITEMS");
                        break;
                }
            }
        }
    }
}
