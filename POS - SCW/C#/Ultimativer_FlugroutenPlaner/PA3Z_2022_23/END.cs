using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA3Z_2022_23
{
    public class END : Expression
    {

        public static Expression Parse(List<MainWindow.planningRoute> tokens)
        {
            return IATA.Parse(tokens);
        }

        public override bool CheckRoute(MainWindow.planningRoute lastAirport)
        {
            throw new NotImplementedException();
        }
    }
}
