﻿using System.Collections.Generic;
namespace FakeLoader
{
    public class RetrieveFake
    {
        public static BaseRetriever From(string source)
        {
            var retriever = new BaseRetriever();
            return retriever.From(source);
        }
    }
}