using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Grammar
{

    public enum Statement
    {
        Vor, LinksUm, RechtsUm
    }

    public class Token
    {
        public Statement Statement { get; set; }

        public static List<Token> Tokenize(string str)
        {
            List<Token> tokens = new List<Token>();
            string s = "";
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if (c == ';')
                {
                    tokens.Add(TokenizeStatement(s));
                    s = "";
                }
                else if (char.IsWhiteSpace(c))
                {
                    if (s != "")
                    {
                        throw new Exception("Syntax error: Expected semicolon ';' after statement '" + s + "' at index " + i + ".");
                    }
                }
                else
                {
                    s += c;
                }
            }
            if (s != "")
            {
                throw new Exception("Syntax error: Expected semicolon ';' after statement '" + s + "' at index " + str.Length + ".");
            }
            return tokens;
        }

        private static Token TokenizeStatement(string statement)
        {
            Token token = new Token();
            switch (statement)
            {
                case "vor()":
                    token.Statement = Statement.Vor;
                    break;
                case "linksUm()":
                    token.Statement = Statement.LinksUm;
                    break;
                case "rechtsUm()":
                    token.Statement = Statement.RechtsUm;
                    break;
                default:
                    throw new Exception("Unknown statement '" + statement + "' found while tokenizing.");
            }
            return token;
        }

    }

}
