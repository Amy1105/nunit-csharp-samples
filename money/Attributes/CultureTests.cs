using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money.Attributes
{
    /// <summary>
    /// Culture属性用于指定应运行测试或夹具的区域性。它不会影响区域性设置，而只是使用它来确定是否运行测试。
    /// 如果您希望在运行测试时更改区域性，请改用SetCulture属性。
    /// 如果未满足测试的指定区域性要求，则跳过该测试。在gui中，测试的树节点保持灰色，状态栏颜色不受影响。
    /// Culture属性的一个用途是提供不同文化下的替代测试。您可以指定特定的区域性，如“en-GB”，也可以指定中性的区域性如“de”。
    /// 
    /// 
    /// 
    /// 
    /// SetCulture属性用于在测试期间设置当前区域性。它可以在测试、夹具或组件级别指定。
    /// 区域性保持设置状态，直到测试或夹具完成，然后重置为其原始值。如果要使用当前区域性设置来决定是否运行测试，请使用culture属性而不是此属性。
    /// 只能指定一种文化。在多种文化下运行测试是未来计划的改进。此时，您可以通过将测试代码分解为由每个单独的测试方法调用的私有方法来实现相同的结果。
    /// 
    /// 
    /// SetUICulture属性用于在测试期间设置当前UI区域性。它可以在测试或夹具级别指定。
    /// UI区域性保持设置状态，直到测试或夹具完成，然后重置为其原始值。如果要使用当前区域性设置来决定是否运行测试，请使用culture属性而不是此属性。
    /// 只能指定一种文化。在多种文化下运行测试是未来计划的改进。此时，您可以通过将测试代码分解为由每个单独的测试方法调用的私有方法来实现相同的结果。
    /// 
    /// 
    /// 
    /// </summary>
    internal class CultureTests
    {

        [TestFixture]
        [Culture("fr-FR")]
        public class FrenchCultureTests
        {
            // ...
        }

        [Test]
        [Culture(Exclude = "en,de")]
        public void SomeTest()
        { 
        
        }



        [TestFixture]
        [SetCulture("fr-FR")]
        public class FrenchCultureTests2
        {
            // ...
        }

        [Test]
        [SetCulture("en")]
        public void SomeTest2()
        {

        }


        [TestFixture]
        [SetUICulture("fr-FR")]
        public class FrenchCultureTests3
        {
            // ...
        }

        [Test]
        [SetUICulture("en")]
        public void SomeTest3()
        {

        }
    }
}
