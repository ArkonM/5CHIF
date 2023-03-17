using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PA3_Math
{
    internal class PlusMinus : Expression
    {
        private Expression left;
        private Expression right;
        private Token operand;

        public static Expression Parse(List<Token> context)
        {
            Expression l = MalDivMod.Parse(context);

            while(context.Count != 0 && (context[0].Value == "+" || context[0].Value == "-"))
            {
                PlusMinus p = new PlusMinus();
                p.left = l;
                p.operand = context[0];
                context.RemoveAt(0);
                p.right = MalDivMod.Parse(context);
                p.Name = p.operand.Value == "+" ? "Plus" : "Minus";
                p.Expressions.Add(p.left);
                p.Expressions.Add(p.right);
                l = p;
            }

            return l;
        }

        public override double Calculate()
        {
            return operand.Value == "+" ? left.Calculate() + right.Calculate() : left.Calculate() - right.Calculate();
        }
    }
}
