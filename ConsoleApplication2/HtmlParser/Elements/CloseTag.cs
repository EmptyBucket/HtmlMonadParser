namespace ConsoleApplication2
{
    public class CloseTag : ITag
    {
        public string Name { get; }
        public override string ToString() =>
            $"</{Name}>";

        public CloseTag(string name)
        {
            Name = name;
        }
    }
}
