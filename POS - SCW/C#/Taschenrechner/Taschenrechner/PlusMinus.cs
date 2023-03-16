using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taschenrechner
{
    internal class PlusMinus : Expression
    {

        public Expression left;
        public Expression right;
        public Token operatorToken;

        public static Expression Parse(List<Token> tokens)
        {

            Expression leftExpr = MultDiv.Parse(tokens);
            while(tokens.Count > 0 && tokens[0].Type == TokenType.PlusMinus) {
                PlusMinus pm = new PlusMinus();
                pm.left= leftExpr;
                pm.operatorToken = tokens[0];
                tokens.RemoveAt(0);
                pm.right = MultDiv.Parse(tokens);
                leftExpr = pm;
            }

            return leftExpr;

        }

        public override double Calculate()
        {
            if(operatorToken.Value.Equals("+"))
            {
                return left.Calculate() + right.Calculate();
            }

            return left.Calculate() - right.Calculate();
        }

    }
}
