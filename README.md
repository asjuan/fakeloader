## FakeLoader Library
### License

This library is provided under the Microsoft Public License (Ms-PL)

### Abstract

Fake Loader is a simple tool to retrieve data stored in plain text files. It is targeted for the .Net Framework version 4.5+.

It doesn't matter the flavor, testing is a must. There are several good practices around, sometimes to use mocks or stubs, prior to concrete implementations. But some times you just need to take a shortcut and retrieve something to test functionallity.

One common approach is to implement a MotherClass or a Factory so your tests
can invoke it. From my experience I know that it requires effort to maintain that additional piece of code. That's the problem this little library tries to address,
by storing sample data in a plain text file and mapping that data to a concrete class, so new instances can be retrieved just as you do with a factory.

#### Usage

Find below a simple example in C#

`

            var orderDetails = PlainTextRetriever
            
                                            .From(@"..\..\Resources\OrderDetails.txt")
                                            
                                              .DelimitBy(ColumnDelimiter.Tab)
                                              
                                               .GetAListOf<OrderDetail>();
                                               
            Assert.AreEqual(orderDetails.Count, 5);
`

The file OrderDetails.txt just contain tab delimited data.

Important the first row contains the headers, by default it being skipped. The sequence of the columns match the properties of the OrderDetail class, to override this behavior see the documentation.

### How to install

From Microsoft's Visual Studio open the NuGet Package Manager console and type

`
Install-Package FakeLoader
`

### How to use

FakeLoader was designed to use a fluent like syntax. The readme file includes the following example written in C#

`

            var orderDetails = PlainTextRetriever            
               .From(@"..\..\Resources\OrderDetails.txt")
               .DelimitBy(ColumnDelimiter.Tab)
               .GetAListOf<OrderDetail>();
                                               
            Assert.AreEqual(orderDetails.Count, 5);
`

!!Backward Compatibility

The RetrieveFake object is still available, but will be deprecated in future versions
#### Methods
##### From(string path)
There is only one argument, A path to the target text file must be provided.
##### DelimitBy(ColumnDelimiter delimiter)
The ColumnDelimiter enumerator contains common delimiters.
##### DelimitBy(char delimiter)
If your file doesn't include any of the delimiters listed by the ColumnDelimiter enumerator, use the
overload provided to pass in a char.
##### GetAListOf<T>()
This method gets a list of the specified class. This method will skip the first row, assuming it contains headers.

To avoid default behavior and read everything use it like in the following example
`

//To use a custom mapper

var orderDetails = PlainTextRetriever.From(@"file.txt")

                .DelimitBy(ColumnDelimiter.Comma)

                .GetAListOf<OrderDetail>(
                new MapperConfiguration
                {
                    DefaultPropertyReader = PropertyReader.SkipHeaders,
                    MapPositions = new string[6] { "Id", "Description", "OrderId", "Quantity", "IsPriority", "Price" });
`

That overload can also try to infer properties from the headers using the following switch
`
//Use headers to infer mappings

var orderDetails = PlainTextRetriever.From(@"file2.txt").DelimitBy(ColumnDelimiter.Comma)

                .GetAListOf<OrderDetail>(
                new MapperConfiguration
                {
                    DefaultPropertyReader = PropertyReader.UseHeadersToInferProperties
                }
`

### Tip
This library can be combined with Moq, so you can easily mimic responses coming from a method or property.

## Known issues
It doesn´t detect dates and GUIDs. It only get lists out of text files. If the format is not met the library throws an exception.

