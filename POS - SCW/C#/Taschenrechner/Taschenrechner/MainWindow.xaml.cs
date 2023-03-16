using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Taschenrechner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string input = "-7.13+(4*2)^3/2"; //"-(2.3*21)+2^2-31*-1^3";
            Console.WriteLine(input);
            var tokens = Token.GetTokensFromString(input);
            
            foreach(var token in tokens)
            {
                Console.WriteLine(token.Value + " " + token.Type);
            }

            var expr = Expression.Parse(tokens);
            double result = expr.Calculate();

            Console.WriteLine(result);

        }
    }
}
