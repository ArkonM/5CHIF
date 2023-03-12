using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{

    public enum MessageType
    {
        MESSAGE,
        HELLO,
        BYE
    }

    public class Message
    {
        public string msg { get; set; }
    }

    public class MSG
    {
        public MessageType type { get; set; }
        public Message message { get; set; }
    }
}
