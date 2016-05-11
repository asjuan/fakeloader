using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeLoader;
using MockLoaderTest.DTO;
using System.Linq;
namespace MockLoaderTest
{
    [TestClass]
    public class LoaderTests
    {
        [TestMethod]
        public void ShouldGet5Lines()
        {
            var reader = new FakeReader();
            var lines = reader.GetLinesFromFile(@"..\..\Resources\OrderDetails.txt");
            Assert.AreEqual(lines.ToList().Count, 6);
        }
        [TestMethod]
        public void ShouldGetFirstItem()
        {
            var reader = new FakeReader();
            var lines = reader.GetLinesFromFile(@"..\..\Resources\OrderDetails.txt");
            var splited = reader.GetLinesSplitedBy(lines, '\t');
            var details = reader.GetInstances<OrderDetail>(splited, new FakeLoader.FakeMapper<OrderDetail>()).ToList();
            Assert.AreEqual(details.FirstOrDefault().Description, "Cake");
        }
        [TestMethod]
        public void ShouldGet5Entries()
        {
            var reader = new FakeReader();
            var lines = reader.GetLinesFromFile(@"..\..\Resources\OrderDetails.txt");
            var splited = reader.GetLinesSplitedBy(lines, '\t');
            var details = reader.GetInstances<OrderDetail>(splited, new FakeMapper<OrderDetail>()).ToList();
            Assert.AreEqual(details.ToList().Count, 5);
        }
        [TestMethod]
        public void ShouldGet5EntriesUsingSyntacticImprovements()
        {
            var orderDetails = PlainTextRetriever.From(@"..\..\Resources\OrderDetails.txt").DelimitBy('\t').GetAListOf<OrderDetail>(new MapperConfiguration { DefaultPropertyReader = PropertyReader.SkipHeaders });
            Assert.AreEqual(orderDetails.Count, 5);
        }
        [TestMethod]
        public void ShouldGet5EntriesUsingSyntacticSugarByTab()
        {
            var orderDetails = PlainTextRetriever.From(@"..\..\Resources\OrderDetails.txt").DelimitBy(ColumnDelimiter.Tab).GetAListOf<OrderDetail>(new MapperConfiguration { DefaultPropertyReader = PropertyReader.SkipHeaders });
            Assert.AreEqual(orderDetails.Count, 5);
        }
        [TestMethod]
        public void ShouldGet5EntriesUsingSyntacticSugarByComma()
        {
            var orderDetails = RetrieveFake.From(@"..\..\Resources\SameByComma.txt").DelimitBy(ColumnDelimiter.Comma).GetAListOf<OrderDetail>();
            Assert.AreEqual(orderDetails.Count, 5);
        }
        [TestMethod]
        public void ShouldGet5EntriesUsingSyntacticSugarByPipe()
        {
            var orderDetails = PlainTextRetriever.From(@"..\..\Resources\SameByPipes.txt").DelimitBy(ColumnDelimiter.Pipe).GetAListOf<OrderDetail>();
            Assert.AreEqual(orderDetails.Count, 5);
        }
        [TestMethod]
        public void ShouldGet5EntriesUsingSyntacticSugarByWhitespace()
        {
            var orderDetails = PlainTextRetriever.From(@"..\..\Resources\SameByWhitespace.txt").DelimitBy(ColumnDelimiter.WhiteSpace).GetAListOf<OrderDetail>(new MapperConfiguration { DefaultPropertyReader = PropertyReader.ReadAllFile });
            Assert.AreEqual(orderDetails.Count, 5);
        }
        [TestMethod]
        public void ShouldGet5EntriesFirstRowContainsFieldNames()
        {
            var orderDetails = PlainTextRetriever.From(@"..\..\Resources\UnsortedByPipes.txt").DelimitBy(ColumnDelimiter.Pipe).GetAListOf<OrderDetail>(new MapperConfiguration { DefaultPropertyReader = PropertyReader.UseHeadersToInferProperties });
            Assert.AreEqual(orderDetails.Count, 5);
        }
        [TestMethod]
        public void ShouldGet5ByUsingAdvancedMapper()
        {
            var orderDetails = PlainTextRetriever.From(@"..\..\Resources\SameByComma.txt")
                .DelimitBy(ColumnDelimiter.Comma)
                .GetAListOf<OrderDetail>(
                new MapperConfiguration
                {
                    DefaultPropertyReader = PropertyReader.SkipHeaders,
                    MapPositions = new string[6] { "Id", "Description", "OrderId", "Quantity", "IsPriority", "Price" }
                });
            Assert.AreEqual(orderDetails.Count, 5);
        }
    }
}
