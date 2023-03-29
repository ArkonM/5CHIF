using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA3_Math
{
    internal class HochQuadrat : Expression
    {
        private Expression left;
        private Expression right;

        public static new Expression Parse(List<Token> context)
        {
            Expression l = null;
            if (context.Count > 0)
            {

                if (context[0].Type == TokenType.Bracket)
                {
                    l = Klammer.Parse(context);
                }
                else if (context[0].Type == TokenType.Literal)
                {
                    l = Number.Parse(context);
                }
                else
                {
                    throw new Exception("Unexpected Token " + context[0].Value);
                }
            }
            else
            {
                throw new Exception("Unexpected End of Input");
            }
            if (context.Count > 0 && context[0].Value == "^")
            {
                context.RemoveAt(0);
                HochQuadrat h = new HochQuadrat();
                h.left = l;
                h.right = HochQuadrat.Parse(context);
                h.Name = "Hoch";
                h.Expressions.Add(h.left);
                h.Expressions.Add(h.right);
                return h;
            } else if (context.Count > 0 && context[0].Value == "²")
            {
                context[0].Value = "2";
                context[0].Type = TokenType.Literal;
                HochQuadrat h = new HochQuadrat();
                h.left = l;
                h.right = HochQuadrat.Parse(context);
                h.Name = "Hoch";
                h.Expressions.Add(h.left);
                h.Expressions.Add(h.right);
                return h;
            }
            return l;
        }

        public override double Calculate()
        {
            return Math.Pow(left.Calculate(), right.Calculate());
        }
    }
}
