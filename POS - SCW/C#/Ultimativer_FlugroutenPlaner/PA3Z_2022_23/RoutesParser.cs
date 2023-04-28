using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PA3Z_2022_23
{
    public class RoutesParser : Expression
    {
        public Expression left {  get; set; }
        public Expression right { get; set; }


        public static Expression Parse(List<MainWindow.planningRoute> tokens)
        {
            Expression expression = null;

            if (tokens.Count > 0 && tokens[0].type == "START") 
            {
                expression = START.Parse(tokens);
                tokens.RemoveAt(0);
            }
            if (expression == null)
            {
                MessageBox.Show("Route have to start with an START Airport");
                return null;
            }

            while (tokens.Count > 0 && tokens[0].type == "NEXT")
            {
                RoutesParser p = new RoutesParser();
                p.left = expression;
                p.right = NEXT.Parse(tokens);
                tokens.RemoveAt(0);
                expression = p;
            }

            if (tokens.Count == 1 && tokens[0].type == "END")
            {
                RoutesParser p = new RoutesParser();
                p.left = expression;
                p.right = END.Parse(tokens);
                tokens.RemoveAt(0);
                expression = p;
                return expression;
            }

            MessageBox.Show("Route must have an END Airport!");
            return null;
        }

        public override bool CheckRoute(MainWindow.planningRoute lastAirport)
        {

            

            return false;
        }
    }
}
