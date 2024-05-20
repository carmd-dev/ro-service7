using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;

namespace Innova.Vehicles
{
    /// <summary>
    /// Summary description for Recall.
    /// </summary>
    public class Recall
    {
        private Registry registry;
        private RuntimeInfo runtimeInfo;
        private int recordNumber;
        private string campaignNumber = "";
        private DateTime recallDate;
        private string defectDescription = "";
        private string defectDescription_es = "";
        private string defectDescription_fr = "";
        private string defectDescription_zh = "";
        private string defectConsequence = "";
        private string defectConsequence_es = "";
        private string defectConsequence_fr = "";
        private string defectConsequence_zh = "";
        private string defectCorrectiveAction = "";
        private string defectCorrectiveAction_es = "";
        private string defectCorrectiveAction_fr = "";
        private string defectCorrectiveAction_zh = "";

        /// <summary>
        /// Constructor for the <see cref="Recall"/> object.
        /// </summary>
        /// <param name="registry">The <see cref="Registry"/> currently in use.</param>
        /// <param name="recordNumber">The <see cref="int"/> record number.</param>
        /// <param name="campaignNumber">The <see cref="string"/> campaign number.</param>
        /// <param name="recallDate">The <see cref="DateTime"/> recall date.</param>
        /// <param name="defectDescription">The <see cref="string"/> description of the defect.</param>
        /// <param name="defectDescription_es">The <see cref="string"/> description of the defect in Spanish.</param>
        /// <param name="defectDescription_fr">The <see cref="string"/> description of the defect in French.</param>
        /// <param name="defectDescription_zh">The <see cref="string"/> description of the defect in Mandarin Chinese.</param>
        /// <param name="defectConsequence">The <see cref="string"/> consequence of the defect.</param>
        /// <param name="defectConsequence_es">The <see cref="string"/> consequence of the defect in Spanish.</param>
        /// <param name="defectConsequence_fr">The <see cref="string"/> consequence of the defect in French.</param>
        /// <param name="defectConsequence_zh">The <see cref="string"/> consequence of the defect in Mandarin Chinese.</param>
        /// <param name="defectCorrectiveAction">The <see cref="string"/> corrective action required to fix the defect.</param>
        /// <param name="defectCorrectiveAction_es">The <see cref="string"/> corrective action required to fix the defect in Spanish.</param>
        /// <param name="defectCorrectiveAction_fr">The <see cref="string"/> corrective action required to fix the defect in French.</param>
        /// <param name="defectCorrectiveAction_zh">The <see cref="string"/> corrective action required to fix the defect in Mandarin Chinese.</param>
        public Recall(Registry registry, int recordNumber, string campaignNumber, DateTime recallDate, string defectDescription, string defectDescription_es, string defectDescription_fr, string defectDescription_zh, string defectConsequence, string defectConsequence_es, string defectConsequence_fr, string defectConsequence_zh, string defectCorrectiveAction, string defectCorrectiveAction_es, string defectCorrectiveAction_fr, string defectCorrectiveAction_zh)
        {
            this.registry = registry;

            // First see if we have a RuntimeInfo object already in the Registry.
            this.runtimeInfo = this.registry.CustomObject1 as RuntimeInfo;

            // If not, then create an object and set the language to English.
            if (this.runtimeInfo == null)
            {
                this.runtimeInfo = new RuntimeInfo(this.registry);
                this.runtimeInfo.CurrentLanguage = Language.English;
                this.registry.CustomObject1 = this.runtimeInfo;
            }

            this.recordNumber = recordNumber;
            this.campaignNumber = campaignNumber;
            this.recallDate = recallDate;
            this.defectDescription = defectDescription;
            this.defectDescription_es = defectDescription_es;
            this.defectDescription_fr = defectDescription_fr;
            this.defectDescription_zh = defectDescription_zh;
            this.defectConsequence = defectConsequence;
            this.defectConsequence_es = defectConsequence_es;
            this.defectConsequence_fr = defectConsequence_fr;
            this.defectConsequence_zh = defectConsequence_zh;
            this.defectCorrectiveAction = defectCorrectiveAction;
            this.defectCorrectiveAction_es = defectCorrectiveAction_es;
            this.defectCorrectiveAction_fr = defectCorrectiveAction_fr;
            this.defectCorrectiveAction_zh = defectCorrectiveAction_zh;
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> record number.
        /// </summary>
        public int RecordNumber
        {
            get
            {
                return recordNumber;
            }
            set
            {
                recordNumber = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> campaign number.
        /// </summary>
        public string CampaignNumber
        {
            get
            {
                return campaignNumber;
            }
            set
            {
                campaignNumber = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> date of the recall.
        /// </summary>
        public DateTime RecallDate
        {
            get
            {
                return recallDate;
            }
            set
            {
                recallDate = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> description of the defect.
        /// </summary>
        public string DefectDescription
        {
            get
            {
                return this.defectDescription;
            }
            set
            {
                this.defectDescription = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> description of the defect in Spanish.
        /// </summary>
        public string DefectDescription_es
        {
            get
            {
                return this.defectDescription_es;
            }
            set
            {
                this.defectDescription_es = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> description of the defect in French.
        /// </summary>
        public string DefectDescription_fr
        {
            get
            {
                return this.defectDescription_fr;
            }
            set
            {
                this.defectDescription_fr = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> description of the defect in Mandarin Chinese.
        /// </summary>
        public string DefectDescription_zh
        {
            get
            {
                return this.defectDescription_zh;
            }
            set
            {
                this.defectDescription_zh = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> description of the defect in the language specified in the Registry.
        /// </summary>
        public string DefectDescription_Translated
        {
            get
            {
                return this.runtimeInfo.GetTranslatedValue(this.DefectDescription, this.DefectDescription_es, this.DefectDescription_fr, this.DefectDescription_zh);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> consequnce of the defect.
        /// </summary>
        public string DefectConsequence
        {
            get
            {
                return this.defectConsequence;
            }
            set
            {
                this.defectConsequence = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> consequnce of the defect in Spanish.
        /// </summary>
        public string DefectConsequence_es
        {
            get
            {
                return this.defectConsequence_es;
            }
            set
            {
                this.defectConsequence_es = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> consequnce of the defect in French.
        /// </summary>
        public string DefectConsequence_fr
        {
            get
            {
                return this.defectConsequence_fr;
            }
            set
            {
                this.defectConsequence_fr = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> consequnce of the defect in Mandarin Chinese.
        /// </summary>
        public string DefectConsequence_zh
        {
            get
            {
                return this.defectConsequence_zh;
            }
            set
            {
                this.defectConsequence_zh = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> consequence of the defect in the language specified in the Registry.
        /// </summary>
        public string DefectConsequence_Translated
        {
            get
            {
                return this.runtimeInfo.GetTranslatedValue(this.DefectConsequence, this.DefectConsequence_es, this.DefectConsequence_fr, this.DefectConsequence_zh);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> corrective action required to fix the defect.
        /// </summary>
        public string DefectCorrectiveAction
        {
            get
            {
                return this.defectCorrectiveAction;
            }
            set
            {
                this.defectCorrectiveAction = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> corrective action required to fix the defect in Spanish.
        /// </summary>
        public string DefectCorrectiveAction_es
        {
            get
            {
                return this.defectCorrectiveAction_es;
            }
            set
            {
                this.defectCorrectiveAction_es = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> corrective action required to fix the defect in French.
        /// </summary>
        public string DefectCorrectiveAction_fr
        {
            get
            {
                return this.defectCorrectiveAction_fr;
            }
            set
            {
                this.defectCorrectiveAction_fr = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> corrective action required to fix the defect in Mandarin Chinese.
        /// </summary>
        public string DefectCorrectiveAction_zh
        {
            get
            {
                return this.defectCorrectiveAction_zh;
            }
            set
            {
                this.defectCorrectiveAction_zh = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> corrective action required to fix the defect in the language specified in the Registry.
        /// </summary>
        public string DefectCorrectiveAction_Translated
        {
            get
            {
                return this.runtimeInfo.GetTranslatedValue(this.DefectCorrectiveAction, this.DefectCorrectiveAction_es, this.DefectCorrectiveAction_fr, this.DefectCorrectiveAction_zh);
            }
        }

        /// <summary>
        /// Gets a collection of <see cref="Recall"/> objects.
        /// </summary>
        /// <param name="registry">The <see cref="Registry"/> object currently in use.</param>
        /// <param name="year">The <see cref="int"/> year of the vehicle.</param>
        /// <param name="make">The <see cref="string"/> make of the vehicle.</param>
        /// <param name="model">The <see cref="string"/> model of the vehicle.</param>
        /// <param name="trimLevel">The <see cref="string"/> trim level of the vehicle.</param>
        /// <returns>A <see cref="RecallCollection"/> of <see cref="Recall"/> objects.</returns>
        public static RecallCollection Search(Registry registry, int year, string make, string model, string trimLevel)
        {
            RecallCollection recalls = new RecallCollection();

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(registry.ConnectionStringDefault))
            {
                dr.ProcedureName = "Recall_Search";
                dr.AddInt32("Year", year);
                dr.AddNVarChar("Make", make);
                dr.AddNVarChar("Model", model);
                dr.AddNVarChar("Trim", trimLevel);

                dr.Execute();

                while (dr.Read())
                {
                    DateTime recallDate = DateTime.MinValue;

                    if (dr.GetString("RecallDate") != "")
                    {
                        try
                        {
                            recallDate = DateTime.ParseExact(dr.GetString("RecallDate"), "yyyymmdd", System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat);
                        }
                        catch
                        {
                            // Do nothing. The recall date remains DateTime.MinValue.
                        }
                    }

                    Recall r = new Recall(
                        registry,
                        dr.GetInt32("RecordNumber"),
                        dr.GetString("CampaignNumber"),
                        recallDate,
                        dr.GetString("DefectDescription"),
                        dr.GetString("DefectDescription_es"),
                        dr.GetString("DefectDescription_fr"),
                        dr.GetString("DefectDescription_zh"),
                        dr.GetString("DefectConsequence"),
                        dr.GetString("DefectConsequence_es"),
                        dr.GetString("DefectConsequence_fr"),
                        dr.GetString("DefectConsequence_zh"),
                        dr.GetString("DefectCorrectiveAction"),
                        dr.GetString("DefectCorrectiveAction_es"),
                        dr.GetString("DefectCorrectiveAction_fr"),
                        dr.GetString("DefectCorrectiveAction_zh"));

                    recalls.Add(r);
                }
            }

            return recalls;
        }
    }
}