using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA3Z_2022_23
{
    public abstract class Expression
    {
        public static Expression Parse(List<MainWindow.planningRoute> tokens)
        {
            Expression expression = RoutesParser.Parse(tokens);
            
            return expression;
        }

        public abstract bool CheckRoute(MainWindow.planningRoute lastAirport);
    }
}
