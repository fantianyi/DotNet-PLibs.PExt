using System.Collections.Generic;
using System.Linq;

namespace System.Web.Mvc
{
    public static class ModelStateExt
    {
        public static List<string> ToList(this ModelStateDictionary modelStateDictionary)
        {
            return modelStateDictionary.Values.SelectMany(m => m.Errors)
                                       .Select(e => e.ErrorMessage)
                                       .ToList();

        }

        public static Dictionary<string, string[]> ToDictionary(this ModelStateDictionary modelStateDictionary)
        {
            return modelStateDictionary.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );
        }

        public static Dictionary<string, string> ToDictionary(this ViewDataDictionary viewDataDictionary)
        {
            return viewDataDictionary.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.ToString()
            );
        }
    }
}