using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp
{
    /// <summary>
    /// 存储 键 值 文本说明
    /// </summary>
    public class NameValueText : NameValueText<int>
    {
    }
    /// <summary>
    /// 存储 键 值 文本说明
    /// </summary>
    [Serializable]
    public class NameValueText<T>
    {
        /// <summary>
        /// Creates a new <see cref="NameValueText"/>.
        /// </summary>
        public NameValueText()
        {

        }
        /// <summary>
        /// Creates a new <see cref="NameValueText"/>.
        /// </summary>
        public NameValueText(string name, T value, string text)
        {
            this.Name = name;
            this.Value = value;
            this.Text = text;
        }
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Value.
        /// </summary>
        public T Value { get; set; }
        /// <summary>
        /// 文本说明.
        /// </summary>
        public string Text { get; set; }
    }
}
