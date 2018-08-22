using System;
using Abp.Timing;

namespace Abp.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 将日期数组 转为时间范围
        /// </summary>
        /// <param name="dataArr"></param>
        /// <returns></returns>
        public static DateTimeRange ToDateTimeRange(this DateTime[] dataArr)
        {
            if (dataArr != null && dataArr.Length == 2)
            {
                return new DateTimeRange
                {
                    StartTime = dataArr[0],
                    EndTime = dataArr[1].Date.AddDays(1)
                };
            }
            return null;
        }

        /// <summary>  
        /// 取得日期 对应年月的 第一天日期
        /// </summary>  
        /// <returns></returns>  
        public static DateTime FirstDay(this DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day);
        }

        /// <summary>
        /// 取得日期 对应年月的 最后一天日期
        /// </summary>
        /// <returns></returns>
        public static DateTime LastDay(this DateTime datetime)
        {
            return datetime.FirstDay().AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// 时间戳 1970-1-1
        /// </summary>
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Unix时间戳转为Datetime
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeFromUnixTimeStamp(this int unixTimeStamp)
        {
            return UnixEpoch.AddSeconds(unixTimeStamp).ToLocalTime();
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式(单位秒)
        /// </summary>
        /// <param name="time">DateTime时间格式</param>
        /// <returns>Unix时间戳格式</returns>
        public static int ToUnixTimestamp(this DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
    }
}