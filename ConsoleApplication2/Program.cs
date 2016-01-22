using System.Linq;
using Sprache;

namespace ConsoleApplication2
{
    class Program
    {
        static Parser<string> TestParser =>
            from q in HtmlParser.FindTag("dd", "class", "currency")
            select q;

        static void Main(string[] args)
        {
            string testTxt = "<dd><strong class=\"we\">22 050<span><dd class=\"currency\">,00<dd></dd></dd></span></strong><span class=\"currency\">Российский рубль</span></dd>";
            var result = TestParser.Parse(testTxt);
        }
    }
}
