using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ComponentModel;
using System.Text;

namespace System.IO
{
    public static class StreamPExt
    {
        /// <summary>
        /// 转换成MD5散列字符串
        /// </summary>
        /// <param name="source">源数据流</param>
        /// <param name="toLower">返回哈希值格式 true：英文小写，false：英文大写</param>
        /// <returns>MD5散列字符串</returns>
        /// <example>
        /// file.ToMd5Hash();
        /// </example>
        public static string ToMd5Hash(this Stream source, bool toLower = true)
        {
            var result = string.Empty;

            using (var hash = new System.Security.Cryptography.MD5CryptoServiceProvider())
            {
                hash.Initialize();
                var bytes = hash.ComputeHash(source);
                result = Convert.ToBase64String(bytes);
            }

            return toLower ? result.ToLower() : result.ToUpper();
        }

        /// <summary>
        /// 转换成RSA1散列字符串
        /// </summary>
        /// <param name="source">源数据流</param>
        /// <param name="toLower">返回哈希值格式 true：英文小写，false：英文大写</param>
        /// <returns>RSA1散列字符串</returns>
        /// <example>
        /// file.ToRsa1Hash();
        /// </example>
        public static string ToRsa1Hash(this Stream source, bool toLower = true)
        {
            var result = string.Empty;

            using (var hash = new System.Security.Cryptography.SHA1CryptoServiceProvider())
            {
                hash.Initialize();
                var bytes = hash.ComputeHash(source);
                result = Convert.ToBase64String(bytes);
            }

            return toLower ? result.ToLower() : result.ToUpper();
        }
    }
}