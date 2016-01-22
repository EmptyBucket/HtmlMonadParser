using System.Linq;
using Sprache;

namespace ConsoleApplication2
{
    class Program
    {
        static Parser<string> TestParser =>
            from dd in HtmlParser.FindTagAttributeParser("class", "we")
            from q in HtmlParser.FindTagNameParser("span")
            select q;

        static void Main(string[] args)
        {
            string testTxt = "<dd><strong class=\"we\">22 050<span><dd class=\"currency\">,00</dd></span></strong><span class=\"currency\">Российский рубль</span></dd>";
            var result = TestParser.Parse(testTxt);
        }
    }
}
