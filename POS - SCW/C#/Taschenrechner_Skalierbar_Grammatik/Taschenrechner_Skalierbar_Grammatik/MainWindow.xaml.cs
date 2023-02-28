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

namespace Taschenrechner_Skalierbar_Grammatik
{
    public partial class MainWindow : Window
    {
        List<string> tokenList = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
        }

        


        private void Input_Btn_Result_OnClick(object sender, RoutedEventArgs e)
        {
            if (calcInput_TxB.Text.Length == 0)
            {
                return;
                
            } 
            else
            {
                string input_txt = calcInput_TxB.Text;
                foreach (string input in input_txt.Split(" "))
                {
                    tokenList.Add(input);
                }
            }
        }

        private void input_Btn_0_Click(object sender, RoutedEventArgs e)
        {

        }

        private void input_Btn_1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void input_Btn_2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void input_Btn_3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void input_Btn_4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void input_Btn_5_Click(object sender, RoutedEventArgs e)
        {

        }

        private void input_Btn_6_Click(object sender, RoutedEventArgs e)
        {

        }

        private void input_Btn_7_Click(object sender, RoutedEventArgs e)
        {

        }

        private void input_Btn_8_Click(object sender, RoutedEventArgs e)
        {

        }

        private void input_Btn_9_Click(object sender, RoutedEventArgs e)
        {

        }

        private void input_Btn_Plus_Click(object sender, RoutedEventArgs e)
        {

        }

        private void input_Btn_Minus_Click(object sender, RoutedEventArgs e)
        {

        }

        private void input_Btn_Mal_Click(object sender, RoutedEventArgs e)
        {

        }

        private void input_Btn_Divide_Click(object sender, RoutedEventArgs e)
        {

        }

        private void input_Btn_Vorzeichen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void input_Btn_Comma_Click(object sender, RoutedEventArgs e)
        {

        }

        private void input_Btn_KlammerAuf_Click(object sender, RoutedEventArgs e)
        {

        }

        private void input_Btn_KlammerZu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void input_Btn_Square_Click(object sender, RoutedEventArgs e)
        {

        }

        private void input_Btn_Clear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void input_Btn_Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
