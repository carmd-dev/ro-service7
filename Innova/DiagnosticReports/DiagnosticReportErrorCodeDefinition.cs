using Innova.DtcCodes;
using Metafuse3;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Innova.DiagnosticReports
{
    //Add ErrorCode Definitions to ToolInfo - Added by INNOVA DEV TEAM 6/20/2017
    /// <summary>
    /// Contains the diagnostic report error code object with the associated data.
    /// </summary>
    public class DiagnosticReportErrorCodeDefinition : InnovaBusinessObjectBase
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
	    public List<DiagnosticReportResultErrorCodeDefinition> GetDefinitions(List<string> errorCodes, int? year, string make, string model, string transmission, string trimLevel)
        {
            var result = new List<DiagnosticReportResultErrorCodeDefinition>();

            DtcCodeCollection dtcCodeCollection = GetDtcCodes(errorCodes, year, make, model, transmission,
                trimLevel);

            foreach (DtcCode dtc in dtcCodeCollection)
            {
                if (result.Count(s => s.DTCCode.ErrorCode == dtc.ErrorCode) == 0)
                    result.Add(GetErrorCodeDefinition(dtc, make));
            }
            return result;
        }

        /// <summary>
        /// Gets a <see cref="DtcCodeCollection"/> of <see cref="DtcCode"/> objects that match this diagnostic report from the DtcCode table.
        /// </summary>
        /// <returns><see cref="DtcCodeCollection"/> of <see cref="DtcCode"/> objects that match this diagnostic report from the DtcCode table.</returns>
        private DtcCodeCollection GetDtcCodes(List<string> errorCodes,
            int? year, string make, string model, string transmission, string trimLevel)
        {
            DtcCodeCollection dtcCodes = new DtcCodeCollection(this.Registry);

            SqlProcedureCommand call = new SqlProcedureCommand();
            call.ProcedureName = "DTCCode_LoadBySearchWithLimitedInputs";
            call.AddInt32("Year", year);
            call.AddNVarChar("Make", make);
            call.AddNVarChar("Model", model);
            call.AddNVarChar("Transmission", transmission);
            call.AddNVarChar("TrimLevel", trimLevel);
            call.AddInt32("PageSize", int.MaxValue);
            call.AddInt32("CurrentPage", 1);
            call.AddNVarChar("ErrorCodes", GetErrorCodesAsXmlList(errorCodes));

            dtcCodes.Load(call, "DTCCodeId", true, true, true);

            SortDictionaryCollection sorts = new SortDictionaryCollection();

            sorts.Add(new SortDictionary("HasDefinedCount"));
            sorts.Add(new SortDictionary("FrequencyCount"));
            sorts.Add(new SortDictionary("UpdatedDateTimeUTC", SortDirection.Descending));

            dtcCodes.Sort(sorts);

            return dtcCodes;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dtcCode"></param>
        /// <param name="make"></param>
        private DiagnosticReportResultErrorCodeDefinition GetErrorCodeDefinition(DtcCode dtcCode, string make)
        {
            DiagnosticReportResultErrorCodeDefinition def = (DiagnosticReportResultErrorCodeDefinition)Registry.CreateInstance(typeof(DiagnosticReportResultErrorCodeDefinition));
            def.DiagnosticReportResultErrorCode = null;

            def.DTCCode = dtcCode;

            def.Title = dtcCode.Title;
            def.Title_es = dtcCode.Title_es;
            def.Title_fr = dtcCode.Title_fr;
            def.Title_zh = dtcCode.Title_zh;
            def.Conditions = dtcCode.Conditions;
            def.Conditions_es = dtcCode.Conditions_es;
            def.Conditions_fr = dtcCode.Conditions_fr;
            def.Conditions_zh = dtcCode.Conditions_zh;
            def.PossibleCauses = dtcCode.PossibleCauses;
            def.PossibleCauses_es = dtcCode.PossibleCauses_es;
            def.PossibleCauses_fr = dtcCode.PossibleCauses_fr;
            def.PossibleCauses_zh = dtcCode.PossibleCauses_zh;
            def.Trips = dtcCode.Trips;
            def.MessageIndicatorLampFile = dtcCode.MessageIndicatorLampFile;
            def.TransmissionControlIndicatorLampFile = dtcCode.TransmissionControlIndicatorLampFile;
            def.PassiveAntiTheftIndicatorLampFile = dtcCode.PassiveAntiTheftIndicatorLampFile;
            def.ServiceThrottleSoonIndicatorLampFile = dtcCode.ServiceThrottleSoonIndicatorLampFile;
            def.MonitorType = dtcCode.MonitorType;
            def.MonitorFile = dtcCode.MonitorFile;

            def.Model = GetDelimittedString(dtcCode.Models, "|");
            def.Make = GetDelimittedString(dtcCode.Makes, "|");
            def.Year = 0;
            if (dtcCode.Years.Count > 0)
            {
                def.Year = dtcCode.Years[0];
            }
            def.EngineType = GetDelimittedString(dtcCode.EngineTypes, "|");
            def.EngineVINCode = GetDelimittedString(dtcCode.EngineVINCodes, "|");
            def.TransmissionControlType = GetDelimittedString(dtcCode.TransmissionControlTypes, "|");

            this.SetLaymanTermProperties(dtcCode.ErrorCode, def, make);

            return def;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="errorCodes"></param>
        /// <returns></returns>
        private static string GetErrorCodesAsXmlList(List<string> errorCodes)
        {
            StringBuilder xml = new StringBuilder();

            xml.Append("<List>\n");

            foreach (string id in errorCodes)
            {
                xml.Append($"\t<Item value=\"{id}\"/>\n");
            }

            xml.Append("</List>");

            return xml.ToString();
        }

        private static string GetDelimittedString(IEnumerable list, string delimiter)
        {
            string s = "";

            foreach (object o in list)
            {
                string os = o.ToString();

                if (!string.IsNullOrEmpty(os))
                {
                    if (s.Length > 0)
                    {
                        s += delimiter;
                    }
                    s += os.Trim();
                }
            }
            return s;
        }

        /// <summary>
        /// This method will set the Title and Conditions to layman's terms if any exist.
        /// </summary>
        /// <param name="errorCode">The <see cref="string"/> error code.</param>
        /// <param name="def">The <see cref="DiagnosticReportResultErrorCodeDefinition"/> to be updated.</param>
        /// <param name="make"></param>
        private void SetLaymanTermProperties(string errorCode, DiagnosticReportResultErrorCodeDefinition def, string make)
        {
            DtcCodeLaymanTerm dtcCodeLaymanTerm = null;

            // Look up the layman's term object
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(ConnectionString))
            {
                dr.AddNVarChar("ErrorCode", errorCode);
                if (!string.IsNullOrWhiteSpace(make))
                {
                    dr.ProcedureName = "DTCCodeLaymanTerm_LoadByErrorCodeAndMake";
                    dr.AddNVarChar("Make", make);
                }
                else
                {
                    dr.ProcedureName = "DTCCodeLaymanTerm_LoadByErrorCode";
                }

                dr.Execute();

                if (dr.Read())
                {
                    dtcCodeLaymanTerm = (DtcCodeLaymanTerm)this.Registry.CreateInstance(typeof(DtcCodeLaymanTerm), dr.GetGuid("DtcCodeLaymanTermId"));
                    dtcCodeLaymanTerm.LoadPropertiesFromDataReader(dr, true);
                }
            }

            // If we found a layman's term object then update the definition.
            if (dtcCodeLaymanTerm == null) return;
            def.LaymansTermTitle = dtcCodeLaymanTerm.Title;
            def.LaymansTermTitle_es = dtcCodeLaymanTerm.Title_es;
            def.LaymansTermTitle_fr = dtcCodeLaymanTerm.Title_fr;
            def.LaymansTermTitle_zh = dtcCodeLaymanTerm.Title_zh;
            def.LaymansTermDescription = dtcCodeLaymanTerm.Description;
            def.LaymansTermDescription_es = dtcCodeLaymanTerm.Description_es;
            def.LaymansTermDescription_fr = dtcCodeLaymanTerm.Description_fr;
            def.LaymansTermDescription_zh = dtcCodeLaymanTerm.Description_zh;
            def.LaymansTermConditions = dtcCodeLaymanTerm.Description;
            def.LaymansTermConditions_es = dtcCodeLaymanTerm.Description_es;
            def.LaymansTermConditions_fr = dtcCodeLaymanTerm.Description_fr;
            def.LaymansTermConditions_zh = dtcCodeLaymanTerm.Description_zh;
            def.LaymansTermSeverityLevel = dtcCodeLaymanTerm.SeverityLevel;
            def.LaymansTermEffectOnVehicle = dtcCodeLaymanTerm.EffectOnVehicle;
            def.LaymansTermEffectOnVehicle_es = dtcCodeLaymanTerm.EffectOnVehicle_es;
            def.LaymansTermEffectOnVehicle_fr = dtcCodeLaymanTerm.EffectOnVehicle_fr;
            def.LaymansTermEffectOnVehicle_zh = dtcCodeLaymanTerm.EffectOnVehicle_zh;
            def.LaymansTermResponsibleComponentOrSystem = dtcCodeLaymanTerm.ResponsibleComponentOrSystem;
            def.LaymansTermResponsibleComponentOrSystem_es = dtcCodeLaymanTerm.ResponsibleComponentOrSystem_es;
            def.LaymansTermResponsibleComponentOrSystem_fr = dtcCodeLaymanTerm.ResponsibleComponentOrSystem_fr;
            def.LaymansTermResponsibleComponentOrSystem_zh = dtcCodeLaymanTerm.ResponsibleComponentOrSystem_zh;
            def.LaymansTermWhyItsImportant = dtcCodeLaymanTerm.WhyItsImportant;
            def.LaymansTermWhyItsImportant_es = dtcCodeLaymanTerm.WhyItsImportant_es;
            def.LaymansTermWhyItsImportant_fr = dtcCodeLaymanTerm.WhyItsImportant_fr;
            def.LaymansTermWhyItsImportant_zh = dtcCodeLaymanTerm.WhyItsImportant_zh;
            def.LaymansTermSeverityLevelDefinition = dtcCodeLaymanTerm.SeverityDefinition;
            def.LaymansTermSeverityLevelDefinition_es = dtcCodeLaymanTerm.SeverityDefinition_es;
            def.LaymansTermSeverityLevelDefinition_fr = dtcCodeLaymanTerm.SeverityDefinition_fr;
            def.LaymansTermSeverityLevelDefinition_zh = dtcCodeLaymanTerm.SeverityDefinition_zh;
        }
    }
}