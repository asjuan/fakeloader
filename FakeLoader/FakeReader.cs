using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace FakeLoader
{
    public class FakeReader
    {
        public IEnumerable<string> GetLinesFromFile(string path)
        {
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    yield return reader.ReadLine();
                }
            }
        }
        public List<string[]> GetLinesSplitedBy(IEnumerable<string> items, char separator)
        {
            return items.Select(o => o.Split(separator)).ToList();
        }
        public IEnumerable<T> GetInstances<T>(List<string[]> items, FakeMapper<T> mapper) where T : new()
        {
            return items.Where(o => !string.IsNullOrEmpty(o[0])).Skip(1).Select(mapper.PickInstance);
        }
        

        internal List<T1> GetInstances<T1>(IEnumerable<string[]> items, System.Func<string[], T1> func)
        {
            return items.Where(o => !string.IsNullOrEmpty(o[0])).Skip(1).Select(o=>func(o)).ToList();
        }
    }
}
