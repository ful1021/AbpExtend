using System;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace Abp.Extensions
{
    public static class EntityTypeConfigurationExtensions
    {
        #region 改名私有方法

        public static void ChangeTablePrefix(this ModelBuilder modelBuilder, string prefix = "", params Type[] types)
        {
            foreach (var type in types)
            {
                var name = ToPlural(type.Name);
                SetTableName(modelBuilder, type, prefix + name);
            }
        }

        private static void SetTableName(ModelBuilder modelBuilder, Type entityType, string tableName, string schemaName = null)
        {
            if (schemaName == null)
            {
                modelBuilder.Entity(entityType).ToTable(tableName);
            }
            else
            {
                modelBuilder.Entity(entityType).ToTable(tableName, schemaName);
            }
        }

        /// <summary>
        /// 单词变成单数形式
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private static string ToSingular(string word)
        {
            Regex plural1 = new Regex("(?<keep>[^aeiou])ies$");
            Regex plural2 = new Regex("(?<keep>[aeiou]y)s$");
            Regex plural3 = new Regex("(?<keep>[sxzh])es$");
            Regex plural4 = new Regex("(?<keep>[^sxzhyu])s$");

            if (plural1.IsMatch(word))
                return plural1.Replace(word, "${keep}y");
            else if (plural2.IsMatch(word))
                return plural2.Replace(word, "${keep}");
            else if (plural3.IsMatch(word))
                return plural3.Replace(word, "${keep}");
            else if (plural4.IsMatch(word))
                return plural4.Replace(word, "${keep}");

            return word;
        }

        /// <summary>
        /// 单词变成复数形式
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private static string ToPlural(string word)
        {
            Regex plural1 = new Regex("(?<keep>[^aeiou])y$");
            Regex plural2 = new Regex("(?<keep>[aeiou]y)$");
            Regex plural3 = new Regex("(?<keep>[sxzh])$");
            Regex plural4 = new Regex("(?<keep>[^sxzhy])$");

            if (plural1.IsMatch(word))
                return plural1.Replace(word, "${keep}ies");
            else if (plural2.IsMatch(word))
                return plural2.Replace(word, "${keep}s");
            else if (plural3.IsMatch(word))
                return plural3.Replace(word, "${keep}es");
            else if (plural4.IsMatch(word))
                return plural4.Replace(word, "${keep}s");

            return word;
        }

        #endregion 改名私有方法
    }
}