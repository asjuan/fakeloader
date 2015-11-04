using System;
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
            return items.Where(o => !string.IsNullOrEmpty(o[0])).Skip(1)
                .Select(p => mapper.PickInstance(p, (values, pn, pos) => values[pos]));
        }

        public List<T1> GetInstances<T1>(IEnumerable<string[]> items, PropertyReader propertyReader, FakeMapper<T1> mapper) where T1 : new()
        {
            var result = items.Where(o => !string.IsNullOrEmpty(o[0]));
            if (propertyReader == PropertyReader.SkipHeaders)
            {
                result = result.Skip(1);
            }
            else if (propertyReader == PropertyReader.UseHeadersToInferProperties)
            {
                var header = result.First();
                return result.Skip(1).Select(o => mapper.PickInstance(o,
                    (values, pn, pos) =>
                    {
                        var index = header.ToList().FindIndex(t => t.ToUpper().Equals(pn.ToUpper()));
                        return values[index];
                    })).ToList();
            }
            return result.Select(o => mapper.PickInstance(o, (values, pn, pos) => values[pos])).ToList();
        }
    }
}
