namespace MilCalc;

public class Tests
{
    [Test]
    public void MeterToMil1600()
    {
        var expectedMeters = 1600;
        Int32 expectedMils = 622;
        var actualMils = MilCalc.Mils(expectedMeters);
        Assert.That(actualMils, Is.EqualTo(expectedMils));
    }

    [Test]
    public void MeterToMil400()
    {
        var expectedMeters = 400;
        Int32 expectedMils = 907;
        var actualMils = MilCalc.Mils(expectedMeters);
        Assert.That(actualMils, Is.EqualTo(expectedMils));
    }
}
