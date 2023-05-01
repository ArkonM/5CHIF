using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe2
{
    public enum MessageType
    {
        START,
        CLICK,
        END
    }

    public class CLICKINFO
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    public class MSG
    {
        public MessageType Type { get; set; }
        public CLICKINFO Info { get; set; }
    }
}
