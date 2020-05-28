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
                { ')', '⁾' }
            };
            var reg = new Regex(@"\\[uU](.+?)\\n");
            return Replace(replaceDic, str, reg);
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
            var reg = new Regex(@"\\[dD](.+?)\\n");
            return Replace(replaceDic, str, reg);
        }
        private static string Replace(Dictionary<char, char> replaceDic, string str, Regex reg)
        {
            var newStr = str;
            var matchs = reg.Matches(str);
            foreach (Match match in matchs)
            {
                var replaceStr = "";
                foreach (var item in match.Groups[1].Value)
                {
                    replaceStr += replaceDic[item];
                }
                newStr = newStr.Replace(match.Groups[0].Value, replaceStr);
            }

            return newStr;
        }
    }
