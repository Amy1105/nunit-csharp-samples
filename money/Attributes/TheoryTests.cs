using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money.Attributes
{
    /// <summary>
    /// 
    /// Theory是一种特殊类型的测试，用于验证关于正在开发的系统的一般陈述。常规测试是基于示例的。
    /// 也就是说，开发人员在测试代码中提供一个或多个输入和预期输出的示例，或者在参数化测试的情况下，作为测试方法的参数。
    /// 另一方面，一个理论做出了一个一般性的陈述，即它的所有断言都将适用于满足某些假设的所有论点。
    /// 
    /// Datapoint属性用于为Theories提供数据，对于普通测试（包括带参数的测试）则忽略该属性。
    /// 
    /// DatapointSource属性用于为Theories提供数据，对于普通测试（包括带参数的测试）将忽略该属性。
    /// 
    /// </summary>
    internal class TheoryTests
    {

    }
}
