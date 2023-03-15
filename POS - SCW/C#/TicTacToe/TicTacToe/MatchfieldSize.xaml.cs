using System.Windows;

namespace TicTacToe
{
    /// <summary>
    /// Interaktionslogik für MatchfieldSize.xaml
    /// </summary>
    public partial class MatchfieldSize : Window
    {
        public MatchfieldSize()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
