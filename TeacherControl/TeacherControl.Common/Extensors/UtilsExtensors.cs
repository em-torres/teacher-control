using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TeacherControl.Common.Extensors
{
    public static class UtilsExtensors
    {
        public static int ToInt32(this string curr)
        {
            if (Int32.TryParse(curr, out int result)) return result;
            return 0;
        }

        public static float ToFloat(this string curr)
        {
            if (float.TryParse(curr, out float result)) return result;
            return 0;
        }

        public static DateTime ToDateTime(this string date)
        {
            if (DateTime.TryParse(date, out DateTime result)) return result;
            return DateTime.MinValue;
        }

        public static JObject ToJson(this object obj)
        {
            try
            {
                return JObject.FromObject(obj);
            } catch(Exception e)
            {
                //TODO: logger here
                return null;
            }
        }

        public static JArray ToJsonArray(this IEnumerable<object> array)
        {
            try
            {
                return JArray.FromObject(array);
            }
            catch (Exception e)
            {
                //TODO: logger here
                return null;
            }
        }

    }
}
