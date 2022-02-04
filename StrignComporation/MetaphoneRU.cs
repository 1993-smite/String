using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace StrignComporation
{
    public class MetaphoneRU
    {
        string alf => "ОЕАИУЭЮЯПСТРКЛМНБВГДЖЗЙФХЦЧШЩЫЁ";//алфавит кроме исключаемых букв
        string zvonkie => "БЗДВГ";//звонкие
        string gluhie => "ПСТФК";//глухие
        string soglasnie => "ПСТКБВГДЖЗФХЦЧШЩ";//согласные, перед которыми звонкие оглушаются
        string glasnie => "ОЮЕЭЯЁЫ";//образец гласных
        string ct => "АУИИАИА";// замена гласных

        Dictionary<string, string> suffixMap 
            = new Dictionary<string, string>
                            {
                                { "ОВСКИЙ", "@" },
                                { "ЕВСКИЙ", "#" },
                                { "ОВСКАЯ", "$" },
                                { "ЕВСКАЯ", "%" },
                                { "ИЕВА", "9" },
                                { "ЕЕВА", "9" },
                                { "ОВА", "9" },
                                { "ЕВА", "9" },
                                { "ИНА", "1" },
                                { "ИЕВ", "4" },
                                { "ЕЕВ", "4" },
                                { "НКО", "3" },
                                { "ОВ", "4" },
                                { "ЕВ", "4" },
                                { "АЯ", "6" },
                                { "ИЙ", "7" },
                                { "ЫЙ", "7" },
                                { "ЫХ", "5" },
                                { "ИХ", "5" },
                                { "ИН", "8" },
                                { "ИК", "2" },
                                { "ЕК", "2" },
                                { "УК", "0" },
                                { "ЮК", "0" }
                            };

        #region Helpers

        private string replaceLastChar(string s, char c)
        {
            return s.Substring(0, s.Length - 1) + c;
        }

        private char lastChar(string s)
        {
            return s[s.Length - 1];
        }

        #endregion

        private string MetaphoneRu(string w)
        {
            //в верхний регистр
            w = w.ToUpper();
            var sb = new StringBuilder(" ");
            //оставили только символы из alf
            for (int i = 0; i < w.Length; i++)
            {
                if (alf.Contains(w[i])) sb.Append(w[i]);
            }
            var s = sb.ToString();
            //компрессия окончаний
            foreach (var item in suffixMap)
            {
                if (!s.EndsWith(item.Key)) continue;
                s = Regex.Replace(s, item.Key + "$", item.Value);
            }

            //Оглушаем последний символ, если он - звонкий согласный
            var idx = zvonkie.IndexOf(lastChar(s));
            if (idx != -1) s = replaceLastChar(s,gluhie[idx]);
            var old_c = ' ';
            string V = "";
            for (int i = 0; i < s.Length; i++)
            {
                var c = s[i];
                var B = glasnie.IndexOf(c);
                if (B != -1)
                {//гласная
                    if (old_c == 'Й' || old_c == 'И')
                    {
                        if (c == 'О' || c == 'Е')//'Буквосочетания с гласной
                        {
                            old_c = 'И';
                            replaceLastChar(V, old_c);
                        }
                        else//Если не буквосочетания с гласной, а просто гласная
                        {
                            if (c != old_c) V = V + ct[B];
                        }
                    }
                    else//Если не буквосочетания с гласной, а просто гласная
                    {
                        if (c != old_c) V = V + ct[B];
                    }
                }
                else
                {//согласная
                    if (c != old_c)//для «Аввакумов»
                    {
                        if (soglasnie.Contains(c))
                        { // 'Оглушение согласных
                            B = zvonkie.IndexOf(old_c);
                            if (B != -1)
                            {
                                old_c = gluhie[B];
                                V = replaceLastChar(V,old_c);
                            }
                        }
                        if (c != old_c) V = V + c;//для «Шмидт»
                    }
                }
                old_c = c;
            }
            return V;

        }

        private string[] WordMetaphone(string word)
        {
            var words = word.Split(' ');
            var metaValue = new string[words.Length];

            for (int index = 0; index < words.Length; index++)
            {
                metaValue[index] = MetaphoneRu(words[index]);
            }

            return metaValue;
        }

        private string WordMetaphoneOne(string word)
        {
            return MetaphoneRu(word);
        }

        private bool MetaphoneEquals(string[] meta, string[] meta1)
        {
            var min = meta.Length < meta1.Length ? meta : meta1;
            var max = meta.Length >= meta1.Length ? meta : meta1;
            int equals = 0;
            var oldIndexes = new List<int>(max.Length);
            foreach (var mn in min)
            {
                for(int index=0;index < max.Length; index++)
                {
                    if (!oldIndexes.Contains(index) && mn.Equals(max[index]))
                    {
                        oldIndexes.Add(index);
                        equals++;
                        break;
                    }
                }

            }

            return equals == min.Length;
        }

        private bool MetaphoneEquals(string meta, string meta1)
        {
            return meta.Equals(meta1);
        }

        public bool IsSimilar(string[] words)
        {
            var result = true;
            var metaPhoneValue = WordMetaphoneOne(words[0]);
            
            for(int index = 1; index < words.Length; index++)
            {
                var metaPhoneVal = WordMetaphoneOne(words[index]);
                result = MetaphoneEquals(metaPhoneValue, metaPhoneVal);

                if (!result)
                    return false;
            }
            return true;
        }




    }
}
