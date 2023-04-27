using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace Hamster_Parser
{
    /*** 
     * runter();runter();rechts();gib(10);nimm(5);rauf();
     * Grammatik:
     * <Commands> ::= <Command>;{<Command>;}
     * <Command> ::= runter() | rauf() | rechts() | <gib> | <nimm>
     * <gib> ::= gib(<number>)
     * <nimm> ::= nimm(<number>)
     * <number> ::= <digit>{<digit>}
     * <digit> ::= 1 | 2 | 3 | ... | 9
     */
    public partial class MainWindow : Window
    {
        public static int W { get; set; } = 10;
        public static int H { get; set; } = 10;

        public static List<Field> fields = new List<Field>();
        public static int currentField = 0;
        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            FillField();

            fields[0].Color = Brushes.Red;
            Spielfeld.ItemsSource = fields;
        }

        void FillField()
        {
            for (int y = 0; y < H; y++)
            {
                for(int x = 0; x < W; x++)
                {
                    fields.Add(new Field(x, y));
                }
            }
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            List<Token> tokens = Token.tokenize(InputBox.Text);
            foreach (Token token in tokens)
            {
                Debug.WriteLine(token.value + "      " + token.type.ToString());
            }
            Expression erg = Expression.Parse(tokens);
            erg.Move();
        }
    }
}
