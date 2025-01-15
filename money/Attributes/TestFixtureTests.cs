using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money.Attributes
{
    /// <summary>
    /// TestFixture这是标记一个类的属性，该类包含测试，以及可选的安装或拆卸方法。
    /// 现在，对用作测试夹具的类的大多数限制都已消除。测试夹具类别：可以是公共的、受保护的、私人的或内部的。
    /// 可能是一个静态类。可以是泛型的，只要提供了任何类型参数或可以从实际参数中推断出来。
    /// 可能不是抽象的——尽管该属性可能应用于旨在作为测试夹具基类的抽象类。
    /// 如果TestFixtureAttribute没有提供参数，则类必须具有默认构造函数。如果提供了参数，则它们必须与其中一个构造函数匹配。
    /// 
    /// 如果违反了这些限制中的任何一个，该类将无法作为测试运行，并将显示为错误。
    /// 建议构造函数不要有任何副作用，因为NUnit可能会在会话过程中多次构造对象。
    /// 从NUnit 2.5开始，TestFixture属性对于非参数化、非通用的夹具是可选的。
    /// 只要类包含至少一个标记有Test、TestCase或TestCaseSource属性的方法，它就会被视为测试夹具。
    /// 
    /// SetUpFixture
    /// 这是标记一个类的属性，该类包含程序集中给定命名空间（包括下面的嵌套命名空间）中所有测试夹具的一次性设置或拆卸方法。
    /// 该类最多只能包含一个标记有OneTimeSetUpAttribute的方法和一个标记为OneTimeTearDownAttribute的方法。
    /// 
    /// 用作设置夹具的类有一些限制。它必须是公开导出的类型，否则NUnit将看不到它。它必须有一个默认构造函数，否则NUnit将无法构造它。
    /// SetUpFixture中的OneTimeSetUp方法在其命名空间中包含的任何夹具之前执行一次。OneTimeTearDown方法在所有夹具执行完毕后执行一次。
    /// 在下面的示例中，在NUnit中的任何测试或设置方法之前调用RunBeforeAnyTests（）方法。测试命名空间。
    /// RunAfterAnyTests（）方法在命名空间中的所有测试及其单独或夹具拆卸完成执行后调用。
    /// 可以在给定的命名空间中创建多个SetUpFixtures。此类固定装置的执行顺序是不确定的。
    /// 
    /// SetUpFixture的范围仅限于一个组件。命名空间中的SetUpFixture将应用于该命名空间中的所有测试以及程序集中包含的所有命名空间。
    /// 任何命名空间之外的SetUpFixture都为整个程序集提供设置和拆除。
    /// 
    /// </summary>
    internal class TestFixtureTests
    {
    }



    [SetUpFixture]
    public class MySetUpClass
    {
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            // ...
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            // ...
        }
    }
}
