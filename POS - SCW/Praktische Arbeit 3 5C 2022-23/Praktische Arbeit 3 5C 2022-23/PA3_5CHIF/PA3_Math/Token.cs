using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA3_Math
{
    public enum TokenType { Literal, Bracket, Operator}

    public class Token
    {
        public TokenType Type { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return Type + " " + Value;
        }
    }
}
