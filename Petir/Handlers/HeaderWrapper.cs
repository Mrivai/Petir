using System.Collections.Generic;

namespace Petir
{
    public class HeaderWrapper
    {
        public Dictionary<string, string> Headers
        {
            get;
            private set;
        }

        public ulong Identifer
        {
            get;
            private set;
        }

        public HeaderWrapper(ulong id, Dictionary<string, string> headers)
        {
            Identifer = id;
            Headers = headers;
        }
    }
}