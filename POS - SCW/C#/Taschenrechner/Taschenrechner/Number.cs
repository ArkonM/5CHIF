using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taschenrechner
{
    internal class Number : Expression
    {

        public Token number;

        public static Expression Parse(List<Token> tokens)
        {
            Number number = new Number();
            number.number = tokens[0];
            tokens.RemoveAt(0);
            return number;
        }

        public override double Calculate()
        {
            double result = 0;
            double.TryParse(number.Value, out result);
            return result;
        }
    }
}
