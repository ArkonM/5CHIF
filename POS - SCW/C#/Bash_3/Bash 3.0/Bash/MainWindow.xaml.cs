using System.Net.Sockets;
using System.Net;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;
using System.ComponentModel;

namespace Bash
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public class Path : INotifyPropertyChanged
    {
        private string _currentPath;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        public string CurrentPath 
        { 
            get { return _currentPath; } 
            set { 
                _currentPath = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentPath"));
            }
        }

        public Path()
        {
            _currentPath = "root";
        }
    }


    public partial class MainWindow : Window
    {

        public TreeViewItem _currentTreeView = new TreeViewItem();
        public Path path = new Path();

        public MainWindow()
        {
            InitializeComponent();
            _currentTreeView.Header = "root";
            FileSystem.Items.Add(_currentTreeView);
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
                    case Command.DELETE:
                        DeleteDirektory(token);
                        break;
                    case Command.CHANGEDIRECTORY:
                        ChangeDirectory(token);
                        ShowCurrentDirectory();
                        break;
                    case Command.CREATEDIRECTORY:
                        CreateDirectory(token);
                        break;
                    case Command.LISTITEMS:
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

        private void ShowCurrentDirectory()
        {
            string currentDirectory = "Aktuelles Verzeichnis: " + GetFullPath(_currentTreeView) + "\n";
            path.CurrentPath = currentDirectory;
            printLine(currentDirectory);

            DataContext = this;
        }

        private string GetFullPath(TreeViewItem item)
        {
            if (item == null || item.Parent == null)
            {
                return item.Header.ToString();
            }
            else if (item.Parent is TreeViewItem parentItem)
            {
                return GetFullPath(parentItem) + "\\" + item.Header.ToString();
            }
            else
            {
                return item.Header.ToString();
            }
        }



        private void printLine(string str)
        {
            var temp = OutputBox.Text;
            temp += str;
            OutputBox.Text = temp;
        }
    }
}
