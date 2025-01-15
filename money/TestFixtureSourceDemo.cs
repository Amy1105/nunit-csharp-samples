using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money
{
    /// <summary>
    /// 
    /// Form 1 - [TestFixtureSource(string sourceName)]
    ///  此表单中的单属性参数是一个字符串，表示用于为构造TestFixture提供参数的源名称
    /// 它可能是测试类中的字段、属性或方法。
    /// 它必须是静态的。
    /// 它必须返回一个IEnumerable或实现IEnumerable的类型。对于字段，通常使用数组。对于属性和方法，您可以返回一个数组或实现自己的迭代器。
    /// 方法还可以返回IAsyncEnumerable或实现IAsyncEnumrable的类型。（NUnit 4+）
    /// 通过将返回类型包装在Task<T> 中，方法可以是异步的。（单位3.14+）
    /// 
    /// 
    /// </summary>
    internal class TestFixtureSourceDemo
    {

        [TestFixtureSource(nameof(FixtureArgs))]
        public class MyTestClass
        {
            public MyTestClass(string word, int num) 
            { 
                //...
                                                       }

            /* ... */

            static object[] FixtureArgs = {
                new object[] { "Question", 1 },
                new object[] { "Answer", 42 }
             };
        }


        /// <summary>
        /// Form 2 - [TestFixtureSource(Type sourceType, string sourceName)]
        /// 
        /// 此表单中属性的第一个参数是一个Type，表示将提供测试夹具数据的类。
        /// 第二个参数是一个字符串，表示用于提供测试夹具的源名称。它具有以下特点：
        /// 
        /// 它可能是测试类中的字段、属性或方法。它必须是静态的。
        /// 它必须返回一个IEnumerable或实现IEnumerable的类型。对于字段，通常使用数组。对于属性和方法，您可以返回一个数组或实现自己的迭代器。
        /// 方法还可以返回IAsyncEnumerable或实现IAsyncEnumrable的类型。（NUnit 4+）
        /// 过将返回类型包装在Task<T> 中，方法可以是异步的。（单位3.14+）
        /// 枚举器返回的单个项必须是对象数组或从TestFixtureParameters类派生。参数必须与夹具构造函数一致。
        /// 
        /// 
        /// </summary>
        [TestFixtureSource(typeof(AnotherClass), nameof(AnotherClass.FixtureArgs))]
        public class MyTestClass2
        {
            public MyTestClass2(string word, int num) 
            {
            }
        }

       public class AnotherClass
        {
          public  static object[] FixtureArgs = {
                new object[] { "Question", 1 },
                new object[] { "Answer", 42 }
            };
        }

        //Form 3 - [TestFixtureSource(Type sourceType)]


        [TestFixtureSource(typeof(FixtureArgs))]
        public class MyTestClass3
        {
            public MyTestClass3(string word, int num) { /* ... */ }

            /* ... */
        }

        class FixtureArgs : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new object[] { "Question", 1 };
                yield return new object[] { "Answer", 42 };
            }
        }




        //TestOf属性添加了有关正在测试的类的信息

        //Timeout  TimeoutAttribute用于指定测试用例的超时值（以毫秒为单位）。如果测试用例运行的时间超过指定的时间，
        //则会立即取消并报告为失败，并显示一条消息，指示已超过超时时间。

        //MaxTimeAttribute用于测试方法，以毫秒为单位指定测试用例的最大时间。如果测试用例的完成时间超过指定时间，则报告为失败。

        //任何断言失败都优先于已用时间检查。
        //如果超过时间，此属性不会取消测试。它只是等待测试完成，然后将经过的时间与指定的最大值进行比较。如果要取消长时间运行的测试，请参阅超时属性。


        //IgnoreAttribute用于指示由于某种原因不应执行测试。请注意，对于NUnit 3，必须指定原因。跑步者将忽略的测试显示为警告，以提醒他们需要纠正或以其他方式更改和恢复测试。
        // Until命名参数允许您在特定时间段内忽略测试，之后测试将正常运行。截止日期必须是一个可以解析为日期的字符串。


        //FixtureLifeCycleAttribute用于指示应为夹具或组件中的每个测试构建一个测试夹具实例或组件中所有测试夹具的实例。
        //此属性可以应用于测试夹具（类）或测试组件。它与可并行化属性结合使用非常有用，
        //这样就可以为测试夹具中的每个测试构建一个新的测试夹具实例。这允许测试在并行测试运行期间独立运行，
        //而无需共享实例字段和属性。这使得运行并行测试更容易，因为更容易使测试线程安全。
        //使用生命周期时。InstancePerTestCase、OneTimeSetUp和OneTimeTearDown方法必须是静态的，并且每个方法只调用一次。这是必需的，这样安装或拆卸方法就不会访问为每个测试重置的实例字段或属性。
        //使用生命周期时。InstancePerTestCase，类的构造函数将在执行每个测试之前被调用，IDisposable测试夹具将在测试完成后被处理。
        //每次测试前后都会调用SetUp和TearDown方法。Order属性受到尊重。


        //ValuesAttribute用于指定为参数化测试方法的单个参数提供的一组值。由于NUnit将为每个参数提供的数据组合到一组测试用例中，
        //因此如果为其中任何一个参数提供数据，则必须为所有参数提供数据。


        //SingleThreadedAttribute用于TestFixture，表示OneTimeSetUp、OneTimeTearDown和所有子测试必须在同一线程上运行。


        //RequiresThreadAttribute用于指示测试方法、类或程序集应在单独的线程上运行。可选地，可以在构造函数中指定线程所需的单元。


        //RepeatAttribute用于测试方法，以指定应多次执行该方法。如果任何重复失败，其余的将不运行，并报告失败。

        //RetryAttribute用于测试方法，以指定在失败时应重新运行该方法，最多可重复运行次数。


        //RangeAttribute用于指定为参数化测试方法的单个参数提供的值范围。由于NUnit将为每个参数提供的数据组合到一组测试用例中，
        //因此如果为其中任何一个参数提供数据，则必须为所有参数提供数据。


        //RandomAttribute用于指定为单个数值参数或参数化测试方法的Guid提供的一组随机值。
        //由于NUnit将为每个参数提供的数据组合到一组测试用例中，因此如果为其中任何一个参数提供数据，则必须为所有参数提供数据



        //NonParallelizable 此属性用于指示其出现的测试可能不会与任何其他测试并行运行。该属性不接受任何参数，可以在程序集、类或方法级别使用。
        //ParallelizableAttribute用于指示测试和/或其后代可以与其他测试并行运行。默认情况下，不进行并行执行。
        //当不带参数使用时，Parallelizable会使放置它的测试夹具或方法与其他可并行测试并行排队执行。x
        //它可以在程序集、类或方法级别使用。



        //Explicit属性会导致跳过测试或测试夹具，除非明确选择其运行。如果按名称选择测试或夹具，或者使用过滤器包含它，
        //则将运行该测试或夹具。排除某些测试的非过滤器不会被视为显式选择，也不会导致显式测试运行。
        //所有其他过滤器都被认为可以显式选择它们匹配的测试。请参阅下面的示例。

        public class OtherAttribute
        {
            [Test]
            public void MyTest([Values(1, 2, 3)] int x, [Values("A", "B")] string s)
            {
             /* MyTest(1, "A")
                MyTest(1, "B")
                MyTest(2, "A")
                MyTest(2, "B")
                MyTest(3, "A")
                MyTest(3, "B") */
            }

            //ValueSourceAttribute用于测试方法的各个参数，以标识要提供的参数值的命名源。该属性有两个公共构造函数。
            //ValueSourceAttribute(Type sourceType, string sourceName);
            //ValueSourceAttribute(string sourceName);


            //如果指定了sourceType，则它表示提供数据的类。
            //如果未指定sourceType，则使用包含测试方法的类。
            //sourceName表示将提供参数的源的名称。它应该具有以下特征：

            /*
             * 
                它可以是字段、非索引属性或不带参数的方法。
                它必须是静态成员。
                它必须返回一个IEnumerable或实现IEnumerable的类型。
                方法还可以返回IAsyncEnumerable或实现IAsyncEnumrable的类型。（NUnit 4+）
                通过将返回类型包装在Task<T>中，方法可以是异步的。（单位3.14+）
                从枚举器返回的单个项必须与属性所在的参数类型兼容。
             * 
             */

        }

    }
}
