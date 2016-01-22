namespace ConsoleApplication2
{
    public class OpenCloseTag : ITag
    {
        public string Name { get; }
        public override string ToString() =>
            $"<{Name}/>";

        public OpenCloseTag(string name)
        {
            Name = name;
        }
    }
}
