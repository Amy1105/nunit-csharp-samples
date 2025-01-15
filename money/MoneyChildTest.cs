using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Threading.Tasks;

namespace Money
{
    /// <summary>
	/// Tests Money
	/// </summary>
	/// 
	[TestFixture]
    public class MoneyChildTes:MoneyTest
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
            //f12CHF = new Money(12, "CHF");
            f14CHF = new Money(14, "CHF");
            //f7USD = new Money(7, "USD");
            f21USD = new Money(21, "USD");

            //fMB1 = new MoneyBag(f12CHF, f7USD);
            fMB2 = new MoneyBag(f14CHF, f21USD);            
        }

        /// <summary>
        ///  Test属性:是将TestFixture类中的方法标记为测试的一种方式。
        ///  它通常用于简单（非参数化）测试，但也可以应用于参数化测试，而不会生成任何额外的测试用例。
        ///   
        ///  测试方法可以是实例或静态方法。
        /// 
        /// 目标测试方法。Net 4.0或更高版本可能被标记为异步，NUnit将等待方法完成，然后记录结果并继续进行下一个测试。
        /// 如果没有返回值，异步测试方法必须返回Task，如果返回T类型的值，则必须返回Task<T>
        /// 
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


        //如果测试方法返回值，则必须将ExpectedResult命名参数传递给test属性。将检查此预期返回值是否与测试方法的返回值相等。

        //// Async test with an expected result
        //[Test(ExpectedResult = 4)]
        //public async Task<int> TestAdd()
        //{
        //    await  ...
        //return 2 + 2;
        //}


    }
}
