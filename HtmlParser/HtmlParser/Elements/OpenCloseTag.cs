using System.Collections.Generic;
using System.Linq;

namespace HtmlParser
{
    public class OpenCloseTag : OpenTag
    {
        public override string ToString()
        {
            var listStr = new List<string> { Name };
            var atributeStr = Attributes.Select(atr => $"{atr.Key}=\"{atr.Value}\"");
            listStr.AddRange(atributeStr);
            return $"<{string.Join(" ", listStr)}/>";
        }

        public OpenCloseTag(string name) : base(name) { }

        public OpenCloseTag(string name, IReadOnlyCollection<Attribute> attributte) : base(name, attributte) { }
    }
}
