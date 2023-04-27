using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamster_Parser
{
    public enum TokenType
    {
        COMMAND,
        NUMBER,
        BRACKET,
        SEMICOLON
    }

    public class Token
    {
        public TokenType type { get; set; }
        public string value { get; set; }

        public Token(TokenType type, string value)
        {
            this.type = type;
            this.value = value;
        }

        public static List<Token> tokenize(string input)
        {
            List<Token> tokens = new List<Token>();

            string temp = "";

            for(int i = 0; i < input.Length; i++)
            {
                if (input[i] == ';')
                {
                    tokens.Add(new Token(TokenType.SEMICOLON, ","));

                } else if (input[i] == '(')
                {
                    tokens.Add(new Token(TokenType.COMMAND, temp));
                    temp = "";
                    tokens.Add(new Token(TokenType.BRACKET, "("));

                    if (input[i+1] == ')')
                    {
                        tokens.Add(new Token(TokenType.BRACKET, "("));
                        i++;
                    }

                } else if (input[i] == ')')
                {

                    tokens.Add(new Token(TokenType.NUMBER, temp));
                    temp = "";
                    tokens.Add(new Token(TokenType.BRACKET, ")"));

                } else
                {
                    temp += input[i];
                }
            }

            return tokens;
        }
    }
}
