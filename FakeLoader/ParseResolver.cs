using System;
namespace FakeLoader
{
    class ParseResolver:IParseResolver
    {
        public IParseResolver GetResolver(string name) {
            return new ByteResolver().GetResolver(name);
        }

        public object Parse(string value)
        {
            throw new NotImplementedException();
        }
    }
}
