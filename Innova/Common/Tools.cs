using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;

namespace Innova
{
    /// <summary>
    /// The Tools object contains commonly used utility methods.
    /// </summary>
    public static class Tools
    {
        /// <summary>
        /// Parses a string value into a <see cref="Guid"/>.
        /// </summary>
        /// <param name="guidString">The GUID <see cref="string"/> value to be parsed.</param>
        /// <returns>A <see cref="Guid"/> value.</returns>
        public static Guid? GuidParse(string guidString)
        {
            Guid? guid = null;

            string unallowedCharactersPattern = @"[^A-Fa-f0-9]*";

            guidString = Regex.Replace(guidString, unallowedCharactersPattern, "");

            try { guid = new Guid(guidString); }
            catch { }

            return guid;
        }

        /// <summary>
        /// Gets a bool that indicates if two string collections are equal.
        /// </summary>
        /// <param name="stringValues1">A <see cref="string"/> array of strings.</param>
        /// <param name="stringValues2">A <see cref="string"/> array of strings.</param>
        /// <returns>A <see cref="bool"/> that indicates if two string collections are equal.</returns>
        public static bool AreStringCollectionsEqual(string[] stringValues1, string[] stringValues2)
        {
            // If the arrays are different lengths then we're done.
            if (stringValues1.Length != stringValues2.Length)
            {
                return false;
            }

            // Sort the collections
            stringValues1 = stringValues1.OrderBy(s => s).ToArray();
            stringValues2 = stringValues2.OrderBy(s => s).ToArray();

            // Now compare each posistion in the arrays for equality.
            // If we find a mismatch then we're done.
            for (int i = 0; i < stringValues1.Length; i++)
            {
                if (String.Compare(stringValues1[i], stringValues2[i], true) != 0)
                {
                    return false;
                }
            }

            // If we made it this far then the arrays are equal.
            return true;
        }

        /// <summary>
        /// Gets a bool that indicates if two string collections are equal.
        /// </summary>
        /// <param name="stringValues1">A <see cref="StringCollection"/> of strings.</param>
        /// <param name="stringValues2">A <see cref="StringCollection"/> of strings.</param>
        /// <returns>A <see cref="bool"/> that indicates if two string collections are equal.</returns>
        public static bool AreStringCollectionsEqual(StringCollection stringValues1, StringCollection stringValues2)
        {
            string[] stringValues1Array = new string[stringValues1.Count];
            stringValues1.CopyTo(stringValues1Array, 0);

            string[] stringValues2Array = new string[stringValues2.Count];
            stringValues2.CopyTo(stringValues2Array, 0);

            return AreStringCollectionsEqual(stringValues1Array, stringValues2Array);
        }

        /// <summary>
        /// Gets a bool that indicates if two string collections are equal.
        /// </summary>
        /// <param name="stringValues1">A <see cref="string"/> array of strings.</param>
        /// <param name="stringValues2">A <see cref="StringCollection"/> of strings.</param>
        /// <returns>A <see cref="bool"/> that indicates if two string collections are equal.</returns>
        public static bool AreStringCollectionsEqual(string[] stringValues1, StringCollection stringValues2)
        {
            string[] stringValues2Array = new string[stringValues2.Count];
            stringValues2.CopyTo(stringValues2Array, 0);

            return AreStringCollectionsEqual(stringValues1, stringValues2Array);
        }

        /// <summary>
        /// Gets a bool that indicates if two string collections are equal.
        /// </summary>
        /// <param name="stringValues1">A <see cref="string"/> array of strings.</param>
        /// <param name="values2">A <see cref="ArrayList"/> of values.</param>
        /// <returns>A <see cref="bool"/> that indicates if two string collections are equal.</returns>
        public static bool AreStringCollectionsEqual(string[] stringValues1, ArrayList values2)
        {
            string[] stringValues2Array = new string[values2.Count];

            for (int i = 0; i < values2.Count; i++)
            {
                object obj = values2[i];
                stringValues2Array[i] = obj.ToString();
            }

            return AreStringCollectionsEqual(stringValues1, stringValues2Array);
        }

        /// <summary>
        /// Gets a bool that indicates if two string collections are equal.
        /// </summary>
        /// <param name="list1">A <see cref="List{T}"/> of strings.</param>
        /// <param name="list2">A <see cref="List{T}"/> of strings.</param>
        /// <returns>A <see cref="bool"/> that indicates if two string collections are equal.</returns>
        public static bool AreStringCollectionsEqual(List<string> list1, List<string> list2)
        {
            if (list1.Count != list2.Count)
            {
                return false;
            }
            else
            {
                StringComparer stringComparer = StringComparer.Create(System.Globalization.CultureInfo.CurrentCulture, true);
                foreach (string str in list1)
                {
                    if (!list2.Contains(str, stringComparer))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        /// <summary>
        /// Gets a bool that indicates if two string collections are equal.
        /// </summary>
        /// <param name="list1">A <see cref="string"/> <see cref="Array"/> of string values.</param>
        /// <param name="list2">A <see cref="List{T}"/> of string values.</param>
        /// <returns>A <see cref="bool"/> that indicates if two string collections are equal.</returns>
        /// <returns></returns>
        public static bool AreStringCollectionsEqual(string[] list1, List<string> list2)
        {
            if (list1.Length != list2.Count)
            {
                return false;
            }
            else
            {
                foreach (string str in list1)
                {
                    if (list2.Any(s => s.Equals(str, StringComparison.OrdinalIgnoreCase)))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        //#BCUCodes_
        /// <summary>
        /// ToUpperCase, return empty string if Null
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToUpperCase(this string source)
        {
            return string.IsNullOrWhiteSpace(source) ? string.Empty : source.ToUpper();
        }

        //#BCUCodes_
    }
}