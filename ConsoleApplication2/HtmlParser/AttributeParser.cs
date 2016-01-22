using System.Linq;
using Sprache;

namespace ConsoleApplication2
{
    public static class AttributeParser
    {
        public static Parser<Attribute> Attribute =>
           from space in Parse.WhiteSpace
           from name in Parse.CharExcept('=').Many().Text()
           from equal in Parse.Char('=').Token()
           from openQuote in Parse.Char('"')
           from value in Parse.CharExcept('"').Many().Text()
           from closeQuote in Parse.Char('"')
           select new Attribute(name, value);
    }
}
