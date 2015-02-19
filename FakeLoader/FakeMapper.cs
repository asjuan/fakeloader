namespace FakeLoader
{
    public class FakeMapper<T> where T : new()
    {
        public T PickInstance(string[] values)
        {
            var instance = new T();
            var properties = typeof(T).GetProperties();
            for (var counter = 0; counter < properties.Length; counter += 1)
            {
                var property = properties[counter];
                var parser = new ParseResolver();
                var resolver = parser.GetResolver(property.PropertyType.FullName);
                if (resolver.GetType()!=typeof(NullResolver))
                {
                    property.SetValue(instance, resolver.Parse(values[counter]));
                }
            }
            return instance;
        }
    }
}
