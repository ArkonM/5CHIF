using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace PA3_Math
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

        private Expression CurrentResult { get; set; } = null;

        private void Calculate(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Token> tokens = TokenizeRegex(Formel.Text);
                Tokens.ItemsSource = tokens.ToList();
                CurrentResult = Expression.Parse(tokens);
                List<Expression> tree = new List<Expression>();
                tree.Add(CurrentResult);
                Expressions.ItemsSource = tree;
                Result.Text = "=" + CurrentResult.Calculate().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            Formel.Text = "";
            Result.Text = "";
            Tokens.ItemsSource = null;
        }

        private void AddSymbol(object sender, RoutedEventArgs e)
        {
            Formel.Text += (sender as Button).Content;
        }

        List<Token> TokenizeRegex(String context)
        {
            List<Token> tokens = new List<Token>();

            Regex regex = new Regex("([a-zA-Z]+|(?<=[\\*\\-+/^%])-[0-9,]+|\\^|\\)|\\(|\\*|\\+|-|/|²|%|[0-9,]+)");
            MatchCollection match = regex.Matches(context);
            Regex literal = new Regex("-?[0-9,]+");
            Regex variable = new Regex("[a-zA-Z]+");
            Regex operation = new Regex("[\\*\\-+/^²%]");
            Regex braces = new Regex("[\\)\\(]");

            foreach (Match m in match)
            {
                Token token = new Token();
                token.Value = m.Value;
                if (literal.IsMatch(token.Value))
                {
                    token.Type = TokenType.Literal;
                }
                else if (operation.IsMatch(token.Value))
                {
                    token.Type = TokenType.Operator;
                }
                else if (braces.IsMatch(token.Value))
                {
                    token.Type = TokenType.Bracket;
                }
                else
                {
                    throw new Exception("Unrecognized Token-Type");
                }
                tokens.Add(token);
            }

            return tokens;
        }
    }
}
