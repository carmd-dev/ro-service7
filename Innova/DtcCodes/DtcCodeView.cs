using System.Collections.Specialized;

namespace Innova.DtcCodes
{
    /// <summary>
    /// An object intended for displaying DTC codes.
    /// </summary>
    public class DtcCodeView
    {
        private string codeType = "";
        private StringCollection codes;

        /// <summary>
        ///
        /// </summary>
        /// <param name="codeType"></param>
        public DtcCodeView(string codeType)
        {
            this.codeType = codeType;
            this.codes = new StringCollection();
        }

        /// <summary>
        ///
        /// </summary>
        public string CodeType
        {
            get
            {
                return this.codeType;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public StringCollection Codes
        {
            get
            {
                return this.codes;
            }
        }
    }
}