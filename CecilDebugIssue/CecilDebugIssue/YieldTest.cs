using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

[TestFixture]
public class YieldTest
{
    [Test]
    public void TestYield()
    {
        var list = MethodWithYield().ToList();
    }

    IEnumerable<DateTime> MethodWithYield()
    {
        var dateTime = DateTime.Now;
        yield return dateTime;
    }

}