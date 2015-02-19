namespace FakeLoader
{
    interface IParseResolver
    {
        IParseResolver GetResolver(string name);

        object Parse(string value);
    }
}
