using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Abp.Extensions
{
    /// <summary>
    /// 枚举扩展 方法
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 将枚举转换为键值对
        /// </summary>
        /// <returns></returns>
        public static List<NameValueText> EnumNameValueTextList(this Type type)
        {
            var list = Enum.GetValues(type);
            List<NameValueText> result = new List<NameValueText>();
            foreach (var item in list)
            {
                var fi = type.GetField(item.ToString());
                var display = fi.GetCustomAttribute<DisplayAttribute>();
                var text = display?.Name ?? fi?.Name ?? Enum.GetName(type, item);

                result.Add(new NameValueText
                {
                    Name = item.ToString(),
                    Text = text,
                    Value = Convert.ToInt32(item)
                });
            }
            return result;
        }

        /// <summary>
        /// 将字符串转换为 枚举
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static TEnum TryParse<TEnum>(this string str) where TEnum : struct
        {
            TEnum type;
            Enum.TryParse(str, true, out type);

            return type;
        }

        /// <summary>
        /// 获取枚举的展示名称
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetNameOrDisplayName(this Enum enumValue)
        {
            var value = enumValue.ToString();
            var enumField = enumValue.GetType().GetField(value);
            var displayName = enumField.GetCustomAttribute<DisplayAttribute>();


            return displayName?.Name ?? value;
        }
    }
}