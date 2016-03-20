using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.ComponentModel;

namespace System
{
    public static class DictionaryPExt
    {
        /// <summary>
        /// 仅仅增加一次同名键的键值对
        /// </summary>
        /// <example>
        /// var args = new Dictionary&lt;string, object&gt;();
        /// args.AddOnceOnly("ID", 123);
        /// args.AddOnceOnly("ID", 456);
        /// ID值为 123
        /// </example>
        public static void AddOnceOnly<TKey, TValue>(
            this Dictionary<TKey, TValue> source, TKey key, TValue val)
        {
            if (!source.ContainsKey(key))
            {
                source.Add(key, val);
            }
        }

        /// <summary>
        /// 仅仅增加一次键值对的集合
        /// 键同名而值不同的键值对，附加前缀
        /// </summary>
        /// <param name="pre">前缀</param>
        /// <example>
        /// var args = new Dictionary&lt;string, object&gt;();
        /// args.AddOnceOnly("ID", 123);
        /// args.AddOnceOnly("ID", 456);
        /// ID值为 123
        /// </example>
        public static void AddDict<T>(
            this Dictionary<string, T> source, string pre, Dictionary<string, T> dict)
        {
            foreach (var pair in dict)
            {
                source.AddOnceOnly(pre + pair.Key, pair.Value);
            }
        }

        /// <summary>
        /// 仅仅增加一次键值对的集合
        /// 键同名而值不同的键值对，附加前缀
        /// </summary>
        /// <param name="pre">前缀</param>
        /// <param name="pre">前缀</param>
        /// <example>
        /// var args = new Dictionary&lt;string, object&gt;();
        /// args.AddOnceOnly("ID", 123);
        /// args.AddOnceOnly("ID", 456);
        /// ID值为 123
        /// </example>
        public static void AddDictOnceOnly<T>(
            this Dictionary<string, T> source, string pre, Dictionary<string, T> dict)
        {
            foreach (var pair in dict)
            {
                if (!source.ContainsKey(pair.Key))
                {
                    source.Add(pair.Key.ToLower(), pair.Value);
                }
                else if (source[pair.Key].Equals(pair.Value))
                {
                    ;
                }
                else
                {
                    source.AddOnceOnly(pre + pair.Key, pair.Value);
                }
            }
        }

        /// <summary>
        /// 仅仅增加一次名值的集合
        /// 键同名而值不同的键值对，附加前缀
        /// </summary>
        /// <param name="pre">前缀</param>
        /// <param name="collection">内容</param>
        /// <example>
        /// var args = new Dictionary&lt;string, object&gt;();
        /// args.AddNameValueOnceOnly("request_header-", HttpContext.Current.Request.Headers);
        /// </example>
        public static void AddNameValueOnceOnly(
            this Dictionary<string, object> source, string pre, NameValueCollection collection)
        {
            var dict = collection.ToDictionary();
            source.AddDictOnceOnly(pre, dict);
        }
    }
}