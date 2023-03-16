using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public enum MessageType
    {
        CONFIG,
        PLAYFIELD,
        HELLO,
        BYE
    }

    public class Config
    {
        public int fieldSize { get; set; }
        public bool startPlayer { get; set; }
    }

    public class PlayfieldIndex
    {
        public int indx { get; set; }
    }

    public class Play
    {
        public MessageType type { get; set; }
        public PlayfieldIndex index { get; set; }
        public Config Config { get; set; }
    }
}
