using NUnit.Framework;
using System;
using System.Globalization;
using WeSyncSys.Extensions;

public class TestTimeFormat {
    [Test]
    public void TestTimeFormatSimplePasses() {

        var format = "yyyyMMddHHmmss";
        var input = "20220622191433";

        var nowUtc = DateTimeOffset.UtcNow;
        var nowLocal = DateTimeOffset.Now;

        Assert.True(input.TryParseAsDateTimeOffset(out var date, format));

        Assert.AreEqual(2022, date.Year);
        Assert.AreEqual(06, date.Month);
        Assert.AreEqual(22, date.Day);
        Assert.AreEqual(19, date.Hour);
        Assert.AreEqual(14, date.Minute);
        Assert.AreEqual(33, date.Second);

        Assert.AreEqual(nowUtc.Offset, date.Offset);



        Assert.True(input.TryParseAsDateTimeOffset(out date, format, true));
        Assert.AreEqual(nowLocal.Offset, date.Offset);
    }
}