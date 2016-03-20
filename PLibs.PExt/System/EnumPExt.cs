using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ComponentModel;

namespace System
{
    public static class EnumPExt
    {
        /// <summary>
        /// Description 特性值，如没有用 Name 代替
        /// </summary>
        public static string ToDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault() as DescriptionAttribute;
            if (attribute == null)
            {
                return value.ToString();
            }
            else
            {
                return attribute.Description;
            }
        }
    }
}