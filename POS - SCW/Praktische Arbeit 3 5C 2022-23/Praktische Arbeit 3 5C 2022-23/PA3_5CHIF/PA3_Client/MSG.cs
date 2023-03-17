using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PA3_Client
{
    public enum MessageType { NEW, PICK, EXPLODE, SAVE }

    public class Config
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Mines { get; set; }
    }

    public class Pick
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Mines
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Count { get; set; }
    }

    public class MSG
    {
        public MessageType Type { get; set; }
        public Config? Config { get; set; }
        public Pick? Pick { get; set; }
        public List<Mines>? Nearby { get; set; }
    }
}