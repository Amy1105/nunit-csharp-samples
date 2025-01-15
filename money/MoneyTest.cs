using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Money
{
    /// <summary>
	/// Tests Money
	/// </summary>
	/// 
	[TestFixture]
    public class MoneyTest
    {
        private Money f12CHF;
        private Money f14CHF;
        private Money f7USD;
        private Money f21USD;

        private MoneyBag fMB1;
        private MoneyBag fMB2;

        /// <summary>
        /// Initializes Money test objects
        /// SetUp 属性：此属性在TestFixture中用于提供一组通用函数，这些函数在调用每个测试方法之前执行。
        ///       特性：如果基类定义了一个SetUp方法，则该方法将在派生类中的每个测试方法之前被调用
        ///       可以在基类中定义一个SetUp方法，在派生类中定义另一个。NUnit将在派生类中的方法之前调用基类SetUp方法。
        ///       
        /// 如果基类SetUp方法在派生类中被重写，则NUnit将不会调用基类SetUp方法；
        /// NUnit不预期包括隐藏基方法在内的用法。请注意，每种方法可能有不同的名称；
        /// 只要两者都有[SetUp]属性，每个都将以正确的顺序被调用。
        /// 
        /// 注意：虽然可以在同一个类中定义多个SetUp方法，但你很少应该这样做。与继承层次结构中单独类中定义的方法不同，
        /// 它们的执行顺序没有保证
        /// </summary>
        /// 
        [SetUp]
        protected void SetUp()
        {
            f12CHF = new Money(12, "CHF");
            f14CHF = new Money(14, "CHF");
            f7USD = new Money(7, "USD");
            f21USD = new Money(21, "USD");

            fMB1 = new MoneyBag(f12CHF, f7USD);
            fMB2 = new MoneyBag(f14CHF, f21USD);
        }

        /// <summary>
        /// Assert that Moneybags multiply correctly
        /// </summary>
        /// 
        [Test]
        public void BagMultiply()
        {
            // {[12 CHF][7 USD]} *2 == {[24 CHF][14 USD]}
            Money[] bag = { new Money(24, "CHF"), new Money(14, "USD") };
            var expected = new MoneyBag(bag);
            Assert.That(fMB1.Multiply(2), Is.EqualTo(expected));
            Assert.That(fMB1.Multiply(1), Is.EqualTo(fMB1));
            ClassicAssert.IsTrue(fMB1.Multiply(0).IsZero);
        }

        //此属性用于标识在执行夹具中的任何测试之前调用一次的方法。它可能出现在TestFixture或SetUpFixture的方法上。
        //虽然可以在同一个类中定义多个OneTimeSetUp方法，但你很少应该这样做。与继承层次结构中单独类中定义的方法不同，它们的执行顺序没有保证。
        //如果在下运行，OneTimeSetUp方法可能是异步的。NET 4.0或更高版本。
        //OneTimeSetUp方法在TestFixture或SetUpFixture的上下文中运行，这与任何单个测试用例的上下文都是分开的。
        //在使用TestContext方法和方法中的属性时，记住这一点很重要。
        //将FixtureLifeCycle与LifeCycle一起使用时。InstancePerTestCase，OneTimeSetUp方法必须是静态的，并且只调用一次。这是必需的，这样安装方法就不会访问为每个测试重置的实例字段或属性。
        //当在基类上设置该方法时，会为从该基类继承的每个夹具调用该方法，如果该基类不是抽象的，也会为该基类调用该方法。如果只需要运行一次，请使用SetUpFixture，或者将代码放入静态构造函数中。
        [OneTimeSetUp]
        public void Init()
        { /* ... */ }

        [OneTimeTearDown]
        public void Cleanup2()
        { /* ... */ }


        [TearDown]
        public void Cleanup()
        { 
            /* ... */
        }


        //ParallelizableAttribute用于指示测试和/或其后代可以与其他测试并行运行。默认情况下，不进行并行执行。

        //OrderAttribute可以放置在测试方法或夹具上，以指定测试在夹具或包含测试的其他套件中运行的顺序。排序由属性的必需顺序参数int给出。
        //如前所述，订购是包含订购测试的测试的本地订购。对于测试用例（方法），订购适用于包含的夹具。对于夹具，它在包含的命名空间内应用。NUnit中没有全局订购测试的设施。
        //具有OrderAttribute参数的测试在没有该属性的任何测试之前启动。
        //有序测试按顺序参数的升序开始。
        //在具有相同顺序值或没有属性的测试中，执行顺序是不确定的。
        //测试不会等到之前的测试完成。如果正在使用多个线程，则可以在一些早期测试仍在运行时启动测试。


       


        /// <summary>
        /// Assert that Moneybags negate(positive to negative values) correctly
        /// </summary>
        /// 
        [Test]
        public void BagNegate()
        {
            // {[12 CHF][7 USD]} negate == {[-12 CHF][-7 USD]}
            Money[] bag = { new Money(-12, "CHF"), new Money(-7, "USD") };
            var expected = new MoneyBag(bag);
            Assert.That(fMB1.Negate(), Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that adding currency to Moneybags happens correctly
        /// </summary>
        /// 
        [Test]
        public void BagSimpleAdd()
        {
            // {[12 CHF][7 USD]} + [14 CHF] == {[26 CHF][7 USD]}
            Money[] bag = { new Money(26, "CHF"), new Money(7, "USD") };
            var expected = new MoneyBag(bag);
            Assert.That(fMB1.Add(f14CHF), Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that subtracting currency to Moneybags happens correctly
        /// </summary>
        /// 
        [Test]
        public void BagSubtract()
        {
            // {[12 CHF][7 USD]} - {[14 CHF][21 USD] == {[-2 CHF][-14 USD]}
            Money[] bag = { new Money(-2, "CHF"), new Money(-14, "USD") };
            var expected = new MoneyBag(bag);
            Assert.That(fMB1.Subtract(fMB2), Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that adding multiple currencies to Moneybags in one statement happens correctly
        /// </summary>
        /// 
        [Test]
        public void BagSumAdd()
        {
            // {[12 CHF][7 USD]} + {[14 CHF][21 USD]} == {[26 CHF][28 USD]}
            Money[] bag = { new Money(26, "CHF"), new Money(28, "USD") };
            var expected = new MoneyBag(bag);
            Assert.That(fMB1.Add(fMB2), Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that Moneybags hold zero value after adding zero value
        /// </summary>
        /// 
        [Test]
        public void IsZero()
        {
            ClassicAssert.IsTrue(fMB1.Subtract(fMB1).IsZero);

            Money[] bag = { new Money(0, "CHF"), new Money(0, "USD") };
            ClassicAssert.IsTrue(new MoneyBag(bag).IsZero);
        }

        /// <summary>
        /// Assert that a new bag is the same as adding value to an existing bag
        /// </summary>
        /// 
        [Test]
        public void MixedSimpleAdd()
        {
            // [12 CHF] + [7 USD] == {[12 CHF][7 USD]}
            Money[] bag = { f12CHF, f7USD };
            var expected = new MoneyBag(bag);
            Assert.That(f12CHF.Add(f7USD), Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that MoneyBag.Equals() works correctly
        /// </summary>
        /// 
        [Test]
        public void MoneyBagEquals()
        {
            //NOTE: Normally we use Assert.AreEqual to test whether two
            // objects are equal. But here we are testing the MoneyBag.Equals()
            // method itself, so using AreEqual would not serve the purpose.
            ClassicAssert.IsFalse(fMB1.Equals(null));

            ClassicAssert.IsTrue(fMB1.Equals(fMB1));
            var equal = new MoneyBag(new Money(12, "CHF"), new Money(7, "USD"));
            ClassicAssert.IsTrue(fMB1.Equals(equal));
            ClassicAssert.IsTrue(!fMB1.Equals(f12CHF));
            ClassicAssert.IsTrue(!f12CHF.Equals(fMB1));
            ClassicAssert.IsTrue(!fMB1.Equals(fMB2));
        }

        /// <summary>
        /// Assert that the hash of a new bag is the same as 
        /// the hash of an existing bag with added value
        /// </summary>
        /// 
        [Test]
        public void MoneyBagHash()
        {
            var equal = new MoneyBag(new Money(12, "CHF"), new Money(7, "USD"));
            Assert.That(equal.GetHashCode(), Is.EqualTo(fMB1.GetHashCode()));
        }

        /// <summary>
        /// Assert that Money.Equals() works correctly
        /// </summary>
        /// 
        [Test]
        public void MoneyEquals()
        {
            //NOTE: Normally we use Assert.AreEqual to test whether two
            // objects are equal. But here we are testing the MoneyBag.Equals()
            // method itself, so using AreEqual would not serve the purpose.
            ClassicAssert.IsFalse(f12CHF.Equals(null));
            var equalMoney = new Money(12, "CHF");
            ClassicAssert.IsTrue(f12CHF.Equals(f12CHF));
            ClassicAssert.IsTrue(f12CHF.Equals(equalMoney));
            ClassicAssert.IsFalse(f12CHF.Equals(f14CHF));
        }

        /// <summary>
        /// Assert that the hash of new Money is the same as 
        /// the hash of initialized Money
        /// </summary>
        /// 
        [Test]
        public void MoneyHash()
        {
            ClassicAssert.IsFalse(f12CHF.Equals(null));
            var equal = new Money(12, "CHF");
            Assert.That(equal.GetHashCode(), Is.EqualTo(f12CHF.GetHashCode()));
        }

        /// <summary>
        /// Assert that adding multiple small values is the same as adding one big value
        /// </summary>
        /// 
        [Test]
        public void Normalize()
        {
            Money[] bag = { new Money(26, "CHF"), new Money(28, "CHF"), new Money(6, "CHF") };
            var moneyBag = new MoneyBag(bag);
            Money[] expected = { new Money(60, "CHF") };
            // note: expected is still a MoneyBag
            var expectedBag = new MoneyBag(expected);
            Assert.That(moneyBag, Is.EqualTo(expectedBag));
        }

        /// <summary>
        /// Assert that removing a value is the same as not having such a value
        /// </summary>
        /// 
        [Test]
        public void Normalize2()
        {
            // {[12 CHF][7 USD]} - [12 CHF] == [7 USD]
            var expected = new Money(7, "USD");
            Assert.That(fMB1.Subtract(f12CHF), Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that removing multiple values works correctly
        /// </summary>
        /// 
        [Test]
        public void Normalize3()
        {
            // {[12 CHF][7 USD]} - {[12 CHF][3 USD]} == [4 USD]
            Money[] s1 = { new Money(12, "CHF"), new Money(3, "USD") };
            var ms1 = new MoneyBag(s1);
            var expected = new Money(4, "USD");
            Assert.That(fMB1.Subtract(ms1), Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that if value is subtracted from 0, the result will be negative.
        /// </summary>
        /// 
        [Test]
        public void Normalize4()
        {
            // [12 CHF] - {[12 CHF][3 USD]} == [-3 USD]
            Money[] s1 = { new Money(12, "CHF"), new Money(3, "USD") };
            var ms1 = new MoneyBag(s1);
            var expected = new Money(-3, "USD");
            Assert.That(f12CHF.Subtract(ms1), Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that Money.ToString() function works correctly
        /// </summary>
        /// 
        [Test]
        public void Print()
        {
            Assert.That(f12CHF.ToString(), Is.EqualTo("[12 CHF]"));
        }

        /// <summary>
        /// Assert that adding more value to Money happens correctly
        /// </summary>
        /// 
        [Test]
        public void SimpleAdd()
        {
            // [12 CHF] + [14 CHF] == [26 CHF]
            var expected = new Money(26, "CHF");
            Assert.That(f12CHF.Add(f14CHF), Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that adding multiple currencies to Moneybags happens correctly
        /// </summary>
        /// 
        [Test]
        public void SimpleBagAdd()
        {
            // [14 CHF] + {[12 CHF][7 USD]} == {[26 CHF][7 USD]}
            Money[] bag = { new Money(26, "CHF"), new Money(7, "USD") };
            var expected = new MoneyBag(bag);
            Assert.That(f14CHF.Add(fMB1), Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that multiplying currency in Money happens correctly
        /// </summary>
        /// 
        [Test]
        public void SimpleMultiply()
        {
            // [14 CHF] *2 == [28 CHF]
            var expected = new Money(28, "CHF");
            Assert.That(f14CHF.Multiply(2), Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that negating(positive to negative values) currency in Money happens correctly
        /// </summary>
        /// 
        [Test]
        public void SimpleNegate()
        {
            // [14 CHF] negate == [-14 CHF]
            var expected = new Money(-14, "CHF");
            Assert.That(f14CHF.Negate(), Is.EqualTo(expected));
        }

        /// <summary>
        /// Assert that removing currency from Money happens correctly
        /// </summary>
        /// 
        [Test]
        public void SimpleSubtract()
        {
            // [14 CHF] - [12 CHF] == [2 CHF]
            var expected = new Money(2, "CHF");
            Assert.That(f14CHF.Subtract(f12CHF), Is.EqualTo(expected));
        }
    }
}
