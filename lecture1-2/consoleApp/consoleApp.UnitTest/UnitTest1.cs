using static consoleApp.Program;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }


    [Test]
    public void ToDoubleTest()
    {
        Fraction fraction = new Fraction(1, 4);
        double ans = 0.25;
        Assert.AreEqual(fraction.ToDouble(), ans);
    }

    [Test]
    public void additionTest()
    {
        Fraction fraction = new Fraction(2, 3);
        Fraction fraction2 = new Fraction(2, 6);
        Fraction sum = new Fraction(1, 1);
        Assert.AreEqual(fraction + fraction2, sum);
    }
    [Test]
    public void subtractionTest()
    {
        Fraction fraction = new Fraction(2, 3);
        Fraction fraction2 = new Fraction(4, 6);
        Fraction sum = new Fraction(0, 1);
        Assert.AreEqual(fraction - fraction2, sum);
    }
    [Test]
    public void multiplicationTest()
    {
        Fraction fraction = new Fraction(2, 3);
        Fraction fraction2 = new Fraction(2, 6);
        Fraction sum = new Fraction(4, 18);
        Assert.AreEqual(fraction * fraction2, sum);
    }
    [Test]
    public void divisionTest()
    {
        Fraction fraction = new Fraction(2, 6);
        Fraction fraction2 = new Fraction(2, 6);
        Fraction sum = new Fraction(1, 1);
        Assert.AreEqual(fraction / fraction2, sum);
    }
    [Test]
    public void equalsTest()
    {
        Fraction fraction = new Fraction(1, 4);
        Fraction fraction2 = new Fraction(1, 4);
        Fraction fraction3 = new Fraction(2, 4);
        Assert.AreEqual(fraction == fraction2, true);
        Assert.AreEqual(fraction == fraction3, false);
    }
    [Test]
    public void notEqualsTest()
    {
        Fraction fraction = new Fraction(1, 4);
        Fraction fraction2 = new Fraction(3, 4);
        Fraction fraction3 = new Fraction(3, 4);
        Assert.AreEqual(fraction != fraction2, true);
        Assert.AreEqual(fraction2 != fraction3, false);
    }
    [Test]
    public void greaterThanTest()
    {
        Fraction fraction = new Fraction(1, 4);
        Fraction fraction2 = new Fraction(3, 4);
        Fraction fraction3 = new Fraction(3, 4);
        Assert.AreEqual(fraction > fraction2, false);
        Assert.AreEqual(fraction2 > fraction, true);
        Assert.AreEqual(fraction2 > fraction3, false);
    }
    [Test]
    public void lesserThanTest()
    {
        Fraction fraction = new Fraction(1, 4);
        Fraction fraction2 = new Fraction(3, 4);
        Fraction fraction3 = new Fraction(3, 4);
        Assert.AreEqual(fraction < fraction2, true);
        Assert.AreEqual(fraction2 < fraction, false);
        Assert.AreEqual(fraction2 < fraction3, false);
    }
    [Test]
    public void reductionTest()
    {
        Fraction fraction = new Fraction(10, 8);
        Fraction fraction2 = new Fraction(5, 4);
        Assert.AreEqual(fraction == fraction2, true);
    }
    [Test]
    public void throwErrorTest()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Fraction(10, 0));
    }
    [Test]
    public void implicitCastToDoubleTest()
    {
        Fraction fraction = new Fraction(1, 4);
        double convert = fraction;
        double expected = 0.25;
        Assert.AreEqual(expected, convert);
    }


}