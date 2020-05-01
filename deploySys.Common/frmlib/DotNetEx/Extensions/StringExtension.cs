using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace System
{
    /// <summary>
    /// StringExtensions
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 判断 s 是否为 null 或者空字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }
        /// <summary>
        /// 判断 s 是否为 null、空还是仅由空白字符。
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }
        /// <summary>
        /// 判断 s 是否不为 null 或者空字符串。
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string s)
        {
            return !string.IsNullOrEmpty(s);
        }
        /// <summary>
        /// 判断 s 是否不为 null、空还是仅由空白字符。
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNotNullOrWhiteSpace(this string s)
        {
            return !string.IsNullOrWhiteSpace(s);
        }

        /// <summary>
        /// 将字符串中的 Encoding.Default 编码为一个字节序列。
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string s)
        {
            return s.ToBytes(Encoding.UTF8);
        }
        /// <summary>
        /// 将字符串中的所有字符编码为一个字节序列。
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string s, Encoding encoding)
        {
            byte[] bytes = encoding.GetBytes(s);
            return bytes;
        }

        /// <summary>
        /// 使用 Encoding.UTF8 对 s 加密
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToMD5(this string s)
        {
            return ToMD5(s, Encoding.UTF8);
        }
        public static string ToMD5(this string s, Encoding encoding)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var inputBytes = encoding.GetBytes(s);
                var hashBytes = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                foreach (var hashByte in hashBytes)
                {
                    sb.Append(hashByte.ToString("X2"));
                }

                return sb.ToString();
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns>string.Format(format, args)</returns>
        public static string ToFormat(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static bool? ToBoolean(this string s, bool? defaultValue = null)
        {
            bool val;
            if (bool.TryParse(s, out val))
                return val;

            return defaultValue;
        }
        public static byte? ToByte(this string s, byte? defaultValue = null)
        {
            byte val;
            if (byte.TryParse(s, out val))
                return val;

            return defaultValue;
        }
        public static short? ToInt16(this string s, short? defaultValue = null)
        {
            short val;
            if (short.TryParse(s, out val))
                return val;

            return defaultValue;
        }
        public static int? ToInt32(this string s, int? defaultValue = null)
        {
            int val;
            if (int.TryParse(s, out val))
                return val;

            return defaultValue;
        }
        public static long? ToInt64(this string s, long? defaultValue = null)
        {
            long val;
            if (long.TryParse(s, out val))
                return val;

            return defaultValue;
        }
        public static float? ToFloat(this string s, float? defaultValue = null)
        {
            float val;
            if (float.TryParse(s, out val))
                return val;

            return defaultValue;
        }
        public static double? ToDouble(this string s, double? defaultValue = null)
        {
            double val;
            if (double.TryParse(s, out val))
                return val;

            return defaultValue;
        }
        public static decimal? ToDecimal(this string s, decimal? defaultValue = null)
        {
            decimal val;
            if (decimal.TryParse(s, out val))
                return val;

            return defaultValue;
        }
        public static Guid? ToGuid(this string s, Guid? defaultValue = null)
        {
            Guid val;
            if (Guid.TryParse(s, out val))
                return val;

            return defaultValue;
        }
        public static DateTime? ToDateTime(this string s, DateTime? defaultValue = null)
        {
            DateTime val;
            if (DateTime.TryParse(s, out val))
                return val;

            return defaultValue;
        }
        public static TEnum? ToEnum<TEnum>(this string s, TEnum? defaultValue = null) where TEnum : struct
        {
            int val;
            if (!int.TryParse(s, out val))
            {
                return defaultValue;
            }

            TEnum e = (TEnum)Enum.ToObject(typeof(TEnum), val);
            return e;
        }

        public static List<string> SplitAsList(this string s, params char[] separator)
        {
            return s.SplitAsList<string>(separator);
        }

        public static List<T> SplitAsList<T>(this string s, params char[] separator)
        {
            List<T> retList = new List<T>();

            if (string.IsNullOrEmpty(s))
                return retList;

            string[] arr = s.Split(separator);

            foreach (string item in arr)
            {
                if (item is T)
                {
                    retList.Add((T)(object)item);
                }
                else
                    retList.Add((T)Convert.ChangeType(item, typeof(T)));
            }

            return retList;
        }
    }
    
    /// <summary>
    /// 通过的Regex类实现了一些特殊功能数据检查
    /// </summary>
    public class MetarnetRegex
    {

        private static MetarnetRegex instance = null;
        public static MetarnetRegex GetInstance()
        {
            if (MetarnetRegex.instance == null)
            {
                MetarnetRegex.instance = new MetarnetRegex();
            }
            return MetarnetRegex.instance;
        }
        private MetarnetRegex()
        {
        }
        /// <summary>
        /// 判断输入的字符串只包含汉字
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsChineseCh(string input)
        {
            Regex regex = new Regex("^[\u4e00-\u9fa5]+$");
            return regex.IsMatch(input);
        }
        /// <summary>
        /// 匹配3位或4位区号的电话号码，其中区号可以用小括号括起来，
        /// 也可以不用，区号与本地号间可以用连字号或空格间隔，
        /// 也可以没有间隔
        /// \(0\d{2}\)[- ]?\d{8}|0\d{2}[- ]?\d{8}|\(0\d{3}\)[- ]?\d{7}|0\d{3}[- ]?\d{7}
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsPhone(string input)
        {
            string pattern = "^\\(0\\d{2}\\)[- ]?\\d{8}$|^0\\d{2}[- ]?\\d{8}$|^\\(0\\d{3}\\)[- ]?\\d{7}$|^0\\d{3}[- ]?\\d{7}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }
        /// <summary>
        /// 判断输入的字符串是否是一个合法的手机号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsMobilePhone(string input)
        {
            Regex regex = new Regex("^[1]+[3,5,8]+\\d{9}$");
            return regex.IsMatch(input);

        }


        /// <summary>
        /// 判断输入的字符串只包含数字
        /// 可以匹配整数和浮点数
        /// ^-?\d+$|^(-?\d+)(\.\d+)?$
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNumber(string input)
        {
            string pattern = "^-?\\d+$|^(-?\\d+)(\\.\\d+)?$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }
        /// <summary>
        /// 匹配非负整数
        ///
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNotNagtive(string input)
        {
            Regex regex = new Regex(@"^\d+$");
            return regex.IsMatch(input);
        }
        /// <summary>
        /// 匹配正整数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsUint(string input)
        {
            Regex regex = new Regex("^[0-9]*[1-9][0-9]*$");
            return regex.IsMatch(input);
        }
        /// <summary>
        /// 判断输入的字符串字包含英文字母
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsEnglisCh(string input)
        {
            Regex regex = new Regex("^[A-Za-z]+$");
            return regex.IsMatch(input);
        }


        /// <summary>
        /// 判断输入的字符串是否是一个合法的Email地址
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsEmail(string input)
        {
            string pattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }


        /// <summary>
        /// 判断输入的字符串是否只包含数字和英文字母
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNumAndEnCh(string input)
        {
            string pattern = @"^[A-Za-z0-9]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }


        /// <summary>
        /// 判断输入的字符串是否是一个超链接
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsURL(string input)
        {
            //string pattern = @"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
            string pattern = @"^[a-zA-Z]+://(\w+(-\w+)*)(\.(\w+(-\w+)*))*(\?\S*)?$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }


        /// <summary>
        /// 判断输入的字符串是否是表示一个IP地址
        /// </summary>
        /// <param name="input">被比较的字符串</param>
        /// <returns>是IP地址则为True</returns>
        public static bool IsIPv4(string input)
        {

            string[] IPs = input.Split('.');
            Regex regex = new Regex(@"^\d+$");
            for (int i = 0; i < IPs.Length; i++)
            {
                if (!regex.IsMatch(IPs[i]))
                {
                    return false;
                }
                if (Convert.ToUInt16(IPs[i]) > 255)
                {
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// 计算字符串的字符长度，一个汉字字符将被计算为两个字符
        /// </summary>
        /// <param name="input">需要计算的字符串</param>
        /// <returns>返回字符串的长度</returns>
        public static int GetCount(string input)
        {
            return Regex.Replace(input, @"[\u4e00-\u9fa5/g]", "aa").Length;
        }

        /// <summary>
        /// 调用Regex中IsMatch函数实现一般的正则表达式匹配
        /// </summary>
        /// <param name="pattern">要匹配的正则表达式模式。</param>
        /// <param name="input">要搜索匹配项的字符串</param>
        /// <returns>如果正则表达式找到匹配项，则为 true；否则，为 false。</returns>
        public static bool IsMatch(string pattern, string input)
        {
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 从输入字符串中的第一个字符开始，用替换字符串替换指定的正则表达式模式的所有匹配项。
        /// </summary>
        /// <param name="pattern">模式字符串</param>
        /// <param name="input">输入字符串</param>
        /// <param name="replacement">用于替换的字符串</param>
        /// <returns>返回被替换后的结果</returns>
        public static string Replace(string pattern, string input, string replacement)
        {
            Regex regex = new Regex(pattern);
            return regex.Replace(input, replacement);
        }

        /// <summary>
        /// 在由正则表达式模式定义的位置拆分输入字符串。
        /// </summary>
        /// <param name="pattern">模式字符串</param>
        /// <param name="input">输入字符串</param>
        /// <returns></returns>
        public static string[] Split(string pattern, string input)
        {
            Regex regex = new Regex(pattern);
            return regex.Split(input);
        }
        /// <summary>
        /// 判断输入的字符串是否是合法的IPV6 地址
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsIPV6(string input)
        {
            string pattern = "";
            string temp = input;
            string[] strs = temp.Split(':');
            if (strs.Length > 8)
            {
                return false;
            }
            int count = MetarnetRegex.GetStringCount(input, "::");
            if (count > 1)
            {
                return false;
            }
            else if (count == 0)
            {
                pattern = @"^([\da-f]{1,4}:){7}[\da-f]{1,4}$";

                Regex regex = new Regex(pattern);
                return regex.IsMatch(input);
            }
            else
            {
                pattern = @"^([\da-f]{1,4}:){0,5}::([\da-f]{1,4}:){0,5}[\da-f]{1,4}$";
                Regex regex1 = new Regex(pattern);
                return regex1.IsMatch(input);
            }

        }
        /* *******************************************************************
        * 1、通过“:”来分割字符串看得到的字符串数组长度是否小于等于8
        * 2、判断输入的IPV6字符串中是否有“::”。
        * 3、如果没有“::”采用 ^([\da-f]{1,4}:){7}[\da-f]{1,4}$ 来判断
        * 4、如果有“::” ，判断"::"是否止出现一次
        * 5、如果出现一次以上 返回false
        * 6、^([\da-f]{1,4}:){0,5}::([\da-f]{1,4}:){0,5}[\da-f]{1,4}$
        * ******************************************************************/
        /// <summary>
        /// 判断字符串compare 在 input字符串中出现的次数
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <param name="compare">用于比较的字符串</param>
        /// <returns>字符串compare 在 input字符串中出现的次数</returns>
        private static int GetStringCount(string input, string compare)
        {
            int index = input.IndexOf(compare);
            if (index != -1)
            {
                return 1 + GetStringCount(input.Substring(index + compare.Length), compare);
            }
            else
            {
                return 0;
            }

        }
    }

}
