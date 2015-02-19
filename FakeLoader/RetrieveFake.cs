using System.Collections.Generic;
namespace FakeLoader
{
    public static class RetrieveFake
    {
        public static IEnumerable<string> From(string source)
        {
            var stub = new FakeReader();
            return stub.GetLinesFromFile(source);
        }
        public static IEnumerable<string[]> SeparateBy(this IEnumerable<string> lines, char separator)
        {
            var stub = new FakeReader();
            return stub.GetLinesSplitedBy(lines, '\t');
        }
        public static List<T> GetAListOf<T>(this IEnumerable<string[]> splitedItems) where T : new()
        {
            var stub = new FakeReader();
            var mapper = new FakeMapper<T>();
            return stub.GetInstances<T>(splitedItems, mapper.PickInstance);
        }
    }
}
