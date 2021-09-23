using NUnit.Framework;
using StrignComporation;

namespace StrignComporationTest
{
    public class StrignComporationTest
    {
        
        [Test]
        [TestCase("����� ��������� ����������", "��������� ���������� �����", 0)]
        [TestCase("��", "��", 1)]
        [TestCase("��������� ��", "�����������", 15)]
        [TestCase("����� ���", "����� �����", 7)]
        public void TestDStringExtension(string arg, string arg1, double res)
        {
            var d = arg.D(arg1);

            Assert.AreEqual(d, res);
        }
    }
}