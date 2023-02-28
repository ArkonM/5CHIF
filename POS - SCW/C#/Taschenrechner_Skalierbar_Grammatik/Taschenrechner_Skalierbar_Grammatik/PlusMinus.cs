using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Taschenrechner_Skalierbar_Grammatik
{
    internal class PlusMinus : Expression
    {
        
    }

    private Expression left;
    private Expression right;
    private Token operation;

    public static Expression Parse(List<Token> context)
    {
        Expression l = MalDiv
    }
}
