using Metafuse3.NullableTypes;
using System;

namespace Innova.WebServiceV07.RO.Helpers
{
    public class DateTimeHelper
    {
        /// <summary>
        /// Gets the <see cref="NullableDateTime"/> date object from the supplied date string.
        /// </summary>
        /// <param name="dateTimeString"><see cref="string"/> date string</param>
        /// <returns><see cref="NullableDateTime"/> object, NullableDateTime.Null if invalid.</returns>
        public static NullableDateTime GetNullableDateTimeFromString(string dateTimeString)
        {
            NullableDateTime date = NullableDateTime.Null;
            if (!String.IsNullOrEmpty(dateTimeString))
            {
                DateTime d;
                if (DateTime.TryParse(dateTimeString, out d))
                {
                    date = d;
                }
            }
            return date;
        }
    }
}