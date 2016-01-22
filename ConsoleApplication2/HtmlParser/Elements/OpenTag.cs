using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication2
{
    public class OpenTag : ITag
    {
        public string Name { get; }
        public IReadOnlyDictionary<string, string> Attributes { get; }
        public override string ToString() =>
            $"<{Name} {string.Join(" ", Attributes.Select(atr => $"{atr.Key}=\"{atr.Value}\""))}>";

        public OpenTag(string name, IReadOnlyCollection<Attribute> atributte)
        {
            Name = name;
            Attributes = atributte.ToDictionary(atr => atr.Name, atr => atr.Value);
        }
    }
}
