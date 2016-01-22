using System.Linq;
using Sprache;

namespace ConsoleApplication2
{
    public static class HtmlParser
    {
        private static Parser<string> Content => Parse.CharExcept('<').Many().Text();

        private static Parser<string> Item => ShortNode.Or(FullNode.Or(Content));

        private static Parser<string> FullNode =>
            from openTag in TagParser.OpenTag
            from content in Item.Many()
            from closeTag in TagParser.CloseTag
            select openTag + string.Join(string.Empty, content) + closeTag;

        private static Parser<string> ShortNode => TagParser.OpenCloseTag.Select(tag => tag.ToString());

        private static Parser<string> TagNameAttribute(string nameTag, Attribute attribute) =>
            from openTag in TagParser.OpenTag
            where openTag.Name == nameTag
            where ((OpenTag)openTag).Attributes.Any(atr => atr.Key == attribute.Name && atr.Value == attribute.Value)
            from content in Item.Many()
            from closeTag in TagParser.CloseTag
            select string.Join(string.Empty, content);

        public static Parser<string> FindTag(string nameTag, Attribute attribute) =>
            from dirt in Parse.AnyChar.Except(TagNameAttribute(nameTag, attribute)).Many()
            from content in TagNameAttribute(nameTag, attribute)
            select content;
    }
}
