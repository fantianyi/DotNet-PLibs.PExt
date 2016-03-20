using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace System
{
    public static class NameValueCollectionPExt
    {
        /// <summary>
        /// 将对象值转换成键值对
        /// </summary>
        /// <example>
        /// var args = HttpContext.Current.Request.Headers.ToDictionary();
        /// </example>
        public static Dictionary<string, object> ToDictionary(
            this NameValueCollection source)
        {
            return source.AllKeys.ToDictionary(k => k, k => (object)source[k]);
        }

        /// <summary>
        /// 将对象值转换成查询字符串
        /// </summary>
        /// <example>
        /// var nvc = new NameValueCollection();
        /// var args = nvc.ToQueryString(); 
        /// </example>
        public static string ToQueryString(this NameValueCollection source)
        {
            var sb = new StringBuilder();
            foreach (string key in source.Keys)
            {
                sb.AppendFormat("{0}={1}&", key, source[key]);
            }
            return sb.ToString().TrimEnd('&');
        }
    }
}