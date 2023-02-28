using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schiffeversenken_Multiplayer
{
    //Port 12345
    public enum MessageType { CONFIG, READY, SHOOT, HIT, MISS }

    public class Config
    {
        public int Width { get; set; }
        public int Height { get; set; }
        //2 3 4 5
        public List<int> Ships { get; set; } = new List<int>();
        public bool StartPlayer { get; set; }
    }

    public class Shot
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class MSG
    {
        public MessageType Type { get; set; }
        public Config? Config { get; set; }
        public Shot? Shot { get; set; }
    }
}
