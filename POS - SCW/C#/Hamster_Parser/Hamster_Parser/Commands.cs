using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamster_Parser
{
    public class Commands : Expression
    {
        public Expression left;
        public Expression right;
        
        public static Expression Parse(List<Token> tokens)
        {
            Expression expression = Command.Parse(tokens);
            while (tokens.Count > 0 && tokens[0].type == TokenType.SEMICOLON)
            {
                tokens.RemoveAt(0);
                Commands c = new Commands();
                c.left = expression;
                c.right = Command.Parse(tokens);
                expression = c;
            }
            return expression;
        }

        public override void Move()
        {
            left.Move();
            right.Move();
        }
    }
}
