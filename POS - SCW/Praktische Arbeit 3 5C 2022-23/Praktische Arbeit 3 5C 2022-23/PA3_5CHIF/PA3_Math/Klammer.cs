using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace PA3_Math
{
    internal class Klammer : Expression
    {
        private Expression content;
        public static new Expression Parse(List<Token> context)
        {
            if (context.Count > 0 && context[0].Value == "(")
            {
                context.RemoveAt(0);
                Expression e = PlusMinus.Parse(context);

                if (context.Count > 0 && context[0].Value == ")")
                {
                    context.RemoveAt(0);
                    Klammer h = new Klammer();
                    h.Name = "Klammer";
                    h.Expressions.Add(e);
                    h.content = e;
                    return h;
                }
                else
                {
                    throw new Exception("Missing )");
                }
            }
            else
            {
                throw new Exception("Missing (");
            }
        }

        public override double Calculate()
        {
            return content.Calculate();
        }
    }
}
