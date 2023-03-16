using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Taschenrechner
{
    internal class Power : Expression
    {

        public Expression left;
        public Expression right;

        public static Expression Parse(List<Token> tokens)
        {

            Expression leftExpr;

            if (tokens.Count > 0 && tokens[0].Value == "(") 
            {
                leftExpr = Bracket.Parse(tokens);
            } else
            {
                leftExpr = Number.Parse(tokens);
            }

            if(tokens.Count > 0 && tokens[0].Type == TokenType.Power)
            {
                tokens.RemoveAt(0);
                Power p = new Power();
                p.left = leftExpr;
                p.right = Power.Parse(tokens);
                leftExpr = p;
            }

            return leftExpr;

        }

        public override double Calculate()
        {
            return Math.Pow(left.Calculate(), right.Calculate());
        }
    }
}
