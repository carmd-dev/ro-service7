using System;
using System.Collections.Specialized;

namespace Innova.Fixes
{
    /// <summary>
    /// Class holding the DTC Codes which are attached to the fix
    /// </summary>
    public class FixDTC
    {
        /// <summary>
		/// Default constructor for the DTC Object
		/// </summary>
		public FixDTC()
        {
            this.PrimaryDTC = "";
            this.SecondaryDTCList = "";
        }

        /// <summary>
        /// Constructor for the <see cref="FixDTC"/> object.
        /// </summary>
        /// <param name="fix"><see cref="Fix"/> the DTC object is associated to</param>
        /// <param name="primaryDTC"><see cref="string"/> primary DTC</param>
        /// <param name="secondaryDTCList"><see cref="string"/> comma separated list of secondary DTC Codes</param>
        public FixDTC(Fix fix, string primaryDTC, string secondaryDTCList)
        {
            //#BCUCodes_
            primaryDTC = primaryDTC.ToUpperCase();
            secondaryDTCList = secondaryDTCList.ToUpperCase();
            //#BCUCodes_

            this.Fix = fix;
            this.PrimaryDTC = primaryDTC;
            this.SecondaryDTCList = secondaryDTCList;

            if (this.PrimaryDTC == null)
            {
                this.PrimaryDTC = "";
            }
            if (this.SecondaryDTCList == null)
            {
                this.SecondaryDTCList = "";
            }
        }

        /// <summary>
        /// Constructor for the <see cref="FixDTC"/> object.
        /// </summary>
        /// <param name="fix"><see cref="Fix"/> the DTC object is associated to</param>
        /// <param name="primaryAndSecondaryDTCList"><see cref="string"/> comma separated list of primary and secondary DTC Codes. The primary DTC is the first in the list.</param>
        public FixDTC(Fix fix, string primaryAndSecondaryDTCList)
        {
            //#BCUCodes_
            primaryAndSecondaryDTCList = primaryAndSecondaryDTCList.ToUpperCase();
            //#BCUCodes_

            this.Fix = fix;
            if (primaryAndSecondaryDTCList.IndexOf(",") > 0)
            {
                this.PrimaryDTC = primaryAndSecondaryDTCList.Substring(0, primaryAndSecondaryDTCList.IndexOf(","));
                this.SecondaryDTCList = primaryAndSecondaryDTCList.Substring(primaryAndSecondaryDTCList.IndexOf(",") + 1);
            }
            else
            {
                this.PrimaryDTC = primaryAndSecondaryDTCList;
            }

            if (this.PrimaryDTC == null)
            {
                this.PrimaryDTC = "";
            }
            if (this.SecondaryDTCList == null)
            {
                this.SecondaryDTCList = "";
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Fix"/> this <see cref="FixDTC"/> object is associated to.
        /// </summary>
        public Fix Fix { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="string"/> primary DTC Code associated with this <see cref="Fix"/> record
        /// </summary>
        public string PrimaryDTC { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="string"/> comma separated list of secondary DTC Codes associated with this <see cref="Fix"/> record.
        /// </summary>
        public string SecondaryDTCList { get; set; }

        /// <summary>
        /// Gets the <see cref="StringCollection"/> of secondary error code list.
        /// </summary>
        public StringCollection GetSecondaryDTCListAsStringCollection()
        {
            StringCollection secondaryDTCStringCollection = new StringCollection();
            if (!String.IsNullOrEmpty(this.SecondaryDTCList))
            {
                string[] codes = this.SecondaryDTCList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string s in codes)
                {
                    secondaryDTCStringCollection.Add(s);
                }
            }
            return secondaryDTCStringCollection;
        }

        /// <summary>
        /// Gets a comma separated list of all DTCs with the PrimaryDTC first in the list.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string stringValue = this.PrimaryDTC;

            if (!String.IsNullOrEmpty(this.SecondaryDTCList))
            {
                stringValue += "," + this.SecondaryDTCList;
            }

            return stringValue;
        }
    }
}