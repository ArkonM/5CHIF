using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Bash
{
    public enum Command
    {
        DELETE,
        CHANGEDIRECTORY,
        LISTITEMS,
        CREATEDIRECTORY
    }

    public class Token
    {
        public Command Command { get; set; }
        public string Name { get; set; }


        public List<Token> tokenize(string com)
        {
            List<Token> tokens = new List<Token>();
            bool commandSelected = false;
            string s = "";
            char prev = new char();

            foreach(char c in com)
            {
                commandSelected = false;
                if (c == ';')
                {
                    tokens.Add(SelectCommand(s));
                    commandSelected = true;
                    s = "";
                } 
                else if(prev == ';' && c == ' ')
                {

                }
                else
                {
                    s += c;
                }
                prev = c;
            }
            if (!commandSelected)
                MessageBox.Show("Bitte geben Sie einen gültigen aufruf ein!");

            return tokens;
        }


        public Token SelectCommand(string str)
        {
            var s = str.Split(' ');
            Token token = new Token();

            switch (s[0].ToLower())
            {
                case "cd":
                    token.Command = Command.CHANGEDIRECTORY;
                    token.Name = s[1];
                    break;
                case "ls":
                    token.Command = Command.LISTITEMS;
                    token.Name = "";
                    break;
                case "mkdir":
                    token.Command = Command.CREATEDIRECTORY;
                    token.Name = s[1];
                    break;
                case "rmdir":
                    token.Command = Command.DELETE;
                    token.Name = s[1];
                    break;
                default: throw new Exception("Unknown statement '" + str + "' found while tokenizing.");
            }
            return token;
        }
    }
}
