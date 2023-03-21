using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Osterhase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    [Table("Personen")]
    public class Person
    {
        [Column("ID", IsPrimaryKey = true)]
        public int id { get; set; }

        [Column("Name")]
        public string name { get; set; }

        [Column("Alter")]
        public int age { get; set; }

        public override string ToString()
        {
            return name + " " + age;
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataConnection dc = new DataConnection("SQLite", "Data Source=Personen.db");
            var personen = dc.GetTable<Person>();
            var query = from x in personen select x;
            PersonenListe.ItemsSource = query;


            dc.Insert(new Person() { name = "Hans", age = 50 });


            PersonenListe.ItemsSource = query;
        }
    }
}
