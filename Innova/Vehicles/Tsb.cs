using Metafuse3.Data.SqlClient;
using Metafuse3.Web.UI;
using System;
using System.Collections.Generic;

namespace Innova.Vehicles
{
    /// <summary>
    /// Summary description for Tsb.
    /// </summary>
    public class Tsb
    {
        private int tsbId;
        private string description = "";
        private string fileNamePDF = "";
        private string manufacturerNumber = "";
        private DateTime issueDate;
        private string tsbText = "";
        private DateTime createdDateTime;
        private DateTime updatedDateTime;
        private string system = "";
        private string subSystem = "";
        private string autoSystem = "";

        //Added on 09/15/2017 at 2:00 PM by INNOVA Dev Team
        private int tsbAAIA;

        private int tsbCount;
        //Added on 09/15/2017 at 2:00 PM by INNOVA Dev Team

        private List<TsbCategory> tsbCategories;
        private List<TsbType> tsbTypes;
        private List<string> dtCcodes;

        private string categoryList = null;

        /// <summary>
        /// The default constuctor for the <see cref="Tsb"/> object.
        /// </summary>
        public Tsb()
        {
            this.tsbCategories = new List<TsbCategory>();
            this.tsbTypes = new List<TsbType>();
            this.dtCcodes = new List<string>();
        }

