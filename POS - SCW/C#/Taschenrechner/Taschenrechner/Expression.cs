using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taschenrechner
{
    internal abstract class Expression
    {

        public static Expression Parse(List<Token> tokens)
        {
            return PlusMinus.Parse(tokens);
        }

        public abstract double Calculate();

    }
}
