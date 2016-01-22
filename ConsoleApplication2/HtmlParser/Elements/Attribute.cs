namespace ConsoleApplication2
{
    public class Attribute
    {
        public string Name { get; }
        public string Value { get; }

        public override string ToString() =>
            $"{Name}=\"{Value}\"";

        public Attribute(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
