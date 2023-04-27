using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamster_Parser
{
    public abstract class Expression
    {
        public static Expression Parse(List<Token> context)
        {
            Expression expr = Commands.Parse(context);
            return expr;
        }

        public abstract void Move();

    }
}
