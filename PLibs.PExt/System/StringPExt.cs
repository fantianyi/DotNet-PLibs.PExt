using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ComponentModel;

namespace System
{
    public static class StringPExt
    {
        /// <summary>
        /// Description 特性值，如没有用 Name 代替
        /// </summary>
        public static object ConvertTo(this String value, TypeCode type)
        {
            switch (type)
            {
                case TypeCode.Int32:
                    Int32 result;
                    Int32.TryParse(value, out result);
                    return result;
                case TypeCode.String:
                    return value;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 转换成MD5散列字符串
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="toLower">返回哈希值格式 true：英文小写，false：英文大写</param>
        /// <returns>MD5散列字符串</returns>
        /// <example>
        /// "123456".ToMd5Hash();
        /// </example>
        public static string ToMd5Hash(this string source, bool toLower = true)
        {
            var result = string.Empty;

            using (var hash = new System.Security.Cryptography.MD5CryptoServiceProvider())
            {
                hash.Initialize();
                var bytes = System.Text.Encoding.UTF8.GetBytes(source);
                bytes = hash.ComputeHash(bytes);
                result = Convert.ToBase64String(bytes);
            }

            return toLower ? result.ToLower() : result.ToUpper();
        }


        /// <summary>
        /// 转换成RSA1散列字符串
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="toLower">返回哈希值格式 true：英文小写，false：英文大写</param>
        /// <returns>RSA1散列字符串</returns>
        /// <example>
        /// "123456".ToRsa1Hash();
        /// </example>
        public static string ToRsa1Hash(this string source, bool toLower = true)
        {
            var result = string.Empty;

            using (var hash = new System.Security.Cryptography.SHA1CryptoServiceProvider())
            {
                hash.Initialize();
                var bytes = System.Text.Encoding.UTF8.GetBytes(source);
                bytes = hash.ComputeHash(bytes);
                result = Convert.ToBase64String(bytes);
            }

            return toLower ? result.ToLower() : result.ToUpper();
        }
    }
}