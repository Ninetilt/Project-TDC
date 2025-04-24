using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TDC.Constants
{
    public static class InvalidCharacters
    {
        public static readonly List<char> InvalidTitle = [';', '\\', '/'];
        public static readonly List<char> InvalidGeneral = [';', '\n'];
    }
}
