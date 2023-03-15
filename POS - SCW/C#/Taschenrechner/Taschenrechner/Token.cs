using System;
using System.Collections.Generic;
using System.Linq;

namespace Taschenrechner
{

    enum TokenType { Bracket, PlusMinus, MultDiv, Power, Number}

    class Token
    {

        string value = "";
        TokenType type;

        public Token() { }
        public Token(string value) { 
            this.value = value; 
        }
        public Token(string value, TokenType type)
        {
            this.value = value;
            this.type = type;
        }


        //50.3 -50.3 

        private static bool IsNumeric(string number)
        {
            return int.TryParse(number, out var digit);
        }

        private static TokenType GetType(string c)
        {
            TokenType type;
            switch (c)
            {
                case "(":
                case ")": type = TokenType.Bracket; break;
                case "+":
                case "-": type = TokenType.PlusMinus; break;
                case "*":
                case "/": type = TokenType.MultDiv; break;
                case "^": type = TokenType.Power; break;
                default: type = TokenType.Number; break;
            }

            return type;
        }

        public static List<Token> GetTokensFromString(string input)
        {
            List<Token> tokens = new List<Token>();

            string copy = (string)input.Clone();
            string currentString = "";
            for(int i= 0; i < copy.Length; i++)
            {
                if (Token.IsNumeric(copy[i].ToString()))
                {
                    currentString += copy[i];
                } else
                {
                    if(!currentString.Equals(""))
                    {
                        tokens.Add(new Token(currentString, TokenType.Number));
                        currentString = "";
                    }
                    tokens.Add(new Token(copy[i].ToString()));
                }
            }

            tokens.Add(new Token(currentString, TokenType.Number));

            for (int i = 1; i < tokens.Count - 1; i++)
            {
                if (tokens[i].Value.Equals("-")
                    && !Token.IsNumeric(tokens[i - 1].Value)
                    && Token.IsNumeric(tokens[i + 1].Value))
                {
                    tokens.RemoveAt(i);
                    tokens[i].Value = "-" + tokens[i].Value;
                }

                if (tokens[i].Value.Equals(".")
                    && Token.IsNumeric(tokens[i - 1].Value)
                    && Token.IsNumeric(tokens[i + 1].Value))
                {
                    tokens.RemoveAt(i);
                    tokens[i - 1].Value += "." + tokens[i].Value;
                    tokens.RemoveAt(i);
                }

            }

            foreach (Token token in tokens)
            {
                token.Type = GetType(token.Value);
            }

            return tokens;
        }

        public string Value { get { return value; } set { this.value = value; } }
        public TokenType Type { get { return type; } set { this.type = value; } }

    }
}
