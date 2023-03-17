using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA3_Math
{
    internal class MalDivMod : Expression
    {
        private Expression left;
        private Expression right;
        private Token operand;

        public static new Expression Parse(List<Token> context)
        {
            Expression l = HochQuadrat.Parse(context);

            while (context.Count != 0 && (context[0].Value == "*" || context[0].Value == "/" || context[0].Value == "%"))
            {
                MalDivMod p = new MalDivMod();
                p.left = l;
                p.operand = context[0];
                context.RemoveAt(0);
                p.right = HochQuadrat.Parse(context);
                switch (p.operand.Value)
                {
                    case "*":
                        p.Name = "Mal";
                        break;
                    case "/":
                        p.Name = "Dividiert";
                        break;
                    case "%":
                        p.Name = "Modulo";
                        break;
                }
                p.Expressions.Add(p.left);
                p.Expressions.Add(p.right);
                l = p;
            }

            return l;
        }

        public override double Calculate()
        {
            switch (operand.Value)
            {
                case "*":
                    return left.Calculate() * right.Calculate();
                case "/":
                    return left.Calculate() / right.Calculate();
                default:
                    return left.Calculate() % right.Calculate();
            }
        }
    }
}
