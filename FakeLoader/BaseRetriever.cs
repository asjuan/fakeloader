using System.Collections.Generic;
namespace FakeLoader
{
    public class BaseRetriever
    {
        private static IEnumerable<string> _lines;
        private List<string[]> _splitedItems;

        public BaseRetriever From(string source)
        {
            var reader = new FakeReader();
            _lines = reader.GetLinesFromFile(source);
            return this;
        }
        public BaseRetriever DelimitBy( char delimiter)
        {
            var reader = new FakeReader();
            _splitedItems = reader.GetLinesSplitedBy(_lines, delimiter);
            return this;
        }
        public BaseRetriever DelimitBy( ColumnDelimiter delimiter)
        {
            return DelimitBy( Utils.GetChar(delimiter));
        }

        public  List<T> GetAListOf<T>(MapperConfiguration configuration = null) where T : new()
        {
            if (configuration == null)
            {
                configuration = new MapperConfiguration
                {
                    DefaultPropertyReader = PropertyReader.SkipHeaders
                };
            }
            var reader = new FakeReader();
            var mapper = new FakeMapper<T>();

            return reader.GetInstances<T>(_splitedItems, configuration, mapper);
        }
    }
}
