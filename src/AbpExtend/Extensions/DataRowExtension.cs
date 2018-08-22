using System;
using System.Data;
using Abp.UI;

namespace Abp.Extensions
{
    /// <summary>
    /// DataRow 扩展方法
    /// </summary>
    public static class DataRowExtension
    {
        /// <summary>
        /// 检验 并得到值
        /// </summary>
        /// <param name="row"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <param name="msg"></param>
        /// <param name="otherCheck"></param>
        /// <param name="isNullable"></param>
        /// <returns></returns>
        public static string CheckGet(this DataRow row, int rowIndex, int columnIndex, string msg = "", Func<string, bool> otherCheck = null, bool isNullable = false)
        {
            var val = row[columnIndex].ToString().Trim();
            if (!isNullable)
            {
                if (string.IsNullOrWhiteSpace(val))
                {
                    throw new UserFriendlyException($"对比模板，检查第 {columnIndex + 1} 列，第 {rowIndex} 行 {msg} 值有误");
                }
            }
            if (!string.IsNullOrWhiteSpace(val))
            {
                if (otherCheck?.Invoke(val) ?? false)
                {
                    throw new UserFriendlyException($"对比模板，检查第 {columnIndex + 1} 列，第 {rowIndex} 行 {msg} 值为 {val} 有误");
                }
            }
            return val;
        }

        public static string TryToString(this DataRow row, int column, string result = "")
        {
            object val = row[column];
            return TryToString(val, result);
        }

        public static string TryToString(this DataRow row, string column, string result = "")
        {
            object val = row[column];
            return TryToString(val, result);
        }

        public static Guid TryToGuid(this DataRow row, int column, Guid result)
        {
            object val = row[column];
            return TryToGuid(val, result);
        }

        public static Guid TryToGuid(this DataRow row, string column, Guid result)
        {
            object val = row[column];
            return TryToGuid(val, result);
        }

        public static Guid TryToGuid(this DataRow row, string column)
        {
            return row.TryToGuid(column, Guid.Empty);
        }

        public static decimal TryToDecimal(this DataRow row, int column, decimal result = 0)
        {
            object val = row[column];
            return TryToDecimal(val, result);
        }

        public static int TryToInt32(this DataRow row, int column, int result = 0)
        {
            object val = row[column];
            return TryToInt32(val, result);
        }

        public static int TryToInt32(this DataRow row, string column, int result = 0)
        {
            object val = row[column];
            return TryToInt32(val, result);
        }

        public static DateTime TryToDateTime(this DataRow row, int column, DateTime result)
        {
            object val = row[column];
            return TryToDateTime(val, result);
        }

        public static DateTime? TryToDateTimeOrNull(this DataRow row, int column)
        {
            object val = row[column];
            return TryToDateTimeOrNull(val);
        }

        public static DateTime TryToDateTime(this DataRow row, string column, DateTime result)
        {
            object val = row[column];
            return TryToDateTime(val, result);
        }

        public static DateTime TryToDateTime(this DataRow row, int column, string result)
        {
            return row.TryToDateTime(column, Convert.ToDateTime(result));
        }

        public static DateTime TryToDatetime(this DataRow row, string column, string result)
        {
            return row.TryToDateTime(column, Convert.ToDateTime(result));
        }

        #region private

        private static string TryToString(object val, string result)
        {
            if (val == null || val == DBNull.Value || string.IsNullOrWhiteSpace(val.ToString()) || val.ToString().ToLower() == "null")
            {
                return result;
            }
            return val.ToString();
        }

        private static Guid TryToGuid(object val, Guid result)
        {
            if (val == null || val == DBNull.Value || string.IsNullOrWhiteSpace(val.ToString()) || val.ToString().ToLower() == "null")
            {
                return result;
            }
            Guid dt;
            if (Guid.TryParse(val.ToString(), out dt))
            {
                result = dt;
            }
            return result;
        }

        private static int TryToInt32(object val, int result = 0)
        {
            if (val == null || val == DBNull.Value || string.IsNullOrWhiteSpace(val.ToString()) || val.ToString().ToLower() == "null")
            {
                return result;
            }
            int dt;
            if (int.TryParse(val.ToString(), out dt))
            {
                result = dt;
            }
            return result;
        }

        private static decimal TryToDecimal(object val, decimal result = 0)
        {
            if (val == null || val == DBNull.Value || string.IsNullOrWhiteSpace(val.ToString()) || val.ToString().ToLower() == "null")
            {
                return result;
            }
            decimal dt;
            if (decimal.TryParse(val.ToString(), out dt))
            {
                result = dt;
            }
            return result;
        }

        public static DateTime? TryToDateTimeOrNull(object val, DateTime? result = null)
        {
            if (val == null || val == DBNull.Value || string.IsNullOrWhiteSpace(val.ToString()) || val.ToString().ToLower() == "null")
            {
                return result;
            }
            DateTime dt;
            if (DateTime.TryParse(val.ToString(), out dt))
            {
                result = dt;
            }
            return result;
        }

        private static DateTime TryToDateTime(object val, DateTime result)
        {
            if (val == null || val == DBNull.Value || string.IsNullOrWhiteSpace(val.ToString()) || val.ToString().ToLower() == "null")
            {
                return result;
            }
            DateTime dt;
            if (DateTime.TryParse(val.ToString(), out dt))
            {
                result = dt;
            }
            return result;
        }

        #endregion private
    }
}