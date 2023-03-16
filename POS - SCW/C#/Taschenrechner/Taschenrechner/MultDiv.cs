using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taschenrechner
{
    internal class MultDiv : Expression
    {

        public Expression left;
        public Expression right;
        public Token operatorToken;

        public static Expression Parse(List<Token> tokens)
        {
            Expression leftExpr = Power.Parse(tokens);
            while (tokens.Count > 0 && tokens[0].Type == TokenType.MultDiv)
            {
                MultDiv md = new MultDiv();
                md.left = leftExpr;
                md.operatorToken = tokens[0];
                tokens.RemoveAt(0);
                md.right = Power.Parse(tokens);
                leftExpr = md;
            }

            return leftExpr;
        }

        public override double Calculate()
        {
            if(operatorToken.Value.Equals("*")) {
                return left.Calculate() * right.Calculate();
            }

            return left.Calculate() / right.Calculate();
        }
    }
}
