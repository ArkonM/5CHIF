using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Hamster_Parser
{
    public class Command : Expression
    {
        public string value { get; set; }
        public Expression next { get; set; }
        public static Expression Parse(List<Token> tokens)
        {
            Command c = new Command();
            if(tokens.Count > 0)
            {
                if (tokens[0].value == "rauf")
                {
                    c.value = tokens[0].value;
                    tokens.RemoveAt(0);
                    tokens.RemoveAt(0);
                    tokens.RemoveAt(0);
                } 
                else if (tokens[0].value == "runter")
                {
                    c.value = tokens[0].value;
                    tokens.RemoveAt(0);
                    tokens.RemoveAt(0);
                    tokens.RemoveAt(0);
                } 
                else if (tokens[0].value == "links")
                {
                    c.value = tokens[0].value;
                    tokens.RemoveAt(0);
                    tokens.RemoveAt(0);
                    tokens.RemoveAt(0);
                } 
                else if (tokens[0].value == "rechts")
                {
                    c.value = tokens[0].value;
                    tokens.RemoveAt(0);
                    tokens.RemoveAt(0);
                    tokens.RemoveAt(0);
                } 
                else if (tokens[0].value == "gib")
                {
                    c.next = Gib.Parse(tokens);
                } 
                else if (tokens[0].value == "nimm")
                {
                    c.next = Nimm.Parse(tokens);
                } else
                {
                    MessageBox.Show("Ungültiger Command!");
                }
            }
            return c;
        }
        public override void Move()
        {
            if(value == "runter")
            {
                MainWindow.fields[MainWindow.currentField].Color = Brushes.Transparent;
                MainWindow.fields[MainWindow.currentField+MainWindow.W].Color = Brushes.Red;
                MainWindow.currentField += MainWindow.W;
            } else if (value == "rauf")
            {
                MainWindow.fields[MainWindow.currentField].Color = Brushes.Transparent;
                MainWindow.fields[MainWindow.currentField - MainWindow.W].Color = Brushes.Red;
                MainWindow.currentField -= MainWindow.W;
            } else if (value == "rechts")
            {
                MainWindow.fields[MainWindow.currentField].Color = Brushes.Transparent;
                MainWindow.currentField++;
                MainWindow.fields[MainWindow.currentField].Color = Brushes.Red;
            } else if (value == "links")
            {
                MainWindow.fields[MainWindow.currentField].Color = Brushes.Transparent;
                MainWindow.currentField--;
                MainWindow.fields[MainWindow.currentField].Color = Brushes.Red;
            } else if (next != null)
            {
                next.Move();
            }
        }
    }
}
