using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money.Attributes
{
    /// <summary>
    ///  Explicit属性会导致跳过测试或测试夹具，除非明确选择其运行。
    ///  如果按名称选择测试或夹具，或者使用过滤器包含它，则将运行该测试或夹具。排除某些测试的非过滤器不会被视为显式选择，也不会导致显式测试运行。
    ///  所有其他过滤器都被认为可以显式选择它们匹配的测试。请参阅下面的示例。
    ///  可选的字符串参数可用于给出将测试标记为Explicit的原因。
    ///  在运行测试的过程中遇到具有Explicit属性的测试或夹具，则会跳过它，除非它是通过上述方式之一特别选择的。
    ///  该测试不会影响测试运行的整体结果。显式测试在gui中显示为跳过。
    ///  
    /// 
    /// 注意：虽然C#语法允许您在SetUpFixture类上放置Explicit属性，但该属性会被NUnit忽略，在当前版本中无效。
    /// 
    /// </summary>
    [TestFixture, Explicit]
    public class ExplicitTests
    {
        // ...
    }
}
