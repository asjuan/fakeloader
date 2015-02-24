using System.Collections.Generic;
namespace FakeLoader
{
    public static class RetrieveFake
    {
        public static IEnumerable<string> From(string source)
        {
            var reader = new FakeReader();
            return reader.GetLinesFromFile(source);
        }
        public static IEnumerable<string[]> DelimitBy(this IEnumerable<string> lines, char delimiter)
        {
            var reader = new FakeReader();
            return reader.GetLinesSplitedBy(lines, delimiter);
        }
        public static IEnumerable<string[]> DelimitBy(this IEnumerable<string> lines, ColumnDelimiter delimiter) {
            var charDelimiter = '\t';            
            switch (delimiter)
            {
                case ColumnDelimiter.Colon:
                    charDelimiter = ':';
                    break;
                case ColumnDelimiter.Semicolon:
                    charDelimiter = ';';
                    break;
                case ColumnDelimiter.Comma:
                    charDelimiter = ',';
                    break;
                case ColumnDelimiter.WhiteSpace:
                    charDelimiter = ' ';
                    break;
                case ColumnDelimiter.Pipe:
                    charDelimiter = '|';
                    break;
            }
            return DelimitBy(lines, charDelimiter);
        }
        public static List<T> GetAListOf<T>(this IEnumerable<string[]> splitedItems, bool skipHeaders = true) where T : new()
        {
            var reader = new FakeReader();
            var mapper = new FakeMapper<T>();
            return reader.GetInstances<T>(splitedItems, skipHeaders, mapper.PickInstance);
        }
        
    }
}
