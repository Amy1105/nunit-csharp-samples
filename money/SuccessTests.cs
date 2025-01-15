using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money
{

    /// <summary>
    /// TestFixture和SetUpFixture在NUnit中的主要区别在于它们的作用范围和执行时机。
    /// 
    /// ‌TestFixture‌用于标记一个类包含测试代码，表明该类是用来进行测试的。它通常放在类定义之前
    /// TestFixture的作用范围是整个测试类，它告诉NUnit这个类包含测试代码，NUnit会在运行时识别并执行该类中的所有测试方法
    /// 
    /// 
    /// 
    /// ‌SetUpFixture‌用于设置和清理测试环境，确保每个测试方法在执行前后都能有一个干净的环境。它有两个具体的属性：‌SetUp‌和‌TearDown‌。
    /// ‌SetUp‌：在每个测试方法执行之前调用，用于初始化测试环境。
    /// ‌TearDown‌：在每个测试方法执行之后调用，用于清理测试环境
    /// 
    /// </summary>
    [TestFixture]
    public class SuccessTests
    {
        // A simple test
        [Test]
        public void Add()
        { /* ... */ }

        // A test with a description property
        [Test(Description = "My really cool test")]
        public void Add2()
        { /* ... */ }

        // Alternate way to specify description as a separate attribute
        [Test, Description("My really really cool test")]
        public void Add3()
        { /* ... */ }

        // A simple async test
        [Test]
        public async Task AddAsync()
        { /* ... */ }

        /// <summary>
        /// 如果测试方法返回值，则必须将ExpectedResult命名参数传递给test属性。
        /// 将检查此预期返回值是否与测试方法的返回值相等。
        /// </summary>
        /// <returns></returns>
        [Test(ExpectedResult =4)]
        public int TestAdd()
        {
            return 2 + 2;
        }


        //[Test(ExpectedResult = 4)]
        //public async Task<int> TestAdd4()
        //{
        //    await ...
        //return 2 + 2;
        //}



        /// <summary>
        /// TestCaseAttribute 具有双重用途，一方面将带有参数的方法标记为测试方法，
        /// 另一方面提供调用该方法时使用的内联数据
        /// 
        /// TestCaseAttribute可能在测试方法上出现一次或多次，该方法还可能携带其他提供测试数据的属性。
        /// 该方法也可以选择性地标记为测试属性。
        /// 
        /// 
        /// 单个测试用例按照NUnit发现它们的顺序执行。此顺序不一定遵循属性的词法顺序，
        /// 并且在不同编译器或CLR的不同版本之间通常会有所不同。
        /// </summary>
        /// <param name="n"></param>
        /// <param name="d"></param>
        /// <param name="q"></param>
        [TestCase(12, 3, 4)]
        [TestCase(12, 2, 6)]
        [TestCase(12, 4, 3)]
        public void DivideTest(int n, int d, int q)
        {
            Assert.That(n / d, Is.EqualTo(q));
        }

        /// <summary>
        /// 通过使用ExpectedResult参数，简化方法
        /// </summary>
        /// <param name="n"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        [TestCase(12, 3, ExpectedResult = 4)]
        [TestCase(12, 2, ExpectedResult = 6)]
        [TestCase(12, 4, ExpectedResult = 3)]
        public int DivideTest(int n, int d)
        {
            return n / d;
        }


        /// <summary>
        /// TestCaseSource 用于参数化测试方法，以标识提供所需参数的源。该属性还将该方法标识为测试方法。
        /// 数据与测试本身分开保存，可用于多种测试方法
        /// 
        /// Form 1 - [TestCaseSource(string sourceName)]
        /// 
        /// sourceName可能是测试类中的字段、属性或方法.
        /// 它必须是静态的。这是对NUnit 2.x的更改。
        /// 必须返回一个IEnumerable或实现IEnumerable的类型。对于字段，通常使用数组。对于属性和方法，您可以返回一个数组或实现自己的迭代器。
        /// 方法还可以返回IAsyncEnumerable或实现IAsyncEnumrable的类型。（NUnit 4+）
        /// 通过将返回类型包装在Task<T> 中，方法可以是异步的。（单位3.14+）
        /// 
        /// 
        /// 
        /// Form 2 - [TestCaseSource(Type sourceType, string sourceName)]
        ///  第一个参数是一个Type，表示将提供测试用例的类
        ///  第二个参数是一个字符串，表示用于提供测试用例的源的名称
        /// 
        /// 它可能是测试类中的字段、属性或方法。
        /// 它必须是静态的。这是对NUnit 2.x的更改。
        /// 必须返回一个IEnumerable或实现IEnumerable的类型。对于字段，通常使用数组。对于属性和方法，您可以返回一个数组或实现自己的迭代器。
        /// 方法还可以返回IAsyncEnumerable或实现IAsyncEnumrable的类型。（NUnit 4+）
        /// 通过将返回类型包装在Task<T> 中，方法可以是异步的。（单位3.14+）
        /// 
        /// 
        /// Form 3 - [TestCaseSource(Type sourceType)]
        /// 
        /// 
        /// 
        /// 
        /// 
        /// </summary>      
        [TestCaseSource(nameof(DivideCases))]  
        public void DivideTest2(int n, int d, int q)
        {
            ClassicAssert.AreEqual(q, n / d);
        }

        public static object[] DivideCases =
        {
            new object[] { 12, 3, 4 },
            new object[] { 12, 2, 6 },
            new object[] { 12, 4, 3 }
        };

        /// <summary>
        /// 如果源是一个方法，则可以将参数传递给源
        /// </summary>
        /// <param name="name"></param>
        [TestCaseSource(nameof(TestStrings), new object[] { true })]
        public void LongNameWithEvenNumberOfCharacters(string name)
        {
            Assert.That(name.Length, Is.GreaterThan(5));

            bool hasEvenNumOfCharacters = (name.Length % 2) == 0;
            Assert.That(hasEvenNumOfCharacters, Is.True);
        }

        [TestCaseSource(nameof(TestStrings), new object[] { false })]
        public void ShortNameWithEvenNumberOfCharacters(string name)
        {
            Assert.That(name.Length, Is.LessThan(15));

            bool hasEvenNumOfCharacters = (name.Length % 2) == 0;
            Assert.That(hasEvenNumOfCharacters, Is.True);
        }

        static IEnumerable<string> TestStrings(bool generateLongTestCase)
        {
            if (generateLongTestCase)
            {
                yield return "ThisIsAVeryLongNameThisIsAVeryLongName";
                yield return "SomeName";
                yield return "YetAnotherName";
            }
            else
            {
                yield return "AA";
                yield return "BB";
                yield return "CC";
            }
        }

        /// <summary>
        /// Form 2 - [TestCaseSource(Type sourceType, string sourceName)]
        /// </summary>
        public class TestFixtureThatUsesClassMethodAsTestCaseSource
        {
            [TestCaseSource(typeof(AnotherClassWithTestFixtures), nameof(AnotherClassWithTestFixtures.DivideCases))]
            public void DivideTest(int n, int d, int q)
            {
                ClassicAssert.AreEqual(q, n / d);
            }
        }

        public class AnotherClassWithTestFixtures
        {
            public static object[] DivideCases =
            {
                new object[] { 12, 3, 4 },
                new object[] { 12, 2, 6 },
                new object[] { 12, 4, 3 }
            };
        }


        /// <summary>
        /// Form 3 - [TestCaseSource(Type sourceType)]
        /// 
        /// 此表单中的Type参数表示提供测试用例的类。它必须具有默认构造函数并实现IEnumerable。
        /// 枚举器应返回与属性所在测试的签名兼容的测试用例数据。
        /// 
        /// </summary>     
        public class TestFixtureThatUsesClassAsTestCaseSource
        {
            [TestCaseSource(typeof(DivideCasesClass))]
            public void DivideTest(int n, int d, int q)
            {
                ClassicAssert.AreEqual(q, n / d);
            }
        }

        public class DivideCasesClass : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new object[] { 12, 3, 4 };
                yield return new object[] { 12, 2, 6 };
                yield return new object[] { 12, 4, 3 };
            }
        }

        //使用TestCaseData获得预期结果的来源


        [TestFixture]
        public class MyTests
        {
            [TestCaseSource(typeof(MyDataClass), nameof(MyDataClass.TestCases))]
            public int DivideTest(int n, int d)
            {
                return n / d;
            }
        }

        public class MyDataClass
        {
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(12, 3).Returns(4);
                    yield return new TestCaseData(12, 2).Returns(6);
                    yield return new TestCaseData(12, 4).Returns(3);
                }
            }
        }


        /// <summary>
        /// 从NUnit 4.1开始，可以显式指定用于泛型方法的泛型类型。当任何测试用例参数与所需的泛型类型不同时，
        /// 这可能很有用。如果省略，NUnit将根据TestCaseSource传递的值推断泛型类型参数。
        /// 
        /// 
        /// </summary>
        [TestFixture]
        public class MyExplicitlyTypedTests
        {
            [TestCaseSource(nameof(ExplicitTypeArgsTestCases))]
            public void ExplicitTypeArgs<T>(T input)
            {
                Assert.That(typeof(T), Is.EqualTo(typeof(long)));
            }

            private static IEnumerable<TestCaseData> ExplicitTypeArgsTestCases()
            {
                yield return new TestCaseData(2) { TypeArgs = new[] { typeof(long) } };
                yield return new TestCaseData(2L) { TypeArgs = new[] { typeof(long) } };
            }
        }


        /// <summary>
        /// 使用带有类型化数据和预期结果的TestCaseSource的示例
        /// </summary>

        public class TypedValuesWithExpectedAsAnonymousTuple
        {
            [TestCaseSource(nameof(TestCases))]
            public void TestOfPersonAge((Person P, bool Expected) td)
            {
                var res = td.P.IsOldEnoughToBuyAlcohol();
                Assert.That(res, Is.EqualTo(td.Expected));
            }

            public static IEnumerable<(Person, bool)> TestCases()
            {
                yield return (new Person { Name = "John", Age = 10 }, false);
                yield return (new Person { Name = "Jane", Age = 30 }, true);
            }
        }

        public class Person
        {
            public string Name { get; set; } = "";
            public int Age { get; set; }

            public bool IsOldEnoughToBuyAlcohol()
            {
                return Age >= 18;
            }
        }


        /// <summary>
        /// 也可以对测试用例数据和预期结果使用通用包装器（或任何自定义包装器），如下例所示。
        /// </summary>
        public class TypedValuesWithExpectedInWrapperClass
        {
            [TestCaseSource(nameof(TestCases))]
            public void TestOfPersonAge(TestDataWrapper<Person, bool> td)
            {
                var res = td.Value?.IsOldEnoughToBuyAlcohol();
                Assert.That(res, Is.EqualTo(td.Expected));
            }

            public static IEnumerable<TestDataWrapper<Person, bool>> TestCases()
            {
                yield return new TestDataWrapper<Person, bool> { Value = new Person { Name = "John", Age = 10 }, Expected = false };
                yield return new TestDataWrapper<Person, bool> { Value = new Person { Name = "Jane", Age = 30 }, Expected = true };
            }
        }

        public class TestDataWrapper<T, TExp>
        {
            public T? Value { get; set; }
            public TExp? Expected { get; set; }
        }




        /// <summary>
        /// TestCaseSourceAttribute支持一个命名参数：类别用于为此源返回的每个测试用例分配一个或多个类别。
        /// 
        /// 在构建测试时，NUnit使用枚举器返回的每个项，如下所示：
        /// 如果它是从TestCaseParameters类派生的对象，则其属性用于提供测试用例。
        /// 如果测试只有一个参数，并且返回的值与该参数的类型匹配，则直接使用它。这可以消除程序员的一些额外打字，如本例所示：
        /// 
        /// </summary>
        private static int[] _evenNumbers = { 2, 4, 6, 8 };

        [Test, TestCaseSource(nameof(_evenNumbers))]
        public void TestMethod(int num)
        {
            //NUnit为此提供了TestCaseData类型。如果测试只有一个参数，并且返回的值与该参数的类型匹配，则直接使用它。
            //这可以消除程序员的一些额外打字
            TestCaseData testCaseData = new TestCaseData(); 
                 
            Assert.That(num % 2, Is.Zero);
        }


    }


    /// <summary>
    /// 可以有多个SetUpFixture，但调用顺序不固定
    /// </summary>
    [SetUpFixture]
    public class TestSetup
    {
        [OneTimeSetUp]
        public void Setup()
        {
            // 初始化代码
        }

        // Alternate way to specify description as a separate attribute
        [Test, Description("My really really cool test")]
        public void Add3()
        { /* ... */ }


        [OneTimeTearDown]
        public void TearDown()
        {
            // 清理代码
        }
    }

}
