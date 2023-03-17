using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace PA3_Math
{
    internal class Number : Expression
    {
        private double value;
        public static Expression Parse(List<Token> context)
        {
            if (context.Count > 0 && context[0].Type == TokenType.Literal)
            {
                Number n = new Number();
                if (double.TryParse(context[0].Value, out n.value))
                {
                    n.Name = context[0].Value;
                    context.RemoveAt(0);
                    return n;
                }
                else
                {
                    throw new Exception("Invalid literal " + context[0].Value);
                }
            }
            else
            {
                throw new Exception("Missing literal");
            }
        }

        public override double Calculate()
        {
            return value;
        }
    }
}
