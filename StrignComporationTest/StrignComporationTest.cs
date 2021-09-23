using NUnit.Framework;
using StrignComporation;

namespace StrignComporationTest
{
    public class StrignComporationTest
    {
        
        [Test]
        [TestCase("Сасов Александр Викторович", "Александр Викторович Сасов", 0)]
        [TestCase("ВВ", "ВБ", 1)]
        [TestCase("Секретать ЦК", "Секретариат", 15)]
        [TestCase("Целый год", "Целая часть", 7)]
        public void TestDStringExtension(string arg, string arg1, double res)
        {
            var d = arg.D(arg1);

            Assert.AreEqual(d, res);
        }
    }
}