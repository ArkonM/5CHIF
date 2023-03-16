
namespace Comms
{
    public class Message
    {
        public Point FieldSize { get; set; }
        public Player Player { get; set; }
        public string Program { get; set; }
        public double Speed { get; set; }
        public string Error { get; set; }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Player
    {
        public Point Position { get; set; }
        public Direction Face { get; set; }
    }

    public enum Direction
    {
        Left, Up, Right, Down
    }
}
