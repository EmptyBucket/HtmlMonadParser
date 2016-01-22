using System.Linq;
using Sprache;

namespace ConsoleApplication2
{
    public static class TagParser
    {
        private static Parser<string> Identifier =>
            from first in Parse.Letter.Once()
            from rest in Parse.LetterOrDigit.Many()
            select new string(first.Concat(rest).ToArray());

        private static Parser<ITag> Tag(Parser<ITag> content) =>
            from beginTag in Parse.Char('<')
            from c in content
            from endTag in Parse.Char('>')
            select c;

        public static Parser<ITag> OpenTag =>
            Tag(
                from id in Identifier
                from attributes in AttributeParser.Attribute.Many()
                select new OpenTag(id, attributes.ToArray()));

        public static Parser<ITag> CloseTag =>
            Tag(
                from slash in Parse.Char('/')
                from id in Identifier
                select new CloseTag(id));

        public static Parser<ITag> OpenCloseTag =>
            Tag(
                from id in Identifier
                from slash in Parse.Char('/')
                select new OpenCloseTag(id));
    }
}
