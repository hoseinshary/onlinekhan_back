using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onlinekhan.SSO.Common
{
    public class ServerMessageResult
    {
        public string FaMessage { get; set; }

        public string EnMessage { get; set; }

        public MessageType MessageType { get; set; }

        public int ErrorNumber { get; set; }

        public int Id { get; set; }
    }
}
