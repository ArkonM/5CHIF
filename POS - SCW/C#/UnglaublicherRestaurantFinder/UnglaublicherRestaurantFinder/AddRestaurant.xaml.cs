using DataModel;
using LinqToDB;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace UnglaublicherRestaurantFinder
{
    /// <summary>
    /// Interaction logic for AddRestaurant.xaml
    /// </summary>
    public partial class AddRestaurant : Window
    {
        public ObservableCollection<String> pictures { get; set; } = new ObservableCollection<String>();
        public ObservableCollection<Kategorie> categories { get; set; } = new ObservableCollection<Kategorie>();

        public MainWindow main;

        public AddRestaurant()
        {
            InitializeComponent();
        }


        private void getRestaurantTypes()
        {
            var query = from x in main.connection.Kategories select x;

            foreach (var category in query)
            {
                categories.Add(category);
            }
            Combo_categories.ItemsSource = categories;
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            if (Rest_Name.Text == "" || Rest_Desc.Text == "" || Review_Slider.Value < 0 || Review_Slider.Value >= 1 || Rest_Long.Text == "" || Rest_Lat.Text == "") { MessageBox.Show("Error", "Nicht alle Argumente eingegeben!"); return; }
            Restaurant restaurant = new Restaurant();
            restaurant.Name = Rest_Name.Text;
            restaurant.Description = Rest_Desc.Text;
            restaurant.Review = long.Parse(Review_Slider.Value.ToString());
            restaurant.Longitude = decimal.Parse(Rest_Long.Text);
            restaurant.Latitude = decimal.Parse(Rest_Lat.Text);
            restaurant.Kategorie = ((Kategorie)Combo_categories.SelectedItem).KatId;

            main.connection.Insert(restaurant);


            var query = from x in main.connection.Restaurants select x;

           

            foreach (var path in pictures)
            {
                Picture pic = new Picture();
                pic.Path = path;
                pic.RestId = query.ToList().Last().RestId;

                main.connection.Insert(pic);
            }

            this.DialogResult = true;
            this.Close();

        }

        private void Select_Pictures(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "Bilder des Restaurants auswählen!";
            fileDialog.ShowDialog();

            foreach(var path in fileDialog.FileNames)
            {
                pictures.Add(path);
            }
            BilderPreview.ItemsSource = pictures;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            getRestaurantTypes();
        }
    }
}
