using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taschenrechner
{
    internal class Bracket
    {

        public static Expression Parse(List<Token> tokens)
        {
            Expression leftExpr = null;
            if(tokens.Count > 0 && tokens[0].Type == TokenType.Bracket)
            {
                tokens.RemoveAt(0);
                leftExpr = PlusMinus.Parse(tokens);
                tokens.RemoveAt(0);
            }

            return leftExpr;
        }

    }
}
