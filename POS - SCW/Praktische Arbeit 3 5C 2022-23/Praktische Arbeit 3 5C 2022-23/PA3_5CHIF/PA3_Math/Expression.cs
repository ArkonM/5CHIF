using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA3_Math
{
    public class VariableValue
    {
        public double Value { get; set; } = 0;
    }

    public abstract class Expression
    {
        public List<Expression> Expressions { get; set; } = new List<Expression>();
        public String Name { get; set; }

        public static Expression Parse(List<Token> context)
        {
            Expression expr = PlusMinus.Parse(context);
            if(context.Count > 0)
            {
                throw new Exception("Unexpected End with Token " + context[0]);
            }
            return expr;
        }

        public abstract double Calculate();
    }
}
