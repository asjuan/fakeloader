﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var orderDetails = RetrieveFake.From(@"..\..\Resources\OrderDetails.txt").SeparateBy('\t').GetAListOf<OrderDetail>();
            Assert.AreEqual(orderDetails.Count, 5);
        }
    }
}
