using NUnit.Framework;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace CSTest._25_CS_NewFeatures._06_CS10;
#if CS11

[TestFixture]
public class _02_Required
{

    [Test]
    public void TestRequired()
    {
        //Book novel = new();Error
        Book novel = new()
        {
            Isbn = "Test"
        };
    }



}

public class Book
{
    public required string Isbn { get; set; }
    public string Title { get; set; }
}

#endif