        //Added on 09/15/2017 at 2:00 PM by INNOVA Dev Team
        /// <summary>
        /// Gets or sets the <see cref="int"/> value of the TSB AAIA.
        /// </summary>
        public int TsbAAIA
        {
            get
            {
                return tsbAAIA;
            }
            set
            {
                tsbAAIA = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> number of the TSB count.
        /// </summary>
        public int TsbCount
        {
            get
            {
                return tsbCount;
            }
            set
            {
                tsbCount = value;
            }
        }

        //Added on 09/15/2017 at 2:00 PM by INNOVA Dev Team

        /// <summary>
        /// Gets or sets the <see cref="int"/> id of the TSB record.
        /// </summary>
        public int TsbId
        {
            get
            {
                return tsbId;
            }
            set
            {
                tsbId = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> description.
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> file name of the PDF of the TSB.
        /// </summary>
        public string FileNamePDF
        {
            get
            {
                return fileNamePDF;
            }
            set
            {
                fileNamePDF = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> manufacturer number.
        /// </summary>
        public string ManufacturerNumber
        {
            get
            {
                return manufacturerNumber;
            }
            set
            {
                manufacturerNumber = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> that the TSB was issued.
        /// </summary>
        public DateTime IssueDate
        {
            get
            {
                return issueDate;
            }
            set
            {
                issueDate = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> text of the TSB.
        /// </summary>
        public string TsbText
        {
            get
            {
                return tsbText;
            }
            set
            {
                tsbText = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> that the TSB record was created.
        /// </summary>
        public DateTime CreatedDateTime
        {
            get
            {
                return createdDateTime;
            }
            set
            {
                createdDateTime = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> that the TSB record was last updated.
        /// </summary>
        public DateTime UpdatedDateTime
        {
            get
            {
                return updatedDateTime;
            }
            set
            {
                updatedDateTime = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> system this TSB belongs to.
        /// </summary>
        public string System
        {
            get
            {
                return system;
            }
            set
            {
                system = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> sub-system this TSB belongs to.
        /// </summary>
        public string SubSystem
        {
            get
            {
                return subSystem;
            }
            set
            {
                subSystem = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> auto-system this TSB belongs to.
        /// </summary>
        public string AutoSystem
        {
            get
            {
                return autoSystem;
            }
            set
            {
                autoSystem = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="TsbCategory"/> collection that indicates which categories apply to this TSB.
        /// </summary>
        public List<TsbCategory> TsbCategories
        {
            get
            {
                return this.tsbCategories;
            }
        }

        /// <summary>
        /// Gets the <see cref="TsbType"/> collection that indicates which types apply to this TSB.
        /// </summary>
        public List<TsbType> TsbTypes
        {
            get
            {
                return this.tsbTypes;
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> collection of codes that apply to this this TSB and that were also used to in the serch process.
        /// </summary>
        public List<string> DTCcodes
        {
            get
            {
                return this.dtCcodes;
            }
        }

        /// <summary>
        /// Gets a <see cref="string"/> list of the category names.
        /// </summary>
        public string CategoryList
        {
            get
            {
                if (categoryList == null)
                {
                    foreach (TsbCategory cat in this.TsbCategories)
                    {
                        if (!String.IsNullOrEmpty(categoryList))
                        {
                            categoryList += ", ";
                        }

                        categoryList += cat.Description;
                    }
                }

                return categoryList;
            }
        }

        /// <summary>
        /// Gets the relative URL to a TSB given the TSB's PDF file name.
        /// </summary>
        /// <param name="tsbRootFolder">The <see cref="string"/> root of the TSB folder.</param>
        /// <returns>A <see cref="string"/> relative URL to a TSB.</returns>
        public string GetTsbVirtualPath(string tsbRootFolder)
        {
            string tsbIdString = this.TsbId.ToString();
            string prefix = (tsbIdString.Length > 3) ? tsbIdString.Substring(0, tsbIdString.Length - 3) : "0";
            string virtualPath = tsbRootFolder;
            if (!String.IsNullOrEmpty(virtualPath))
            {
                virtualPath += "/";
            }
            virtualPath += prefix + "000/" + this.FileNamePDF + ".pdf";

            return virtualPath;
        }

        /// <summary>
        /// Gets the full URL to a TSB given the supplied base URL.
        /// </summary>
        /// <param name="baseUrl">The <see cref="string"/> base URL that includes the protocol, server and base path.</param>
        /// <returns>A <see cref="string"/> fully qualified URL to the PDF of the TSB.</returns>
        public string GetTsbPdfUrl(string baseUrl)
        {
            string virtualPath = this.GetTsbVirtualPath("");

            if (baseUrl.EndsWith("/") && virtualPath.StartsWith("/"))
            {
                virtualPath = virtualPath.Substring(1);
            }

            return baseUrl + virtualPath;
        }

        /// <summary>
        /// Gets the <see cref="int"/> count of TSBs that match the inputs (the count that the search with the same inputs would return.
        /// </summary>
        /// <param name="connectionString"><see cref="string"/> connection string.</param>
        /// <param name="aaiaList"><see cref="string"/> list of AAIA numbers separated by (typically a forward slash from the VinDecoder).</param>
        /// <param name="listDelimeter"><see cref="string"/> list.</param>
        /// <returns><see cref="int"/> TSB count for the inputs</returns>
        public static int GetTSBCountByVehicle(string connectionString, string aaiaList, string listDelimeter)
        {
            int recordCount = 0;

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connectionString))
            {
                dr.ProcedureName = "TSB_CountByVehicle";
                dr.AddNVarChar("AAIAXmlList", Formatting.DelimittedStringToXMLList(aaiaList, listDelimeter));

                dr.Execute();

                if (dr.Read())
                {
                    recordCount = dr.GetInt32("RecordCount");
                }
            }

            return recordCount;
        }

        /// <summary>
        /// Gets the <see cref="int"/> count of TSBs that match the inputs (the count that the search with the same inputs would return.)
        /// </summary>
        /// <param name="connectionString"><see cref="string"/> connection string</param>
        /// <param name="aaiaList"><see cref="string"/> aaia list</param>
        /// <param name="listDelimeter"><see cref="string"/> list delimeter</param>
        /// <returns>List of <see cref="TsbCategory"/> objects.</returns>
        public static List<TsbCategory> GetTSBCountByVehicleByCategory(string connectionString, string aaiaList, string listDelimeter)
        {
            List<TsbCategory> tsbCategories = new List<TsbCategory>();

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connectionString))
            {
                dr.ProcedureName = "TSB_CategoryCountByVehicle";
                dr.AddNVarChar("AAIAXmlList", Formatting.DelimittedStringToXMLList(aaiaList, listDelimeter));

                dr.Execute();

                while (dr.Read())
                {
                    tsbCategories.Add(new TsbCategory(dr.GetInt32("CanOBD2CategoryId"), dr.GetString("CanOBD2CategoryName"), dr.GetInt32("RecordCount")));
                }
            }

            return tsbCategories;
        }
    }
}