using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hamster_Parser
{
    public class Nimm : Expression
    {
        public int value = 0;
        public static Expression Parse(List<Token> tokens)
        {
            Nimm nim = new Nimm();
            tokens.RemoveAt(0);
            tokens.RemoveAt(0);

            bool erg = int.TryParse(tokens[0].value, out nim.value);
            if (!erg)
            {
                MessageBox.Show(tokens[0].value + " is NaN");
            }

            tokens.RemoveAt(0);
            tokens.RemoveAt(0);
            return nim;
        }

        public override void Move()
        {
            MainWindow.fields[MainWindow.currentField].CornCount -= value;
        }
    }
}
