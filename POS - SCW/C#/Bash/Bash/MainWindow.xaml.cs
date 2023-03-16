using System.Net.Sockets;
using System.Net;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;

namespace Bash
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public TreeViewItem _currentTreeView = new TreeViewItem();

        public MainWindow()
        {
            InitializeComponent();
            _currentTreeView.Header = "root";
            FileSystem.Items.Add(_currentTreeView);
            DataContext = this;
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
                printLine(token.Command.ToString() + " " + token.Name.ToString() + "\n");

                switch (token.Command)
                {
                    case CommandEnum.DELETE:
                        DeleteDirektory(token);
                        break;
                    case CommandEnum.CHANGEDIRECTORY:
                        ChangeDirectory(token);
                        break;
                    case CommandEnum.CREATEDIRECTORY:
                        CreateDirectory(token);
                        break;
                    case CommandEnum.LISTITEMS:
                        ListDirektory();
                        break;
                }
            }
        }

        private void CreateDirectory(Token t)
        {
            _currentTreeView.Items.Add(new TreeViewItem() { Header = t.Name });
        }


        private void ChangeDirectory(Token t)
        {
            if (t.Name == "..")
            {
                try
                {
                    _currentTreeView = (TreeViewItem)_currentTreeView.Parent;
                } catch (Exception ex)
                {
                    MessageBox.Show("Es ist kein weiterer Parent vorhanden!", "Error!");
                }
            }
            else
            {
                foreach (TreeViewItem item in _currentTreeView.Items)
                {
                    if (item.Header.ToString() != t.Name) continue;

                    _currentTreeView = item;
                }
            }
        }


        private void DeleteDirektory(Token t)
        {
            foreach (TreeViewItem item in _currentTreeView.Items)
            {
                if (item.Header.ToString() != t.Name) continue;

                _currentTreeView.Items.Remove(item);
                return;
            }
        }


        private void ListDirektory()
        {
            var str = "Directories: ";
            foreach (TreeViewItem item in _currentTreeView.Items)
            {
                str += item.Header + "; ";
            }
            str += "\n";
            printLine(str);
        }


        private void printLine(string str)
        {
            var temp = OutputBox.Text;
            temp += str;
            OutputBox.Text = temp;
        }
    }
}
