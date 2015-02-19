using System;
namespace FakeLoader
{
    class NullResolver:IParseResolver
    {
        public IParseResolver GetResolver(string name)
        {
            return this;
        }

        public object Parse(string value)
        {
            throw new NotImplementedException();
        }
    }
}
