using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
    public static class SuperscriptSubscriptReplace
    {
        
        /// <summary>
        /// 替换\u**\n为unicode上标
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RepalceUp(this string str)
        {
            Dictionary<char, char> replaceDic = new Dictionary<char, char> {
                { '0', '⁰' },
                { '1', '¹' },
                { '2', '²' },
                { '3', '³' },
                { '4', '⁴' },
                { '5', '⁵' },
                { '6', '⁶' },
                { '7', '⁷' },
                { '8', '⁸' },
                { '9', '⁹' },
                { '+', '⁺' },
                { '-', '⁻' },
                { '=', '⁼' },
                { '(', '⁽' },
                { ')', '⁾' },
                {'a','ᵃ'},
                {'b','ᵇ'},
                {'c','ᶜ'},
                {'d','ᵈ'},
                {'e','ᵉ'},
                {'f','ᶠ'},
                {'g','ᵍ'},
                {'h','ʰ'},
                {'i','ⁱ'},
                {'j','ʲ'},
                {'k','ᵏ'},
                {'l','ˡ'},
                {'m','ᵐ'},
                {'n','ⁿ'},
                {'o','ᵒ'},
                {'p','ᵖ'},
                {'q','ʳ'},
                {'r','ʳ'},
                {'s','ˢ'},
                {'t','ᵗ'},
                {'u','ᵘ'},
                {'v','ᵛ'},
                {'w','ʷ'},
                {'x','ˣ'},
                {'y','ʸ'},
                {'z','ᶻ'},
            };
            //var reg = new Regex(@"\\[uU](.+?)\\n");
            //return Replace(replaceDic, str, reg);

            return ReplaceUp(replaceDic, str);
        }
        /// <summary>
        /// 替换\d**\n为unicode下标
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RepalceDown(this string str)
        {
            Dictionary<char, char> replaceDic = new Dictionary<char, char> {
                {'0', '₀' },
                {'1', '₁' },
                {'2', '₂' },
                {'3', '₃' },
                {'4', '₄' },
                {'5', '₅' },
                {'6', '₆' },
                {'7', '₇' },
                {'8', '₈' },
                {'9', '₉' },
                {'+', '₊' },
                {'-', '₋' },
                {'=', '₌' },
                {'(', '₍' },
                {')', '₎' },
            };
            //var reg = new Regex(@"\\[dD](.+?)\\n");
            //return Replace(replaceDic, str, reg);
            return ReplaceDown(replaceDic, str);
        }
        /// <summary>
        /// 使用正则替换，无法匹配到没有\n结尾的数据
        /// </summary>
        /// <param name="replaceDic"></param>
        /// <param name="str"></param>
        /// <param name="reg"></param>
        /// <returns></returns>
        private static string Replace(Dictionary<char, char> replaceDic, string str, Regex reg)
        {
            var newStr = str;
            var matchs = reg.Matches(str);
            foreach (Match match in matchs)
            {
                var replaceStr = "";
                foreach (var item in match.Groups[1].Value)
                {
                    replaceStr += replaceDic.ContainsKey(item) ? replaceDic[item] : item;
                }
                newStr = newStr.Replace(match.Groups[0].Value, replaceStr);
            }

            return newStr;
        }
        /// <summary>
        /// 循环替换
        /// </summary>
        /// <param name="replaceDic"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string ReplaceUp(Dictionary<char, char> replaceDic, string str)
        {
            return Replace(replaceDic, str, new List<char> { 'u', 'U' });
        }
        /// <summary>
        /// 循环替换
        /// </summary>
        /// <param name="replaceDic"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string ReplaceDown(Dictionary<char, char> replaceDic, string str)
        {
            return Replace(replaceDic, str, new List<char> { 'd', 'D' });
        }

        /// <summary>
        /// 循环替换
        /// </summary>
        /// <param name="replaceDic"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string Replace(Dictionary<char, char> replaceDic, string str, IEnumerable<char> specialCharacter)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            var strNew = "";
            var isReplace = false;
            for (int i = 0; i < str.Length; i++)
            {
                //检测到"\u","\U","\d","\D",表示替换开始
                if (str[i] == '\\' && i <= str.Length - 3
                    && (specialCharacter.Contains(str[i + 1])))
                {
                    isReplace = true;
                    i = i + 2;
                }
                //检测到"\n"并且替换开始,表示替换结束
                if (isReplace && str[i] == '\\' && i < str.Length - 1 && str[i + 1] == 'n')
                {
                    isReplace = false;
                    i = i + 2;
                }
                if (i < str.Length)
                {
                    strNew += isReplace && replaceDic.ContainsKey(str[i]) ? replaceDic[str[i]] : str[i];
                }
            }
            return strNew;
        }
    }
