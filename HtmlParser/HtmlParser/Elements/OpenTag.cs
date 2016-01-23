using System.Collections.Generic;
using System.Linq;

namespace HtmlParser
{
    public class OpenTag : ITag
    {
        public string Name { get; }
        public IReadOnlyDictionary<string, string> Attributes { get; }
        public override string ToString()
        {
            var listStr = new List<string> { Name };
            var atributeStr = Attributes.Select(atr => $"{atr.Key}=\"{atr.Value}\"");
            listStr.AddRange(atributeStr);
            return $"<{string.Join(" ", listStr)}>";
        }

        public OpenTag(string name)
        {
            Name = name;
            Attributes = new Dictionary<string, string>();
        }

        public OpenTag(string name, IReadOnlyCollection<Attribute> atributte)
        {
            Name = name;
            Attributes = atributte.ToDictionary(atr => atr.Name, atr => atr.Value);
        }
    }
}
