using System;

namespace AdUserLib
{
    /// <summary>
    /// Translator to english
    /// </summary>
    public static class EnglishTranslate
    {
        /// <summary>
        /// English Translate for Art
        /// </summary>
        /// <param name="pchar"></param>
        /// <returns></returns>
        public static string EnglishTranslateArt(string pchar)
        {
            string resEng = "";
            switch (pchar)
            {
                case "а":
                    resEng = "a";
                    break;
                case "б":
                    resEng = "b";
                    break;
                case "в":
                    resEng = "v";
                    break;
                case "г":
                    resEng = "g";
                    break;
                case "д":
                    resEng = "d";
                    break;
                case "е":
                    resEng = "e";
                    break;
                case "ё":
                    resEng = "e";
                    break;
                case "ж":
                    resEng = "zh";
                    break;
                case "з":
                    resEng = "z";
                    break;
                case "и":
                    resEng = "i";
                    break;
                case "й":
                    resEng = "y";
                    break;
                case "к":
                    resEng = "k";
                    break;
                case "л":
                    resEng = "l";
                    break;
                case "м":
                    resEng = "m";
                    break;
                case "н":
                    resEng = "n";
                    break;
                case "о":
                    resEng = "o";
                    break;
                case "п":
                    resEng = "p";
                    break;
                case "р":
                    resEng = "r";
                    break;
                case "с":
                    resEng = "s";
                    break;
                case "т":
                    resEng = "t";
                    break;
                case "у":
                    resEng = "u";
                    break;
                case "ф":
                    resEng = "f";
                    break;
                case "х":
                    resEng = "kh";
                    break;
                case "ц":
                    resEng = "ts";
                    break;
                case "ч":
                    resEng = "ch";
                    break;
                case "ш":
                    resEng = "sh";
                    break;
                case "щ":
                    resEng = "sch";
                    break;
                case "ъ":
                    resEng = "";
                    break;
                case "ы":
                    resEng = "y";
                    break;
                case "ь":
                    resEng = "";
                    break;
                case "э":
                    resEng = "e";
                    break;
                case "ю":
                    resEng = "yu";
                    break;
                case "я":
                    resEng = "ya";
                    break;
                case " ":
                    resEng = " ";
                    break;
                case ".":
                    resEng = ".";
                    break;
                default:
                    resEng = pchar;
                    break;
            }
            return resEng;
        }

        /// <summary>
        /// Endings
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string Endings(string word)
        {
            string[] endings =    {"ой","ый","ий","ей","ия","ай","ая"};
            string[] engEndings = { "oy","y","y","ey","ia","ay","aya" };
            for (var i = 0; i < endings.Length; i++)
            {
                if (word.LastIndexOf(endings[i], StringComparison.Ordinal) == word.Length - endings[i].Length - 1)
                {
                    word = word.Replace(endings[i], engEndings[i]);
                }
            }
            return word;
        }

        /// <summary>
        /// суффиксы
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string Middles(string word)
        {
            string[] middles = { "ой","ай","эй","ей","уй","ий" };
            string[] engMiddles = { "oi","ai","ei","ei","ui","iy" };
            for (int i = 0;i<middles.Length;i++)
            {
                if (word.LastIndexOf(middles[i], StringComparison.Ordinal) <= word.Length-middles[i].Length-1 
                    && word.LastIndexOf(middles[i], StringComparison.Ordinal)>0)
                {
                    word = word.Replace(middles[i],engMiddles[i]);
                }
            }
            return word;
        }

        /// <summary>
        /// TranslateEnglish
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string TranslateEnglish(string word)
        {
            var engWord = "";
            word = word.ToLower();
            word = Endings(word);
            word = Middles(word);
            foreach (var item in word)
            {
                engWord += EnglishTranslateArt(item.ToString());
            }
            return engWord;
        }

        /// <summary>
        /// TranslateFio
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="name"></param>
        /// <param name="middleName"></param>
        /// <returns></returns>
        public static string TranslateFio(string lastName, string name, string middleName)
        {
            string engFio = $"{lastName} {name} {middleName}";
            engFio = TranslateEnglish(engFio);
            return engFio;
        }
    }
}
