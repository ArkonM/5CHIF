using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA3Z_2022_23
{
    public class IATA : Expression
    {

        public static Expression Parse(List<MainWindow.planningRoute> tokens)
        {
            
            if (tokens.Count > 0 && tokens[0].IATA.Length == 3) 
            {
                return new IATA();
            }
            return null;
        }
        public override bool CheckRoute(MainWindow.planningRoute lastAirport)
        {
            throw new NotImplementedException();
        }
    }
}
