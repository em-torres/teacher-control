using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
