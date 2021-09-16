using F23.StringSimilarity;
using System;

namespace StrignComporation
{
    public static class Ext
    {
        public static double D(this string s, string s1)
        {
            var l = new Damerau();

            var sArr = s.Split(" ");
            var sArr1 = s1.Split(" ");

            double d = 0;
            double min;
            int minIndex = 0;
            double cur;

            foreach (var it in sArr)
            {
                min = double.MaxValue;
                for(int i = 0;i<sArr1.Length;i++)
                {
                    cur = l.Distance(it, sArr1[i]);

                    if (cur < min)
                    {
                        min = cur;
                        minIndex = i;
                    }
                }
                d += min;
            }


            return d;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var l = new QGram();

            int index = 0;

            Console.WriteLine("{0}: {1}", ++index, "Иванов Иван Иванович".D("Иван Иванов"));
            Console.WriteLine("{0}: {1}", ++index, "Петров Иван Иванович".D("Иван Иванов"));
            Console.WriteLine("{0}: {1}", ++index, "Иванов Алексей Иванович".D("Иван Иванов"));
            Console.WriteLine("{0}: {1}", ++index, "Сасов Александр".D("Александр Викторович Сасов"));
            Console.WriteLine("{0}: {1}", ++index, "Сасов Александр Викторович".D("Александр Викторович Сасов"));
            Console.WriteLine("{0}: {1}", ++index, "Сасов Виктор Васильевич".D("Александр Викторович Сасов"));
            Console.WriteLine("{0}: {1}", ++index, "Часов Александр Викторович".D("Александр Викторович Сасов"));
            Console.WriteLine("{0}: {1}", ++index, "Сасов Александр".D("Александр Викторович Костров"));
            Console.WriteLine("{0}: {1}", ++index, "Сасов Александр".D("Дмитрий Викторович Сасов"));
            Console.WriteLine("{0}: {1}", ++index, "Иванов Иван Иванович".D("Иванов Иван Иванович"));

            Console.ReadLine();
        }
    }
}
