namespace FakeLoader
{
    class Int32Resolver:IParseResolver
    {
        public IParseResolver GetResolver(string name)
        {
            if (name.Contains("Int32")) return this;
            return new BooleanResolver().GetResolver(name);
        }
        public object Parse(string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            if (value.ToUpper().Equals("NULL")) return null;
            return int.Parse(value);
        }
    }
}
