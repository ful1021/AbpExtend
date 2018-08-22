using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.Extensions
{
    public static class DecimalExtensions
    {
        /// <summary>
        /// 金额 元 转为 分
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int AmountYuanToCent(this decimal val)
        {
            return RoundToInt(val * 100);
        }

        /// <summary>
        /// 调用 Math.Round 方法后，再转为int
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int RoundToInt(this decimal val)
        {
            var obj = Math.Round(val);
            return Convert.ToInt32(obj);
        }

        /// <summary>
        /// 确保小数2位（四舍五入）。
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static decimal EnsureRoundTwoDigits(this decimal val)
        {
            return Math.Round(val, 2, MidpointRounding.AwayFromZero);
        }
    }
}
