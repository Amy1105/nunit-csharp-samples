using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money
{
    /// <summary>
    /// 这是标记一个类的属性，该类包含程序集中给定命名空间（包括下面的嵌套命名空间）中所有测试夹具的一次性设置或拆卸方法。
    /// 该类最多只能包含一个标记有OneTimeSetUpAttribute的方法和一个标记为OneTimeTearDownAttribute的方法。
    /// 
    /// 用作设置夹具的类有一些限制。
    /// 必须是公开导出的类型，否则NUnit将看不到它。
    /// 必须有一个默认构造函数，否则NUnit将无法构造它。
    /// 
    /// SetUpFixture中的OneTimeSetUp方法在其命名空间中包含的任何夹具之前执行一次。
    /// OneTimeTearDown方法在所有夹具执行完毕后执行一次。
    /// 
    /// 可以在给定的命名空间中创建多个SetUpFixtures。此类固定装置的执行顺序是不确定的。
    /// </summary>
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
