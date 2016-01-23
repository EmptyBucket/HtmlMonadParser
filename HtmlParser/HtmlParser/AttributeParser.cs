using System.Linq;
using Sprache;

namespace HtmlParser
{
    public static class AttributeParser
    {
        private static Parser<string> AttributeName =>
            Parse.CharExcept(" \"'>/=").Many().Text().Token();

        private static Parser<string> UnquotedAttributeValue =>
            Parse.CharExcept("\"'=<>`").Many().Text().Token();

        private static Parser<char> SingleQuote => Parse.Char('\'').Token();

        private static Parser<char> DoubleQuote => Parse.Char('"').Token();

        private static Parser<string> QuotedAttributeValue(Parser<char> quote) =>
            (from doubleQuoteOpen in quote
             from content in Parse.AnyChar.Except(quote).Many().Text()
             from doubleQuoteClose in quote
             select content).Token();

        private static Parser<string> AttributeValue =>
            QuotedAttributeValue(DoubleQuote).XOr(QuotedAttributeValue(SingleQuote).XOr(UnquotedAttributeValue));

        public static Parser<Attribute> Attribute =>
           
           from name in AttributeName
           from equal in Parse.Char('=').Token()
           from value in AttributeValue
           select new Attribute(name, value);
    }
}
