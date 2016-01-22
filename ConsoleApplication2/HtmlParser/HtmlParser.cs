using System.Linq;
using Sprache;

namespace ConsoleApplication2
{
    public static class HtmlParser
    {
        private static Parser<ITag> TagParser(Parser<ITag> content) =>
            from beginTag in Parse.Char('<')
            from c in content
            from endTag in Parse.Char('>')
            select c;

        private static Parser<Attribute> AttributeParser() =>
            from space in Parse.WhiteSpace
            from name in Parse.CharExcept('=').Many().Text()
            from equal in Parse.Char('=').Token()
            from openQuote in Parse.Char('"')
            from value in Parse.CharExcept('"').Many().Text()
            from closeQuote in Parse.Char('"')
            select new Attribute(name, value);

        public static Parser<ITag> OpenTagParser(string name) =>
            TagParser(
                from id in Parse.String(name).Text()
                from attributes in AttributeParser().Many()
                select new OpenTag(id, attributes.ToArray()));

        public static Parser<ITag> CloseTagParser(string name) =>
            TagParser(
                from slash in Parse.Char('/')
                from id in Parse.String(name).Text()
                select new CloseTag(id));

        private static Parser<string> FullSuchNode(string name) =>
            from openTag in OpenTagParser(name)
            from content in ItemParser(name).Many()
            from closeTag in CloseTagParser(name)
            select openTag + string.Join(string.Empty, content) + closeTag;

        private static Parser<string> ContentParser(string name) =>
            Parse.AnyChar.Except(OpenTagParser(name).Or(CloseTagParser(name))).Many().Text();

        private static Parser<string> ItemParser(string name) =>
            FullSuchNode(name).Or(ContentParser(name));

        private static Parser<string> TagNameParser(string name) =>
            from openTag in OpenTagParser(name)
            from content in ItemParser(name).Many()
            from closeTag in CloseTagParser(name)
            select string.Join(string.Empty, content);

        public static Parser<string> FindTagNameParser(string name) =>
            from dirt in Parse.AnyChar.Except(TagNameParser(name)).Many()
            from content in TagNameParser(name)
            select content;

        private static Parser<string> IdentifierParser() =>
            from first in Parse.Letter.Once()
            from rest in Parse.LetterOrDigit.Many()
            select new string(first.Concat(rest).ToArray());

        public static Parser<ITag> OpenTagParser() =>
            TagParser(
                from id in IdentifierParser()
                from attributes in AttributeParser().Many()
                select new OpenTag(id, attributes.ToArray()));

        public static Parser<ITag> CloseTagParser() =>
            TagParser(
                from slash in Parse.Char('/')
                from id in IdentifierParser()
                select new CloseTag(id));

        public static Parser<ITag> OpenCloseTagParser() =>
            TagParser(
                from id in IdentifierParser()
                from slash in Parse.Char('/')
                select new OpenCloseTag(id));

        private static Parser<string> ContentParser() => Parse.CharExcept('<').Many().Text();

        private static Parser<string> ItemParser() =>
            ShortNodeParser().Or(FullNodeParser().Or(ContentParser()));

        private static Parser<string> FullNodeParser() =>
            from openTag in OpenTagParser()
            from content in ItemParser().Many()
            from closeTag in CloseTagParser()
            select openTag + string.Join(string.Empty, content) + closeTag;

        private static Parser<string> ShortNodeParser() =>
            OpenCloseTagParser().Select(tag => tag.ToString());

        private static Parser<string> TagAttributeParser(string nameAttribute, string valueAttribute) =>
            from openTag in OpenTagParser()
            from content in ItemParser().Many()
            from closeTag in CloseTagParser()
            where ((OpenTag)openTag).Attributes.Any(atr => atr.Key == nameAttribute && atr.Value == valueAttribute)
            select string.Join(string.Empty, content);

        public static Parser<string> FindTagAttributeParser(string nameAttribute, string valueAttribute) =>
            from dirt in Parse.AnyChar.Except(TagAttributeParser(nameAttribute, valueAttribute)).Many()
            from content in TagAttributeParser(nameAttribute, valueAttribute)
            select content;
    }
}
