﻿
namespace FakeLoader
{
    public class PlainTextRetriever
    {
        public static BaseRetriever From(string source)
        {
            var retriever = new BaseRetriever();
            return retriever.From(source);
        }
    }
}
