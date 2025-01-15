using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money.Attributes
{
    /// <summary>
    /// 
    /// RangeAttribute用于指定为参数化测试方法的单个参数提供的值范围。
    /// 由于NUnit将为每个参数提供的数据组合到一组测试用例中，
    /// 因此如果为其中任何一个参数提供数据，则必须为所有参数提供数据。
    /// 
    /// PairwiseAttribute用于测试，以指定NUnit应以使用所有可能的值对的方式生成测试用例。
    /// 当涉及两个以上的特征（参数）时，这是一种众所周知的方法来对抗测试用例的组合爆炸。
    /// 
    /// CombinatorialAttribute用于测试，以指定NUnit应为测试参数提供的单个数据项的所有可能组合生成测试用例。
    /// 由于这是默认值，因此使用此属性是可选的。
    /// 
    /// </summary>
    internal class RangeTests
    {

        /// <summary>
        /// 在泛型方法上使用时，程序员必须确保所有可能的参数组合都是有效的。
        /// 当多个参数使用相同的泛型类型（例如：T）时，这可能是不可能的，属性可能会生成无效的测试用例。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="s"></param>
        [Test, Combinatorial]
        public void MyTest([Values(1, 2, 3)] int x, [Values("A", "B")] string s)
        {
            //...
        }
    }
}
