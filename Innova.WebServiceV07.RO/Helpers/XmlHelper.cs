using System;
using System.Text.RegularExpressions;

namespace Innova.WebServiceV07.RO
{
    public class XmlHelper
    {
        /*
        ** EXAMPLE of How-to call this method:
            xmlString = CleanInvalidXmlChars(xmlString);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);
        */

        /// <summary>
        /// Replace invalid XML characters
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string CleanInvalidXmlChars(string text)
        {
            string re = @"[^\x09\x0A\x0D\x20-\xD7FF\xE000-\xFFFD\x10000-x10FFFF]";
            return Regex.Replace(text, re, "");
        }

        //#SP36
        public static string CleanInvalidXmlChars2(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            var isValid = new Predicate<char>(value =>
                (value >= 0x0020 && value <= 0xD7FF) ||
                (value >= 0xE000 && value <= 0xFFFD) ||
                value == 0x0009 ||
                value == 0x000A ||
                value == 0x000D);

            return new string(Array.FindAll(input.ToCharArray(), isValid));
        }
    }
}