using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ComponentModel;

namespace System
{
    public static class GuidPExt
    {
        /// <summary> 根据时间产生有序的GUID编码。
        /// </summary>
        public static Guid ToOrdered(this Guid value)
        {
            byte[] guidArray = value.ToByteArray();

            var baseDate = new DateTime(1900, 1, 1);
            DateTime now = DateTime.Now;
            var days = new TimeSpan(now.Ticks - baseDate.Ticks);
            TimeSpan msecs = now.TimeOfDay;

            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            //Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            //Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 16, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 14, 4);

            return new Guid(guidArray);
        }

        /// <summary> GUID转换为Int64的数字序列。
        /// </summary>
        public static long ToInt64(this Guid value)
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }

        /// <summary> GUID转换为Int64的数字序列。
        /// </summary>
        public static int ToInt(this Guid value)
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return Math.Abs(BitConverter.ToInt32(buffer, 0));
        }
    }
}