using System.Text.RegularExpressions;

namespace Innova
{
    /// <summary>
    /// Static class has methods for string formatting.
    /// </summary>
    public class InnovaFormatting
    {
        static InnovaFormatting()
        {
        }

        /// <summary>
        /// Removes all non-numeric characters from a phone number and the leading "1" of the area code.
        /// </summary>
        /// <param name="phoneNumber">The <see cref="string"/> phone number to be cleaned.</param>
        /// <returns>A 10 digit <see cref="string"/> phone number with no formating or leading "1".</returns>
        public static string CleanPhoneNumber(string phoneNumber)
        {
            Regex regex = new Regex(@"[^\d]?");

            phoneNumber = regex.Replace(phoneNumber, "");

            if (phoneNumber.Length > 10 && phoneNumber.StartsWith("1"))
            {
                phoneNumber = phoneNumber.Substring(1);
            }

            return phoneNumber;
        }

        /// <summary>
        /// Formats a phone number as (xxx) xxx-xxxx or xxx-xxxx depending on how many digits are supplied in the input parameter.
        /// </summary>
        /// <param name="phoneNumber">The <see cref="string"/> phone number to be formatted.</param>
        /// <returns>A formatted <see cref="string"/> phone number.</returns>
        public static string FormatPhoneNumber(string phoneNumber)
        {
            phoneNumber = CleanPhoneNumber(phoneNumber);

            if (phoneNumber.Length == 7)
            {
                phoneNumber = string.Format("{0}-{1}", phoneNumber.Substring(0, 3), phoneNumber.Substring(3));
            }
            else if (phoneNumber.Length == 10)
            {
                phoneNumber = string.Format("{0}-{1}-{2}", phoneNumber.Substring(0, 3), phoneNumber.Substring(3, 3), phoneNumber.Substring(6));
            }

            return phoneNumber;
        }
    }
}