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
        /// SetUp ���ԣ���������TestFixture�������ṩһ��ͨ�ú�������Щ�����ڵ���ÿ�����Է���֮ǰִ�С�
        ///       ���ԣ�������ඨ����һ��SetUp��������÷��������������е�ÿ�����Է���֮ǰ������
        ///       �����ڻ����ж���һ��SetUp���������������ж�����һ����NUnit�����������еķ���֮ǰ���û���SetUp������
        ///       
        /// �������SetUp�������������б���д����NUnit��������û���SetUp������
        /// NUnit��Ԥ�ڰ������ػ��������ڵ��÷�����ע�⣬ÿ�ַ��������в�ͬ�����ƣ�
        /// ֻҪ���߶���[SetUp]���ԣ�ÿ����������ȷ��˳�򱻵��á�
        /// 
        /// ע�⣺��Ȼ������ͬһ�����ж�����SetUp�������������Ӧ������������̳в�νṹ�е������ж���ķ�����ͬ��
        /// ���ǵ�ִ��˳��û�б�֤
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
        ///  Test����:�ǽ�TestFixture���еķ������Ϊ���Ե�һ�ַ�ʽ��
        ///  ��ͨ�����ڼ򵥣��ǲ����������ԣ���Ҳ����Ӧ���ڲ��������ԣ������������κζ���Ĳ���������
        ///   
        ///  ���Է���������ʵ����̬������
        /// 
        /// Ŀ����Է�����Net 4.0����߰汾���ܱ����Ϊ�첽��NUnit���ȴ�������ɣ�Ȼ���¼���������������һ�����ԡ�
        /// ���û�з���ֵ���첽���Է������뷵��Task���������T���͵�ֵ������뷵��Task<T>
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


        //������Է�������ֵ������뽫ExpectedResult�����������ݸ�test���ԡ�������Ԥ�ڷ���ֵ�Ƿ�����Է����ķ���ֵ��ȡ�

        //// Async test with an expected result
        //[Test(ExpectedResult = 4)]
        //public async Task<int> TestAdd()
        //{
        //    await  ...
        //return 2 + 2;
        //}


    }
}
