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
    /// 这是标记一个类的属性，该类包含测试，以及可选的安装或拆卸方法。
    /// 
    /// 
    /// 现在，对用作测试夹具的类的大多数限制都已消除。测试夹具类别：
    /// 可以是公共的、受保护的、私人的或内部的。
    /// 可能是一个静态类。
    /// 可以是泛型的，只要提供了任何类型参数或可以从实际参数中推断出来。
    /// 可能不是抽象的——尽管该属性可能应用于旨在作为测试夹具基类的抽象类。
    /// 如果TestFixtureAttribute没有提供参数，则类必须具有默认构造函数。
    /// 如果提供了参数，则它们必须与其中一个构造函数匹配。
    /// 
    /// 如果违反了这些限制中的任何一个，该类将无法作为测试运行，并将显示为错误。
    /// 建议构造函数不要有任何副作用，因为NUnit可能会在会话过程中多次构造对象。
    /// 从NUnit 2.5开始，TestFixture属性对于非参数化、非通用的夹具是可选的。
    /// 只要类包含至少一个标记有Test、TestCase或TestCaseSource属性的方法，它就会被视为测试夹具。
    /// 
    /// 
    /// 
    /// 
    /// TestFixtureAttribute可以应用于基类，并由任何派生类继承。这包括任何抽象基类，
    /// 因此如果需要，可以实现众所周知的抽象夹具模式。
    /// 为了便于使用泛型和/或参数化类，其中派生类可能需要与基类不同数量的参数（或类型参数），
    /// 使用以下规则忽略多余的TestFixture属性：
    /// 
    /// 1.如果所有TestFixture属性都提供构造函数或类型参数，则使用所有这些参数。
    /// 2.某些属性提供参数，而其他属性不提供，则只使用有参数的属性，忽略没有参数的属性。
    /// 3.如果所有属性都不提供参数，则选择其中一个供NUnit使用。无法预测将使用哪种，因此通常应避免这种情况。
    /// 
    /// </summary>
    [TestFixture]
    internal class TestFixtureDemo
    {

    }

    /// <summary>
    /// 继承
    /// </summary>

    [TestFixture]
    public class AbstractFixtureBase
    {
        /* ... */
    }

    [TestFixture(typeof(string))]
    public class DerivedFixture<T> : AbstractFixtureBase
    {
        /* ... */
    }


    /// <summary>
    /// 参数化测试夹具
    /// </summary>

    [TestFixture("hello", "hello", "goodbye")]
    [TestFixture("zip", "zip")]
    [TestFixture(42, 42, 99)]
    [TestFixture('a', 'a', 'b')]
    [TestFixture('A', 'A')]
    public class ParameterizedTestFixture
    {
        private readonly string _eq1;
        private readonly string _eq2;
        private readonly string? _neq;

        public ParameterizedTestFixture(string eq1, string eq2, string neq)
        {
            _eq1 = eq1;
            _eq2 = eq2;
            _neq = neq;
        }

        public ParameterizedTestFixture(string eq1, string eq2)
            : this(eq1, eq2, null) { }

        public ParameterizedTestFixture(int eq1, int eq2, int neq)
        {
            _eq1 = eq1.ToString();
            _eq2 = eq2.ToString();
            _neq = neq.ToString();
        }

        // Can use params arguments (but not yet optional arguments)
        public ParameterizedTestFixture(params char[] eqArguments)
        {
            _eq1 = eqArguments[0].ToString();
            _eq2 = eqArguments[1].ToString();
            if (eqArguments.Length > 2)
                _neq = eqArguments[2].ToString();
            else
                _neq = null;
        }

        [Test]
        public void TestEquality()
        {
            Assert.That(_eq2, Is.EqualTo(_eq1));
            Assert.That(_eq2.GetHashCode(), Is.EqualTo(_eq1.GetHashCode()));
        }

        [Test]
        public void TestInequality()
        {
            Assert.That(_neq, Is.Not.EqualTo(_eq1));
            if (_neq != null)
            {
                Assert.That(_neq.GetHashCode(), Is.Not.EqualTo(_eq1.GetHashCode()));
            }
        }
    }




    /// <summary>
    /// 通用测试夹具
    /// 
    /// 
    /// 必须指定用作TestFixtureAttribute参数的类型，或者使用命名参数TypeArgs=指定它们。
    /// </summary>
    /// <typeparam name="TList"></typeparam>
    [TestFixture(typeof(ArrayList))]
    [TestFixture(typeof(List<int>))]
    public class GenericListTests<TList> where TList : IList, new()
    {
        private IList _list = null!;

        [SetUp]
        public void CreateList()
        {
            _list = new TList();
        }

        [Test]
        public void CanAddToList()
        {
            _list.Add(1); 
            _list.Add(2); 
            _list.Add(3);
            Assert.That(_list, Has.Count.EqualTo(3));
        }
    }



    //带参数的通用测试夹具

    [TestFixture(typeof(double), typeof(int), 100.0, 42)]
    [TestFixture(typeof(int), typeof(double), 42, 100.0)]
    public class SpecifyBothSetsOfArgs<T1, T2> where T1 : notnull  where T2 : notnull
    {
        private readonly T1 _t1;
        private readonly T2 _t2;

        public SpecifyBothSetsOfArgs(T1 t1, T2 t2)
        {
            _t1 = t1;
            _t2 = t2;
        }

        //[TestCase(5, 7)]
        //public void TestMyArgTypes(T1 t1, T2 t2)
        //{
        //    Assert.That(t1, Is.TypeOf<T1>());
        //    Assert.That(t1, Is.LessThan(_t1));

        //    Assert.That(t2, Is.TypeOf<T2>());
        //    Assert.That(t2, Is.LessThan(_t2));
        //}
    }


    //如果泛型夹具使用构造函数参数，则有三种方法可以告诉NUnit哪些参数是类型参数，哪些是普通构造函数参数。
    //将这两组参数指定为TestFixtureAttribute的参数。领导体制。类型参数用作类型参数，而其余参数用于构造实例。
    //在下面的例子中，这导致了一些明显的重复。。。
    //将普通参数指定为TestFixtureAttribute的参数，并使用命名参数TypeArgs=指定类型参数。
    //同样，对于这个例子，类型信息是重复的，但它至少与正常参数更清晰地分开了。。。

    [TestFixture(100.0, 42, TypeArgs = new[] { typeof(double), typeof(int) })]
    [TestFixture(42, 100.0, TypeArgs = new[] { typeof(int), typeof(double) })]
    public class SpecifyTypeArgsSeparately<T1, T2> where T1 : notnull
    where T2 : notnull
    {
        private readonly T1 _t1;
        private readonly T2 _t2;

        public SpecifyTypeArgsSeparately(T1 t1, T2 t2)
        {
            _t1 = t1;
            _t2 = t2;
        }

        //[TestCase(5, 7)]
        //public void TestMyArgTypes(T1 t1, T2 t2)
        //{
        //    Assert.That(t1, Is.TypeOf<T1>());
        //    Assert.That(t1, Is.LessThan(_t1));

        //    Assert.That(t2, Is.TypeOf<T2>());
        //    Assert.That(t2, Is.LessThan(_t2));
        //}
    }

    //在某些情况下，当构造函数使用所有类型参数时，NUnit可能只是能够从提供的参数中推断出它们。这里就是这种情况，
    //以下是编写此示例的首选方式。。。

    [TestFixture(100.0, 42)]
    [TestFixture(42, 100.0)]
    public class DeduceTypeArgsFromArgs<T1, T2>
    where T1 : notnull
    where T2 : notnull
    {
        private readonly T1 _t1;
        private readonly T2 _t2;

        public DeduceTypeArgsFromArgs(T1 t1, T2 t2)
        {
            _t1 = t1;
            _t2 = t2;
        }

        //[TestCase(5, 7)]
        //public void TestMyArgTypes(T1 t1, T2 t2)
        //{
        //    Assert.That(t1, Is.TypeOf<T1>());
        //    Assert.That(t1, Is.LessThan(_t1));

        //    Assert.That(t2, Is.TypeOf<T2>());
        //    Assert.That(t2, Is.LessThan(_t2));
        //}
    }


}
