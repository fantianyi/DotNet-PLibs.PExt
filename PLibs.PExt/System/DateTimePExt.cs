using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ComponentModel;

namespace System
{
    public static class DateTimePExt
    {
        /// <summary> 返回指定年和月中的天数，以英文中的顺序数表示。
        /// </summary>
        /// <example>25th</example>
        public static string GetOrdinalDayOfMonth(this DateTime value)
        {
            switch (value.Day)
            {
                case 1:
                case 21:
                case 31:
                    return value.Day + "st";

                case 2:
                case 22:
                    return value.Day + "nd";

                case 3:
                case 23:
                    return value.Day + "rd";

                default:
                    return value.Day + "th";
            }
        }

        /// <summary> 返回指定年中的天和月数，以英文中的顺序数表示。
        /// </summary>
        /// <example>25th Dec</example>
        public static string GetOrdinalDayMonthOfYear(this DateTime value)
        {
            return value.GetOrdinalDayOfMonth() + value.ToString("r").Substring(7, 4);
        }

        /// <summary>
        /// 将对象值转换成Unix时间戳，长整数形式，单位毫秒
        /// </summary>
        /// <example>DateTime.Now.ToUnixTimestamp()</example>
        public static long ToUnixTimestamp(this DateTime source)
        {
            return (source.Ticks - 621355968000000000)/10000;
        }
        
        /// <summary>
        /// 将对象值转换成Unix时间戳，长整数形式，单位秒
        /// </summary>
        /// <example>DateTime.Now.ToUnixTimestamp()</example>
        public static long ToUnixTimestamp10(this DateTime source)
        {
            return (source.Ticks - 621355968000000000) / 10000000;
        }
    }
}