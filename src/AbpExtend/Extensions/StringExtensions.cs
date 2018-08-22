using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Abp.Extensions;
using Abp.Timing;

namespace Abp.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// 字符串分割成字符串组
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <param name="splitChars"></param>
        /// <returns></returns>
        public static string[] ToStringArray(this string sourceStr, char[] splitChars = null)
        {
            if (sourceStr.IsNullOrEmpty())
            {
                return new string[] { };
            }
            splitChars = splitChars ?? new char[] { ' ', '\r', '\n', '\t', '，', ',', '、', ';', '；' };

            //排除String数组中空格项
            return sourceStr
                .Trim()
                .Split(splitChars, System.StringSplitOptions.RemoveEmptyEntries)
                .Distinct()
                .ToArray();
        }

        /// <summary>
        /// 去掉字符串两端 \r \n \t 等特殊字符
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <returns></returns>
        public static string ToSafeString(this string sourceStr)
        {
            if (sourceStr.IsNullOrWhiteSpace())
            {
                return sourceStr;
            }
            var removeChars = new char[] { ' ', '\r', '\n', '\'', ',', '，', '\t' };

            return sourceStr.Trim(removeChars);
        }

        /// <summary>
        /// 简单验证中文姓名
        /// 姓名中不能包含(先生、小姐、男士、女士、太太、营养师)
        /// </summary>
        /// <param name="sourceName"></param>
        /// <returns></returns>
        public static bool SimpleValidateName(this string sourceName)
        {
            var reg = @"(?!.*先生.*|.*小姐.*|.*男士.*|.*女士.*|.*太太.*|.*营养师.*)^[\u4e00-\u9fa5]{0,}$";
            return Regex.IsMatch(sourceName, reg);
        }

        /// <summary>
        /// 将字符串（-分割） 拆分为时间范围
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static DateTimeRange ToDateTimeRange(this string str, string separator = " - ")
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                var temp = str.Trim().Split(separator);
                if (temp.Length >= 2)
                {
                    var StartTime = $"{temp[0].Trim()} 00:00:00".TryToDateTimeOrNull();
                    var EndTime = $"{temp[1].Trim()} 23:59:59".TryToDateTimeOrNull();
                    if (StartTime.HasValue && EndTime.HasValue)
                    {
                        return new DateTimeRange
                        {
                            StartTime = StartTime.Value,
                            EndTime = EndTime.Value
                        };
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 字符串转换为DateTime类型或者null，不成功返回null
        /// </summary>
        public static DateTime? TryToDateTimeOrNull(this string str)
        {
            DateTime? result = null;
            if (!string.IsNullOrWhiteSpace(str))
            {
                DateTime tmp;
                if (DateTime.TryParse(str, out tmp))
                {
                    result = tmp;
                }
            }
            return result;
        }

        /// <summary>
        /// 字符串转换为DateTime类型或者null，不成功返回null
        /// </summary>
        public static DateTime TryToDateTime(this string str, string defaultResult = "1900-01-01")
        {
            var result = str.TryToDateTimeOrNull();
            if (result == null && !string.IsNullOrWhiteSpace(defaultResult))
            {
                DateTime tmp;
                if (DateTime.TryParse(defaultResult, out tmp))
                {
                    result = tmp;
                }
            }
            return result ?? DateTime.MinValue;
        }

        /// <summary>
        /// 字符串转换为 int 类型
        /// </summary>
        public static int TryToInt32(this string str, int defaultResult = 0)
        {
            int result = defaultResult;
            if (!string.IsNullOrWhiteSpace(str))
            {
                int tmp;
                if (int.TryParse(str, out tmp))
                {
                    result = tmp;
                }
            }
            return result;
        }

        /// <summary>
        /// 字符串转换为 decimal 类型
        /// </summary>
        public static decimal TryToDecimal(this string str, decimal defaultResult = 0)
        {
            decimal result = defaultResult;
            if (!string.IsNullOrWhiteSpace(str))
            {
                decimal tmp;
                if (decimal.TryParse(str, out tmp))
                {
                    result = tmp;
                }
            }
            return result;
        }

        public static string UrlEncode(this string sourceStr)
        {
            var str = System.Net.WebUtility.UrlEncode(sourceStr) ?? sourceStr;

            var pattern = @"%\w{2}";
            var matchArray = Regex.Matches(str, pattern);

            foreach (var match in matchArray)
            {
                var matchStr = match.ToString();
                str = str.Replace(matchStr, matchStr.ToLower());
            }

            return str;
        }

        public static string UrlDecode(this string sourceStr)
        {
            return System.Net.WebUtility.UrlDecode(sourceStr);
        }

        /// <summary>
        /// 字符串中的中文，转换为 Unicode，非中文跳过
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToUnicode(this string str)
        {
            StringBuilder strResult = new StringBuilder();
            if (!string.IsNullOrEmpty(str))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    var val = (int)str[i];
                    if (val > 127)
                    {
                        strResult.Append("\\u");

                        strResult.Append(val.ToString("x"));
                    }
                    else
                    {
                        strResult.Append(str[i]);
                    }
                }
            }
            return strResult.ToString();
        }






        /// <summary>
        /// 是否是手机号
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static bool IsMobile(this string mobile)
        {
            if (!string.IsNullOrWhiteSpace(mobile))
            {
                string reg = @"^((13|14|15|16|17|18)+\d{9})$";
                Regex dReg = new Regex(reg);
                var b = dReg.IsMatch(mobile);
                return b;
            }
            return false;
        }

        /// <summary>
        /// 18位身份证号码验证
        /// </summary>
        public static bool CheckIdCard18(this string idNumber)
        {
            if (idNumber.Length < 18)
            {
                return false;
            }
            long n = 0;
            if (long.TryParse(idNumber.Remove(17), out n) == false
                || n < Math.Pow(10, 16) || long.TryParse(idNumber.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(idNumber.Remove(2), StringComparison.Ordinal) == -1)
            {
                return false;//省份验证
            }
            string birth = idNumber.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time;
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] ai = idNumber.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(wi[i]) * int.Parse(ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != idNumber.Substring(17, 1).ToLower())
            {
                return false;//校验码验证
            }
            return true;//符合GB11643-1999标准
        }
    }
}