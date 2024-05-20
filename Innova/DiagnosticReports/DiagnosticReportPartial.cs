using Innova.DtcCodes;
using Innova.Fixes;
using Innova.HighlightDtcsList;
using Innova.Vehicles;
using Metafuse3;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using Metafuse3.NullableTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Innova.DiagnosticReports
{
    public partial class DiagnosticReport
    {
        //New properties for OBDFIX - Nam added 1/10/2017
        private Guid? parentDiagnosticReportIdGuid = null;

        private string manualRawFreezeFrameDataString = "";
        private bool additionalHelpRequired = false;
        private bool? isNotifiedRequester = false;
        private NullableDateTime notifiedRequesterDateTimeUTC = NullableDateTime.Null;
        private string notifiedRequesterVia = "";
        private string note = "";
        //-----------------------------------------------

        #region Object Properties

        /// <summary>
        ///
        /// </summary>
        public Guid? ParentDiagnosticReportIdGuid
        {
            get
            {
                this.EnsureLoaded();
                return this.parentDiagnosticReportIdGuid;
            }
            set
            {
                this.EnsureLoaded();
                if (this.parentDiagnosticReportIdGuid != value)
                {
                    this.IsObjectDirty = true;
                    this.parentDiagnosticReportIdGuid = value;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public string ManualRawFreezeFrameDataString
        {
            get
            {
                this.EnsureLoaded();
                return this.manualRawFreezeFrameDataString;
            }
            set
            {
                this.EnsureLoaded();
                if (this.manualRawFreezeFrameDataString != value)
                {
                    this.IsObjectDirty = true;
                    this.manualRawFreezeFrameDataString = value;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public bool AdditionalHelpRequired
        {
            get
            {
                this.EnsureLoaded();
                return this.additionalHelpRequired;
            }
            set
            {
                this.EnsureLoaded();
                if (this.additionalHelpRequired != value)
                {
                    this.IsObjectDirty = true;
                    this.additionalHelpRequired = value;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public bool? IsNotifiedRequester
        {
            get
            {
                this.EnsureLoaded();
                return this.isNotifiedRequester;
            }
            set
            {
                this.EnsureLoaded();
                if (this.isNotifiedRequester != value)
                {
                    this.IsObjectDirty = true;
                    this.isNotifiedRequester = value;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public string NotifiedRequesterVia
        {
            get
            {
                this.EnsureLoaded();
                return this.notifiedRequesterVia;
            }
            set
            {
                this.EnsureLoaded();
                if (this.notifiedRequesterVia != value)
                {
                    this.IsObjectDirty = true;
                    this.notifiedRequesterVia = value;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public string Note
        {
            get
            {
                this.EnsureLoaded();
                return this.note;
            }
            set
            {
                this.EnsureLoaded();
                if (this.note != value)
                {
                    this.IsObjectDirty = true;
                    this.note = value;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public NullableDateTime NotifiedRequesterDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.notifiedRequesterDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (this.notifiedRequesterDateTimeUTC != value)
                {
                    this.IsObjectDirty = true;
                    this.notifiedRequesterDateTimeUTC = value;
                }
            }
        }

        public AbsDtcInfo AbsDtcInfo
        {
            get
            {
                if (OemDtcInfo != null) return OemDtcInfo.AbsDtcInfo;
                return null;
            }
        }

        #endregion Object Properties

        /// <summary>
        /// Create Diagnostic Report Result
        /// </summary>
        public void CreateDiagnosticReportResultFixForSymptoms()
        {
            bool fixFound = false;

            if (this.SymptomGuids != null && this.SymptomGuids.Any())
            {
                foreach (var symptomId in this.SymptomGuids)
                {
                    fixFound = this.CreateDiagnosticReportResultForSymptoms(symptomId,
                        DiagnosticReportErrorCodeSystemType.PowertrainObd2, false, false);

                    if (!this.pwrFixFoundAfterLastFixLookup)
                    {
                        this.pwrFixFoundAfterLastFixLookup = fixFound;
                    }
                }
            }

            this.SetFixStatusesForSymptoms(DiagnosticReportErrorCodeSystemType.PowertrainObd2);

            if (fixFound && this.FixProvidedDateTimeUTC.IsNull)
            {
                this.FixProvidedDateTimeUTC = DateTime.UtcNow;
            }
        }

        /// <summary>
        /// Creates (or updates if one exists) a <see cref="DiagnosticReportResult"/> for this report(Symptoms)
        /// </summary>
        private bool CreateDiagnosticReportResultForSymptoms(Guid? symptomId,
            DiagnosticReportErrorCodeSystemType diagnosticReportErrorCodeSystemType,
            bool logDiscrepancies, bool onlyProcessFixes)
        {
            bool newFixFound = false;
            DiagnosticReportFixStatus oldFixStatus = DiagnosticReportFixStatus.FixNotFound;

            if (diagnosticReportErrorCodeSystemType != DiagnosticReportErrorCodeSystemType.Enhanced)
            {
                if (symptomId == null || symptomId == Guid.Empty)
                {
                    return false;
                }

                FixCollection fixes = new FixCollection(this.Registry);

                if (this.DiagnosticReportResult == null)
                {
                    //creates a new diagnostic report result
                    this.DiagnosticReportResult = (DiagnosticReportResult)Registry.CreateInstance(typeof(DiagnosticReportResult));
                    this.DiagnosticReportResult.DiagnosticReport = this;
                }

                switch (diagnosticReportErrorCodeSystemType)
                {
                    case DiagnosticReportErrorCodeSystemType.PowertrainObd2:

                        fixes = GetFixesSortedForSymptom(symptomId.Value, logDiscrepancies,
                            onlyProcessFixes, diagnosticReportErrorCodeSystemType);

                        oldFixStatus = this.PwrDiagnosticReportFixStatus;
                        break;
                }

                if (!onlyProcessFixes)
                {
                    /*************************************************************
					* Remove the existing fixes for the given DTC and system type
					*************************************************************/

                    this.DiagnosticReportResult.DiagnosticReportResultFixes.LoadRelation(
                        this.DiagnosticReportResult.DiagnosticReportResultFixes.RelationDiagnosticReportResultFixeParts);

                    //ToolDB_
                    this.DiagnosticReportResult.DiagnosticReportResultFixes.LoadRelation(
                        this.DiagnosticReportResult.DiagnosticReportResultFixes.RelationDiagnosticReportResultFixTools);
                    //ToolDB_

                    this.DiagnosticReportResult.DiagnosticReportResultFixes.Clear();

                    /*************************************************************
					* Add the new fixes
					*************************************************************/
                    for (int i = 0; i < fixes.Count; i++)
                    {
                        Fix f = fixes[i];

                        DiagnosticReportResultFix drrFix = f.ToDiagnosticReportResultFix(diagnosticReportErrorCodeSystemType, DiagnosticReportResult);
                        drrFix.PrimaryErrorCode = string.Empty; //No DTC here, just for Symptom

                        drrFix.DiagnosticReportErrorCodeSystemType = diagnosticReportErrorCodeSystemType;
                        drrFix.SortOrder = i;
                        drrFix.SortDescription = "";
                        this.DiagnosticReportResult.DiagnosticReportResultFixes.Add(drrFix);
                    }

                    //resort the entire list now
                    SortDictionaryCollection sorts = new SortDictionaryCollection();
                    sorts.Add(new SortDictionary("DiagnosticReportErrorCodeSystemType"));
                    sorts.Add(new SortDictionary("SortOrder"));
                    this.DiagnosticReportResult.DiagnosticReportResultFixes.Sort(sorts);
                }
            }

            if (!onlyProcessFixes)
            {
                this.SetFixStatusesForSymptoms(diagnosticReportErrorCodeSystemType);
                DiagnosticReportFixStatus newFixStatus = this.GetFixStatus(diagnosticReportErrorCodeSystemType);

                // See if we went from something else to FixFound
                if (newFixStatus == DiagnosticReportFixStatus.FixFound && newFixStatus != oldFixStatus)
                {
                    newFixFound = true;
                }
            }

            return newFixFound;
        }

        /// <summary>
        /// Sets the <see cref="DiagnosticReportFixStatus"/> based on the current values of <see cref="DiagnosticReport.ToolMilStatus"/>, <see cref="DiagnosticReport.ToolLEDStatus"/>, <see cref="string"/> DiagnosticReport.PrimaryErrorCode and <see cref="int"/> DiagnosticReportResult.DiagnosticReportResultFixes.Count on the report.
        /// </summary>
        public void SetFixStatusesForSymptoms(DiagnosticReportErrorCodeSystemType systemType)
        {
            bool isPwrLookForFix = this.SymptomGuids != null && this.SymptomGuids.Any();

            DiagnosticReportFixStatus fixStatus = DiagnosticReportFixStatus.FixNotFound;
            bool isFixFeedbackRequired = false;

            if (systemType == DiagnosticReportErrorCodeSystemType.PowertrainObd2)
            {
                if (!isPwrLookForFix)
                {
                    fixStatus = DiagnosticReportFixStatus.FixNotNeeded;
                }
                else
                {
                    if (this.DiagnosticReportResult.GetFixesBySystemType(systemType).Count == 0)
                    {
                        fixStatus = DiagnosticReportFixStatus.FixNotFound;
                        isFixFeedbackRequired = true;
                    }
                    else
                    {
                        fixStatus = DiagnosticReportFixStatus.FixFound;
                    }
                }
            }
            switch (systemType)
            {
                case DiagnosticReportErrorCodeSystemType.PowertrainObd2:
                    this.PwrDiagnosticReportFixStatus = fixStatus;
                    this.IsPwrObd2FixFeedbackRequired = isFixFeedbackRequired;
                    if (this.IsObjectCreated)
                    {
                        this.PwrDiagnosticReportFixStatusWhenCreated = this.PwrDiagnosticReportFixStatus;
                    }
                    break;
            }
        }

        /// <summary>
        /// Find Fixes for symptom
        /// </summary>
        /// <param name="symptomId"></param>
        /// <param name="logDiscrepancies"></param>
        /// <param name="onlyProcessFixes"></param>
        /// <param name="diagnosticReportErrorCodeSystemType"></param>
        /// <returns></returns>
        private FixCollection GetFixesSortedForSymptom(Guid symptomId,
       bool logDiscrepancies, bool onlyProcessFixes,
       DiagnosticReportErrorCodeSystemType diagnosticReportErrorCodeSystemType)
        {
            FixCollection fixesPolk = new FixCollection(this.Registry);
            SqlProcedureCommand call = new SqlProcedureCommand();
            if (this.Vehicle.PolkVehicleYMME != null)
            {
                call.ProcedureName = "Fix_LoadByDiagnosticReportBySymptom";

                call.AddGuid("SymptomId", symptomId);
                call.AddInt32("Year", this.Vehicle.PolkVehicleYMME.Year);
                call.AddNVarChar("Make", this.Vehicle.PolkVehicleYMME.Make);
                call.AddNVarChar("Model", this.Vehicle.PolkVehicleYMME.Model);
                call.AddNVarChar("TrimLevel", this.Vehicle.PolkVehicleYMME.Trim);
                call.AddNVarChar("Transmission", this.Vehicle.PolkVehicleYMME.Transmission);
                call.AddNVarChar("EngineVINCode", this.Vehicle.PolkVehicleYMME.EngineVinCode);
                call.AddNVarChar("EngineType", this.Vehicle.PolkVehicleYMME.EngineType);
                call.AddInt32("Market", (int)this.RuntimeInfo.CurrentMarket);

                fixesPolk.Load(call, "FixId", true, true);
                fixesPolk.SetIsFromPolkMatch(true);
            }

            FixCollection fixesVinPower = new FixCollection(this.Registry);

            call = new SqlProcedureCommand();
            call.ProcedureName = "Fix_LoadByDiagnosticReportUsingVinPowerBySymptom";
            call.AddInt32("Year", this.Vehicle.VPYear);
            call.AddNVarChar("Make", this.Vehicle.VPMake);
            call.AddNVarChar("Model", this.Vehicle.VPModel);
            call.AddNVarChar("TrimLevel", this.Vehicle.VPTrimLevel);
            call.AddNVarChar("Transmission", this.Vehicle.TransmissionControlType);
            call.AddGuid("SymptomId", symptomId);
            call.AddNVarChar("EngineVINCode", this.Vehicle.VPEngineVINCode);
            call.AddNVarChar("EngineType", this.Vehicle.VPEngineType);
            call.AddInt32("Market", (int)this.RuntimeInfo.CurrentMarket);

            fixesVinPower.Load(call, "FixId", true, true);
            fixesVinPower.SetIsFromVinPowerMatch(true);

            FixCollection fixes = new FixCollection(this.Registry);

            foreach (Fix f in fixesVinPower)
            {
                Fix polkFix = (Fix)fixesPolk.FindByProperty("Id", f.Id);

                if (polkFix == null)
                {
                    if (logDiscrepancies && this.Vehicle.PolkVehicleYMME != null)
                    {
                        FixPolkVehicleDiscrepancy.AddDiscrepancy(this.Registry, f, this.Vehicle.PolkVehicleYMME, true, false);
                    }
                }
                else
                {
                    f.IsFromPolkMatch = true;
                }

                fixes.Add(f);
            }

            fixes.ClearLookupTables();

            foreach (Fix f in fixesPolk)
            {
                Fix vinPowerFix = (Fix)fixesVinPower.FindByProperty("Id", f.Id);

                if (vinPowerFix == null)
                {
                    if (logDiscrepancies && this.Vehicle.PolkVehicleYMME != null)
                    {
                        FixPolkVehicleDiscrepancy.AddDiscrepancy(this.Registry, f, this.Vehicle.PolkVehicleYMME, false, true);
                    }
                }
                else
                {
                    f.IsFromVinPowerMatch = true;
                }

                if (fixes.FindByProperty("Id", f.Id) == null)
                {
                    fixes.Add(f);
                    fixes.ClearLookupTables();
                }
            }

            if (!onlyProcessFixes)
            {
                //load the relations
                this.SortSymptomFixes(fixes, symptomId, new StringCollection(), diagnosticReportErrorCodeSystemType);
            }

            return fixes;
        }

        private void SortSymptomFixes(FixCollection fixesToSort, Guid symptomId, StringCollection secondaryDtcs,
         DiagnosticReportErrorCodeSystemType diagnosticReportErrorCodeSystemType)
        {
            //foreach fix calculate the total cost
            foreach (Fix f in fixesToSort)
            {
                if (PerformFreezeFrameSorting)
                {
                    f.FreezeFrameMatches = f.FixName.GetFreezeFrameMatchCount(this);
                }

                FixSymptom fixSymptom = f.GetFixSymptomMatchingSymptom(symptomId);

                //shouldn't be null, but if it is, we don't want to blow up
                if (fixSymptom != null)
                {
                    {
                        //both are zero
                        f.SetDiagnosticReportIsExactMatch(true);
                        f.SetDiagnosticReportSecondaryCodeAssignmentMatches(0);
                    }
                }
            }
            //finally sort the fixes

            var sorts = new SortDictionaryCollection();
            sorts.Add(new SortDictionary("DiagnosticReportIsExactMatch", SortDirection.Descending));
            sorts.Add(new SortDictionary("DiagnosticReportSecondaryCodeAssignmentMatches", SortDirection.Descending));
            sorts.Add(new SortDictionary("FrequencyCount", SortDirection.Descending));
            sorts.Add(new SortDictionary("TotalCost", SortDirection.Descending));
            sorts.Add(new SortDictionary("Name"));
            sorts.Add(new SortDictionary("CreatedDateTimeUTC", SortDirection.Descending));

            fixesToSort.Sort(sorts);

            if (DiagnosticReport.PerformFreezeFrameSorting)
            {
                this.SaveFixSortPriority(fixesToSort, "Normal" + DiagnosticReport.ReportType,
                    diagnosticReportErrorCodeSystemType);

                sorts = new SortDictionaryCollection();
                sorts.Add(new SortDictionary("FreezeFrameMatches", SortDirection.Descending));
                sorts.Add(new SortDictionary("DiagnosticReportIsExactMatch", SortDirection.Descending));
                sorts.Add(new SortDictionary("DiagnosticReportSecondaryCodeAssignmentMatches",
                    SortDirection.Descending));
                sorts.Add(new SortDictionary("FrequencyCount", SortDirection.Descending));
                sorts.Add(new SortDictionary("TotalCost", SortDirection.Descending));
                sorts.Add(new SortDictionary("Name"));
                sorts.Add(new SortDictionary("CreatedDateTimeUTC", SortDirection.Descending));

                fixesToSort.Sort(sorts);
                this.SaveFixSortPriority(fixesToSort, "FreezeFrameFirst" + DiagnosticReport.ReportType,
                    diagnosticReportErrorCodeSystemType);

                sorts = new SortDictionaryCollection();
                sorts.Add(new SortDictionary("DiagnosticReportIsExactMatch", SortDirection.Descending));
                sorts.Add(new SortDictionary("Name"));
                sorts.Add(new SortDictionary("CreatedDateTimeUTC", SortDirection.Descending));

                fixesToSort.Sort(sorts);
                this.SaveFixSortPriority(fixesToSort, "PrimaryDTCOnly" + DiagnosticReport.ReportType,
                    diagnosticReportErrorCodeSystemType);
            }
        }

        //#BCUCodes
        //Get BCU Codes Fixes
        /// <summary>
        ///
        /// </summary>
        /// <param name="storedCodes"></param>
        /// <param name="logDiscrepancies"></param>
        /// <param name="onlyProcessFixes"></param>
        /// <param name="diagnosticReportErrorCodeSystemType"></param>
        /// <returns></returns>
        private FixCollection GetFixesSorted(IEnumerable<string> storedCodes, bool logDiscrepancies, bool onlyProcessFixes,
            DiagnosticReportErrorCodeSystemType diagnosticReportErrorCodeSystemType)
        {
            FixCollection fixesPolk = new FixCollection(this.Registry);
            SqlProcedureCommand call = new SqlProcedureCommand();

            //#Sprint23
            storedCodes = GetFirstPartErrorCodes(storedCodes);
            //#Sprint23

            if (this.Vehicle.PolkVehicleYMME != null)
            {
                call.ProcedureName = "BCUFix_LoadByDiagnosticReport";

                if (storedCodes != null && storedCodes.Any())
                {
                    storedCodes = storedCodes.Distinct().ToArray();
                    call.AddNVarChar("DTCXmlList", Metafuse3.Xml.XmlList.ToXml(storedCodes));
                }

                call.AddInt32("Year", this.Vehicle.PolkVehicleYMME.Year);
                call.AddNVarChar("Make", this.Vehicle.PolkVehicleYMME.Make);
                call.AddNVarChar("Model", this.Vehicle.PolkVehicleYMME.Model);
                call.AddNVarChar("TrimLevel", this.Vehicle.PolkVehicleYMME.Trim);
                call.AddNVarChar("Transmission", this.Vehicle.PolkVehicleYMME.Transmission);
                call.AddNVarChar("EngineVINCode", this.Vehicle.PolkVehicleYMME.EngineVinCode);
                call.AddNVarChar("EngineType", this.Vehicle.PolkVehicleYMME.EngineType);

                call.AddInt32("Market", (int)this.RuntimeInfo.CurrentMarket);

                //#NewFixLogic
                call.AddNVarChar("ManufacturerName", this.Vehicle.PolkVehicleYMME.Manufacturer);
                call.AddNVarChar("PolkVehicleYmmeCountryId", this.Vehicle.PolkVehicleYMME.CountryID);

                var diagnosticReportId = this.Id;
                if (diagnosticReportId != Guid.Empty)
                {
                    call.AddGuid("DiagnosticReportId", diagnosticReportId);
                }

                fixesPolk.Load(call, "FixId", true, true);
                fixesPolk.SetIsFromPolkMatch(true);
            }

            FixCollection fixesVinPower = new FixCollection(this.Registry);

            FixCollection fixes = new FixCollection(this.Registry);

            foreach (Fix f in fixesVinPower)
            {
                Fix polkFix = (Fix)fixesPolk.FindByProperty("Id", f.Id);

                if (polkFix == null)
                {
                    if (logDiscrepancies && this.Vehicle.PolkVehicleYMME != null)
                    {
                        FixPolkVehicleDiscrepancy.AddDiscrepancy(this.Registry, f, this.Vehicle.PolkVehicleYMME, true, false);
                    }
                }
                else
                {
                    f.IsFromPolkMatch = true;
                }

                fixes.Add(f);
            }

            fixes.ClearLookupTables();

            foreach (Fix f in fixesPolk)
            {
                Fix vinPowerFix = (Fix)fixesVinPower.FindByProperty("Id", f.Id);

                if (vinPowerFix == null)
                {
                    if (logDiscrepancies && this.Vehicle.PolkVehicleYMME != null)
                    {
                        FixPolkVehicleDiscrepancy.AddDiscrepancy(this.Registry, f, this.Vehicle.PolkVehicleYMME, false, true);
                    }
                }
                else
                {
                    f.IsFromVinPowerMatch = true;
                }

                if (fixes.FindByProperty("Id", f.Id) == null)
                {
                    fixes.Add(f);
                    fixes.ClearLookupTables();
                }
            }

            if (!onlyProcessFixes)
            {
                //load the relations
                //this.SortFixes(fixes, primaryErrorCode, secondaryDtcs, diagnosticReportErrorCodeSystemType);
            }

            return fixes;
        }

        //#BCUCodes
        /// <summary>
        ///
        /// </summary>
        /// <param name="storedCodeList"></param>
        /// <param name="diagnosticReportErrorCodeSystemType"></param>
        /// <param name="updateErrorCodes"></param>
        /// <param name="logDiscrepancies"></param>
        /// <param name="onlyProcessFixes"></param>
        /// <returns></returns>
        private bool CreateDiagnosticReportResult(IEnumerable<string> storedCodeList,
            DiagnosticReportErrorCodeSystemType diagnosticReportErrorCodeSystemType,
            bool updateErrorCodes, bool logDiscrepancies, bool onlyProcessFixes)
        {
            bool newFixFound = false;
            DiagnosticReportFixStatus oldFixStatus = DiagnosticReportFixStatus.FixNotFound;

            //#LEDMILUpdate
            bool isFixNotNeeded = false;

            if (diagnosticReportErrorCodeSystemType != DiagnosticReportErrorCodeSystemType.Enhanced)
            {
                if (storedCodeList == null || !storedCodeList.Any())
                {
                    return false;
                }

                FixCollection fixes = new FixCollection(this.Registry);

                if (this.DiagnosticReportResult == null)
                {
                    //creates a new diagnostic report result
                    this.DiagnosticReportResult = (DiagnosticReportResult)Registry.CreateInstance(typeof(DiagnosticReportResult));
                    this.DiagnosticReportResult.DiagnosticReport = this;
                }

                switch (diagnosticReportErrorCodeSystemType)
                {
                    case DiagnosticReportErrorCodeSystemType.ABS:
                        if (this.AbsDtcInfo != null) //For Servicev7/AZV2/3
                        {
                            var newStoredCodeList = new List<string>();
                            foreach (var code in storedCodeList)
                            {
                                if (this.AbsDtcInfo.RecommendRepairDtcs.Contains(code.ToUpper()))
                                {
                                    newStoredCodeList.Add(code);
                                }
                            }
                            if (newStoredCodeList.Any())
                            {
                                fixes = this.GetFixesSorted(newStoredCodeList, logDiscrepancies, onlyProcessFixes, diagnosticReportErrorCodeSystemType);
                            }
                            else
                            {
                                this.AbsDiagnosticReportFixStatus = DiagnosticReportFixStatus.FixNotNeeded;
                                isFixNotNeeded = true;
                            }
                        }
                        else//For ServiceV6
                        {
                            fixes = this.GetFixesSorted(storedCodeList, logDiscrepancies, onlyProcessFixes, diagnosticReportErrorCodeSystemType);
                        }

                        oldFixStatus = this.AbsDiagnosticReportFixStatus;
                        break;

                    //#SP36
                    case DiagnosticReportErrorCodeSystemType.SRS:
                        if (this.OemDtcInfo != null && this.OemDtcInfo.SrsRecommendRepairDtcs != null) //For Servicev7/AZV2/3
                        {
                            var newStoredCodeList = new List<string>();
                            foreach (var code in storedCodeList)
                            {
                                if (this.OemDtcInfo.SrsRecommendRepairDtcs.Contains(code.ToUpper()))
                                {
                                    newStoredCodeList.Add(code);
                                }
                            }
                            if (newStoredCodeList.Any())
                            {
                                fixes = this.GetFixesSorted(newStoredCodeList, logDiscrepancies, onlyProcessFixes, diagnosticReportErrorCodeSystemType);
                            }
                            else
                            {
                                this.SrsDiagnosticReportFixStatus = DiagnosticReportFixStatus.FixNotNeeded;
                                isFixNotNeeded = true;
                            }
                        }
                        else//For ServiceV6
                        {
                            fixes = this.GetFixesSorted(storedCodeList, logDiscrepancies, onlyProcessFixes, diagnosticReportErrorCodeSystemType);
                        }

                        oldFixStatus = this.SrsDiagnosticReportFixStatus;
                        break;
                }

                if (!onlyProcessFixes)
                {
                    /*************************************************************
					* Remove the existing fixes for the given DTC and system type
					*************************************************************/

                    this.DiagnosticReportResult.DiagnosticReportResultFixes.LoadRelation(this.DiagnosticReportResult
                        .DiagnosticReportResultFixes.RelationDiagnosticReportResultFixeParts);

                    //ToolDB_
                    this.DiagnosticReportResult.DiagnosticReportResultFixes.LoadRelation(this.DiagnosticReportResult
                        .DiagnosticReportResultFixes.RelationDiagnosticReportResultFixTools);
                    //ToolDB_

                    /*************************************************************
					* Add the new fixes for all stored DTCs
					*************************************************************/

                    //#AddNew
                    storedCodeList = storedCodeList.OrderBy(s => s);
                    var comboSortedDtcs = string.Join(",", GetFirstPartErrorCodes(storedCodeList));
                    for (int i = 0; i < fixes.Count; i++)
                    {
                        Fix f = fixes[i];
                        //Set result fix for combo DTCs first
                        if (f.FixDtcComboList.Any(fdtc => string.Equals(fdtc, comboSortedDtcs, StringComparison.OrdinalIgnoreCase)))
                        {
                            foreach (var dtc in storedCodeList)
                            {
                                DiagnosticReportResultFix drrFix =
                                    f.ToDiagnosticReportResultFix(diagnosticReportErrorCodeSystemType,
                                        DiagnosticReportResult);
                                drrFix.PrimaryErrorCode = dtc;

                                drrFix.DiagnosticReportErrorCodeSystemType = diagnosticReportErrorCodeSystemType;
                                drrFix.SortOrder = i;
                                drrFix.SortDescription = "";
                                this.DiagnosticReportResult.DiagnosticReportResultFixes.Add(drrFix);
                            }
                        }
                        else //If combo DTCs not match
                        {
                            //Then set result fix for single DTC if any
                            foreach (var dtc in storedCodeList)
                            {
                                if (f.FixDtcComboList.Any(fdtc => string.Equals(fdtc, GetFirstPartErrorCode(dtc), StringComparison.OrdinalIgnoreCase)))
                                {
                                    DiagnosticReportResultFix drrFix =
                                        f.ToDiagnosticReportResultFix(diagnosticReportErrorCodeSystemType,
                                            DiagnosticReportResult);
                                    drrFix.PrimaryErrorCode = dtc;

                                    drrFix.DiagnosticReportErrorCodeSystemType = diagnosticReportErrorCodeSystemType;
                                    drrFix.SortOrder = i;
                                    drrFix.SortDescription = "";
                                    this.DiagnosticReportResult.DiagnosticReportResultFixes.Add(drrFix);
                                }
                            }
                        }
                    }
                    //#AddNew

                    //resort the entire list now
                    SortDictionaryCollection sorts = new SortDictionaryCollection();
                    sorts.Add(new SortDictionary("DiagnosticReportErrorCodeSystemType"));
                    sorts.Add(new SortDictionary("SortOrder"));
                    this.DiagnosticReportResult.DiagnosticReportResultFixes.Sort(sorts);
                }
            }

            if (!onlyProcessFixes)
            {
                //#LEDMILUpdate
                this.SetFixStatuses(diagnosticReportErrorCodeSystemType, isFixNotNeeded);

                DiagnosticReportFixStatus newFixStatus = this.GetFixStatus(diagnosticReportErrorCodeSystemType);

                // See if we went from something else to FixFound
                if (newFixStatus == DiagnosticReportFixStatus.FixFound && newFixStatus != oldFixStatus)
                {
                    newFixFound = true;
                }

                if (!onlyProcessFixes && updateErrorCodes)
                {
                    this.CreateDiagnosticReportResultErrorCodes(diagnosticReportErrorCodeSystemType);
                }
            }

            return newFixFound;
        }

        //#Sprint12 - Update DTC Definition Order by Chilton>Innova>ETI
        //Re-AddChiltonDTC - Replace method
        //Removed Chilton DTCs on 2018-10-15 2:47 PM
        //#ChiltonDTC_reintegrate
        private DiagnosticReportResultErrorCode GetDiagnosticReportResultErrorCode(string errorCode, DiagnosticReportErrorCodeType diagnosticReportErrorCodeType, string errorCodeType, DtcCodeCollection dtcCodes, VehicleTypeCodeAssignmentCollection vehicleCodeAssignments, DtcMasterCodeListCollection dtcMasterCodes)
        {
            //create new diagnostic report result error code
            DiagnosticReportResultErrorCode drrErrorCode = (DiagnosticReportResultErrorCode)Registry.CreateInstance(typeof(DiagnosticReportResultErrorCode)); ;

            drrErrorCode.DiagnosticReportResult = this.DiagnosticReportResult;

            var foundErrorCodes = false;

            if (!foundErrorCodes)
            {
                //INNOVA DTC
                DtcCode dtcCode = null;

                foreach (DtcCode c in dtcCodes)
                {
                    if (string.Equals(errorCode, c.ErrorCode, StringComparison.OrdinalIgnoreCase))
                    {
                        dtcCode = c;
                        break;
                    }
                }

                //if we found an innova specific code then we add the diagnostic report for it.
                if (dtcCode != null)
                {
                    //add the master item to the list
                    drrErrorCode.AddDiagnosticReportResultErrorCodeDefinition(dtcCode);
                    foundErrorCodes = true;
                }
            }

            //Chilton
            if (!foundErrorCodes)
            {
                if (vehicleCodeAssignments != null && vehicleCodeAssignments.Count > 0) //#DisableChiltonTDC
                {
                    ArrayList errorCodeAssignments = null;
                    foreach (VehicleTypeCodeAssignment vc in vehicleCodeAssignments)
                    {
                        if (string.Equals(errorCode, vc.ErrorCode, StringComparison.OrdinalIgnoreCase))
                        {
                            if (errorCodeAssignments == null)
                            {
                                errorCodeAssignments = new ArrayList();
                            }
                            errorCodeAssignments.Add(vc);
                        }
                    }

                    if (errorCodeAssignments != null && errorCodeAssignments.Count > 0)
                    {
                        //add the definitions to the error code
                        drrErrorCode.AddDiagnosticReportResultErrorCodeDefinition(errorCodeAssignments);
                        foundErrorCodes = true;
                    }
                }
            }

            //Then ETI DTC
            if (!foundErrorCodes)
            {
                //see if we find one in the master list for this code
                DtcMasterCodeList dtcMasterCode = null;

                //use case insensitive comparison
                foreach (DtcMasterCodeList c in dtcMasterCodes)
                {
                    if (string.Equals(errorCode, c.ErrorCode, StringComparison.OrdinalIgnoreCase))
                    {
                        dtcMasterCode = c;
                        break;
                    }
                }

                if (dtcMasterCode != null)
                {
                    //add the definitions to the error code
                    drrErrorCode.AddDiagnosticReportResultErrorCodeDefinition(dtcMasterCode);
                }
            }

            //set the type of error code
            drrErrorCode.DiagnosticReportErrorCodeType = diagnosticReportErrorCodeType;
            drrErrorCode.ErrorCodeType = errorCodeType;
            //set the report result to this item
            drrErrorCode.DiagnosticReportResult = DiagnosticReportResult;
            //set the error code
            drrErrorCode.ErrorCode = errorCode;

            return drrErrorCode;
        }

        //#Sprint23
        public static StringCollection GetFirstPartErrorCodes(StringCollection codes)
        {
            StringCollection result = new StringCollection();
            foreach (var c in codes)
            {
                if (string.IsNullOrWhiteSpace(c)) continue;
                result.Add(c.Split('-')[0].Trim());
            }
            return result;
        }

        public static IEnumerable<string> GetFirstPartErrorCodes(IEnumerable<string> codes)
        {
            List<string> result = new List<string>();
            foreach (var c in codes)
            {
                if (string.IsNullOrWhiteSpace(c)) continue;
                result.Add(c.Split('-')[0].Trim());
            }
            return result;
        }

        //#Logic for P0420, P0421, P0422, P0430, P0431, P0432
        public List<string> PwrStoredDTCList
        {
            get
            {
                if (PwrStoredCodes == null || PwrStoredCodes.Count == 0) return new List<string>();
                List<string> storedDtcs = new List<string>();

                foreach (var code in PwrStoredCodes)
                {
                    storedDtcs.Add(GetFirstPartErrorCode(code));
                }

                return storedDtcs;
            }
        }

        public List<string> PwrPendingDTCList
        {
            get
            {
                if (PwrPendingCodes == null || PwrPendingCodes.Count == 0) return new List<string>();
                List<string> pendingDtcs = new List<string>();

                foreach (var code in PwrPendingCodes)
                {
                    pendingDtcs.Add(GetFirstPartErrorCode(code));
                }

                return pendingDtcs;
            }
        }

        //#Logic for P0420, P0421, P0422, P0430, P0431, P0432

        /// <summary>
        /// GetFirstPartErrorCode
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetFirstPartErrorCode(string code)
        {
            return code.Split('-')[0].Trim();
        }

        /// <summary>
        /// Gets the <see cref="FixCollection"/> of fixes that match the diagnostic upload data for this car.
        /// Logic for P0420, P0421, P0422, P0430, P0431, P0432
        /// </summary>
        /// <param name="primaryErrorCode">The <see cref="string"/> primary DTC.</param>
        /// <param name="secondaryDtcs">The <see cref="StringCollection"/> of secondary DTCs.</param>
        /// <param name="logDiscrepancies">A <see cref="bool"/> that indicates if discrepancies should be logged.</param>
        /// <param name="onlyProcessFixes">A <see cref="bool"/> that indicates if only logic necessary for looking up fixes should be executed. Everything else should be skipped.</param>
        /// <param name="diagnosticReportErrorCodeSystemType">The <see cref="DiagnosticReportErrorCodeSystemType"/> type of system the codes are for.</param>
        /// <returns>A <see cref="FixCollection"/> of <see cref="Fix"/> objects.</returns>
        private FixCollection GetFixesSorted2(string primaryErrorCode, StringCollection secondaryDtcs, bool logDiscrepancies,
            bool onlyProcessFixes, DiagnosticReportErrorCodeSystemType diagnosticReportErrorCodeSystemType)
        {
            //#Logic for P0420, P0421, P0422, P0430, P0431, P0432
            primaryErrorCode = primaryErrorCode.Split('-')[0].Trim().ToUpper();
            secondaryDtcs = GetFirstPartErrorCodes(secondaryDtcs);
            //#Sprint23

            //Check MIL and Stored/Pending DTCs by the new logic
            //First step: Output fix using current logic/process
            if (!"P0420,P0421,P0422,P0430,P0431,P0432".Contains(primaryErrorCode))
                return GetFixesSorted(primaryErrorCode, secondaryDtcs, logDiscrepancies, onlyProcessFixes, diagnosticReportErrorCodeSystemType);

            //Other DTC in Stored DTC(s) and Pending DTC(s) ? = NO => Output fix using current logic/process
            //Case #1
            /*
             * 1.	MIL DTC = P0420
                2.	Secondary DTCs = True
                a.	Confirmed DTC = 1 (P0420)
                b.	Pending DTC = 1 (P0420)
                c.	Most Likely Fix = P0420
             */
            if (
                    (PwrStoredDTCList.Count == 1 && PwrStoredDTCList[0] == primaryErrorCode)
                    &&
                    (PwrPendingDTCList.Count == 1 && PwrPendingDTCList[0] == primaryErrorCode)
                )
            {
                return GetFixesSorted(primaryErrorCode, secondaryDtcs, logDiscrepancies, onlyProcessFixes, diagnosticReportErrorCodeSystemType);
            }

            //Case #2
            /*
             *  1.	MIL DTC = P0420
                2.	Secondary DTCs = True
                a.	Confirmed DTC = 2 (P0420 + other DTC)
                b.	Pending DTC = 1 (P0420)
                c.	Most Likely Fix = P0420
             */
            if (
                    (PwrStoredDTCList.Count == 2 && PwrStoredDTCList[0] == primaryErrorCode)
                    &&
                    (PwrPendingDTCList.Count == 1 && PwrPendingDTCList[0] == primaryErrorCode)
                )
            {
                return GetFixesSorted(primaryErrorCode, secondaryDtcs, logDiscrepancies, onlyProcessFixes, diagnosticReportErrorCodeSystemType);
            }

            //Case #3
            /*
             * 1.	MIL DTC = P0420
                2.	Secondary DTCs = True
                a.	Confirmed DTC = 2 (P0420 + other DTC)
                b.	Pending DTC = 1 (Other DTC)
                c.	Most Likely Fix = Other DTC
             */
            if (
                  (PwrStoredDTCList.Count == 2 && PwrStoredDTCList[0] == primaryErrorCode)
                  &&
                  (PwrPendingDTCList.Count == 1 && PwrPendingDTCList[0] == PwrStoredDTCList[1])
              )
            {
                return GetFixesSorted(PwrStoredDTCList[1], secondaryDtcs, logDiscrepancies, onlyProcessFixes, diagnosticReportErrorCodeSystemType);
            }

            //Case #4
            /*
             * 1.	MIL DTC = P0420
                2.	Secondary DTCs = True
                a.	Confirmed DTC = 1 (other DTC)
                b.	Pending DTC = 2 (P0420 + other DTC)
                c.	Most Likely Fix = Other DTC
             */
            if (
                 (PwrPendingDTCList.Count == 2 && PwrPendingDTCList[0] == primaryErrorCode)
                    &&
                 (PwrStoredDTCList.Count == 1 && PwrStoredDTCList[0] == PwrPendingDTCList[1])
             )
            {
                return GetFixesSorted(PwrPendingDTCList[1], secondaryDtcs, logDiscrepancies, onlyProcessFixes, diagnosticReportErrorCodeSystemType);
            }

            //Case #5 (Same as Case #1 but other DTC is the same)
            /*
             * 1.	MIL DTC = P0420
                2.	Secondary DTCs = True
                a.	Confirmed DTC = 1 (other DTC)
                b.	Pending DTC = 1 (other DTC)
                c.	Most Likely Fix = Other DTC
             */
            if (
                    (PwrStoredDTCList.Count == 1 && PwrPendingDTCList.Count == 1 && PwrStoredDTCList[0] == PwrPendingDTCList[0])
               )
            {
                return GetFixesSorted(PwrStoredDTCList[0], secondaryDtcs, logDiscrepancies, onlyProcessFixes, diagnosticReportErrorCodeSystemType);
            }

            //Case #6
            /*
             * 1.	MIL DTC = P0420
                2.	Secondary DTCs = True
                a.	Confirmed DTC = 2 (P0420 + other DTC)
                b.	Pending DTC = 2 (P0420 + other DTC)
                c.	Most Likely Fix = Other DTC
             */
            if (
                   (PwrPendingDTCList.Count == 2 && PwrPendingDTCList[0] == primaryErrorCode)
                   &&
                   (PwrStoredDTCList.Count == 2 && PwrStoredDTCList[0] == primaryErrorCode)
                   &&
                   (PwrPendingDTCList[1] == PwrStoredDTCList[1])
                )
            {
                return GetFixesSorted(PwrPendingDTCList[1], secondaryDtcs, logDiscrepancies, onlyProcessFixes, diagnosticReportErrorCodeSystemType);
            }

            //Case #7
            /*
             * 1.	MIL DTC = P0420
                2.	Secondary DTCs = True
                a.	Confirmed DTC = above 2 (P0420 + other DTCs)
                b.	Pending DTC = above 2 (P0420 + other DTCs)
                c.	Most Likely Fix = Other DTCs

                To determine the most likely Fix for other DTCs related to P0420 when other DTCs are above 2 DTCs:
                -	DTC must be found in both Confirmed and Pending state.
                -	Once Active DTCs are found, they are then presented by using the Fix Probability.
                -	When all frequency of all additional DTCs are all equal, output lowest DTC’s fix

                If the DTCs all of equal or no fix probability than we assign by DTC category, i.e., P00XX Cam/Crank Faults have highest probability.

             */
            if (
                    //Fix issue when Stored DTCs and Pending DTCs do not match
                    //(PwrStoredDTCList.Count > 2 && PwrPendingDTCList.Count > 2)
                    (PwrStoredDTCList.Count > 2 || PwrPendingDTCList.Count > 2)
              )
            {
                var dtcsNeedToGetFixes = GetDTCsNeedToGetFixes(primaryErrorCode, PwrStoredDTCList, PwrPendingDTCList);//#P04XX_Replace
                if (dtcsNeedToGetFixes.Any())
                {
                    FixCollection fixesPolk = new FixCollection(this.Registry);
                    SqlProcedureCommand call = new SqlProcedureCommand();

                    if (this.Vehicle.PolkVehicleYMME != null)
                    {
                        call.ProcedureName = "Fix_LoadByDiagnosticReportForDTCList";

                        call.AddNVarChar("DTCXmlList", Metafuse3.Xml.XmlList.ToXml(dtcsNeedToGetFixes));

                        call.AddInt32("Year", this.Vehicle.PolkVehicleYMME.Year);
                        call.AddNVarChar("Make", this.Vehicle.PolkVehicleYMME.Make);
                        call.AddNVarChar("Model", this.Vehicle.PolkVehicleYMME.Model);
                        call.AddNVarChar("TrimLevel", this.Vehicle.PolkVehicleYMME.Trim);
                        call.AddNVarChar("Transmission", this.Vehicle.PolkVehicleYMME.Transmission);
                        call.AddNVarChar("EngineVINCode", this.Vehicle.PolkVehicleYMME.EngineVinCode);
                        call.AddNVarChar("EngineType", this.Vehicle.PolkVehicleYMME.EngineType);

                        call.AddInt32("Market", (int)this.RuntimeInfo.CurrentMarket);

                        //#NewFixLogic
                        call.AddNVarChar("ManufacturerName", this.Vehicle.PolkVehicleYMME.Manufacturer);
                        call.AddNVarChar("PolkVehicleYmmeCountryId", this.Vehicle.PolkVehicleYMME.CountryID);

                        var diagnosticReportId = this.Id;
                        if (diagnosticReportId != Guid.Empty)
                        {
                            call.AddGuid("DiagnosticReportId", diagnosticReportId);
                        }

                        fixesPolk.Load(call, "FixId", true, true);
                        fixesPolk.SetIsFromPolkMatch(true);
                        FixCollection fixes = new FixCollection(this.Registry);

                        //Check to get fixes for DTC by frequency or index
                        List<Fix> foundFixes = new List<Fix>();
                        List<Fix> selectedFixes = new List<Fix>();
                        foreach (Fix fix in fixesPolk)
                        {
                            if (!foundFixes.Any(f => f.Id == fix.Id))
                                foundFixes.Add(fix);
                        }

                        if (!foundFixes.Any())
                            return fixes;

                        //get fixes by freq fix first
                        var frequencyList = foundFixes.Select(f => f.FrequencyCount).Distinct();
                        if (frequencyList.Count() > 1)
                        {
                            /*
                             * Frequency of
                                Stored and
                                Pending DTCs
                                different? = TRUE
                             */
                            var maxFreq = frequencyList.Max();
                            selectedFixes.AddRange(foundFixes.Where(f => f.FrequencyCount == maxFreq));
                        }
                        else //all fixes are same freq
                        {
                            //then find fixDtcs with highest probability P00xx
                            var highestProbDtcs = dtcsNeedToGetFixes.Where(dtc => dtc.StartsWith("P00")).ToList();
                            if (highestProbDtcs.Any())
                            {
                                foreach (var dtc in highestProbDtcs)
                                {
                                    selectedFixes.AddRange(foundFixes.Where(f => f.PrimaryDTCs.Contains(dtc)));
                                }
                            }

                            //When all frequency of all additional DTCs are all equal, output lowest DTC’s fix
                            if (!selectedFixes.Any()) //No fix found for highestProbDtcs
                            {
                                IEnumerable<Fix> fixesByIndex = new List<Fix>();
                                for (var i = 0; i < dtcsNeedToGetFixes.Count; i++)
                                {
                                    fixesByIndex = foundFixes.Where(f => f.PrimaryDTCs.Contains(dtcsNeedToGetFixes[i]));
                                    if (fixesByIndex.Any())
                                    {
                                        selectedFixes.AddRange(fixesByIndex);
                                        break;
                                    }
                                }
                            }
                        }

                        //Check to get fixes for DTC by frequency or index

                        fixes.ClearLookupTables();
                        foreach (Fix f in selectedFixes)
                        {
                            if (logDiscrepancies && this.Vehicle.PolkVehicleYMME != null)
                            {
                                FixPolkVehicleDiscrepancy.AddDiscrepancy(this.Registry, f, this.Vehicle.PolkVehicleYMME, false, true);
                            }

                            if (fixes.FindByProperty("Id", f.Id) == null)
                            {
                                fixes.Add(f);
                                fixes.ClearLookupTables();
                            }
                        }

                        if (!onlyProcessFixes)
                        {
                            //load the relations
                            this.SortFixes(fixes, primaryErrorCode, secondaryDtcs, diagnosticReportErrorCodeSystemType);
                        }

                        return fixes;
                    }
                }
            }

            //Not in 7 cases above, just find fixes for MIL DTC as current process
            return GetFixesSorted(primaryErrorCode, secondaryDtcs, logDiscrepancies, onlyProcessFixes, diagnosticReportErrorCodeSystemType);
        }

        //#IWD-12
        /// <summary>
        /// Gets the <see cref="FixCollection"/> of fixes that match the diagnostic upload data for this car.
        /// Logic for P0420, P0421, P0422, P0430, P0431, P0432
        /// </summary>
        /// <param name="primaryErrorCode">The <see cref="string"/> primary DTC.</param>
        /// <param name="secondaryDtcs">The <see cref="StringCollection"/> of secondary DTCs.</param>
        /// <param name="logDiscrepancies">A <see cref="bool"/> that indicates if discrepancies should be logged.</param>
        /// <param name="onlyProcessFixes">A <see cref="bool"/> that indicates if only logic necessary for looking up fixes should be executed. Everything else should be skipped.</param>
        /// <param name="diagnosticReportErrorCodeSystemType">The <see cref="DiagnosticReportErrorCodeSystemType"/> type of system the codes are for.</param>
        /// <returns>A <see cref="FixCollection"/> of <see cref="Fix"/> objects.</returns>
        //#P04XX_Replace
        private FixCollection GetFixesSorted3(string primaryErrorCode, StringCollection secondaryDtcs, bool logDiscrepancies,
            bool onlyProcessFixes, DiagnosticReportErrorCodeSystemType diagnosticReportErrorCodeSystemType)
        {
            //#Logic for P0420, P0421, P0422, P0430, P0431, P0432
            primaryErrorCode = primaryErrorCode.Split('-')[0].Trim().ToUpper();
            secondaryDtcs = GetFirstPartErrorCodes(secondaryDtcs);
            //#Sprint23

            //Check MIL and Stored/Pending DTCs by the new logic
            //First step: Output fix using current logic/process
            if (!"P0420,P0421,P0422,P0430,P0431,P0432".Contains(primaryErrorCode))
                return GetFixesSorted(primaryErrorCode, secondaryDtcs, logDiscrepancies, onlyProcessFixes, diagnosticReportErrorCodeSystemType);

            var storedDtcs = TrimUCodes(PwrStoredDTCList);
            var pendingDtcs = TrimUCodes(PwrPendingDTCList);

            //#1: Other Dtc in stored and pending = NO -> Find fixes for MIL DTC
            if (storedDtcs.Count == 0 && pendingDtcs.Count == 0)
            {
                return GetFixesSorted(primaryErrorCode, secondaryDtcs, logDiscrepancies, onlyProcessFixes, diagnosticReportErrorCodeSystemType);
            }

            //#1: Other Dtc in stored and pending = YES (1 DTC and dif from MIL) -> Find fixes for Stored/Pending DTC
            if (storedDtcs.Count == 1
                && pendingDtcs.Count == 1
                && storedDtcs[0] == pendingDtcs[0]
                && storedDtcs[0] != primaryErrorCode)
            {
                return GetFixesSorted(storedDtcs[0], secondaryDtcs, logDiscrepancies, onlyProcessFixes, diagnosticReportErrorCodeSystemType);
            }


            if (storedDtcs.Count == 2
                && pendingDtcs.Count == 1
                && storedDtcs.Contains(pendingDtcs[0])
                )
            {
                return GetFixesSorted(pendingDtcs[0], secondaryDtcs, logDiscrepancies, onlyProcessFixes, diagnosticReportErrorCodeSystemType);
            }


            if (storedDtcs.Count == 1
                && pendingDtcs.Count == 2
                && pendingDtcs.Contains(storedDtcs[0])
                )
            {
                return GetFixesSorted(storedDtcs[0], secondaryDtcs, logDiscrepancies, onlyProcessFixes, diagnosticReportErrorCodeSystemType);
            }

            //#2: Other Dtc in stored and pending = NO -> Find fixes for MIL DTC
            /*
             * More than one DTC in both stored and pending (Not including MIL DTC) = NO
             */
            if (storedDtcs.Count == 2 && pendingDtcs.Count == 2)
            {
                var otherDtc = storedDtcs.SkipWhile(p => p == primaryErrorCode).FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(otherDtc))
                {
                    if (storedDtcs.Contains(primaryErrorCode)
                        && pendingDtcs.Contains(otherDtc))
                    {
                        return GetFixesSorted(otherDtc, secondaryDtcs, logDiscrepancies, onlyProcessFixes, diagnosticReportErrorCodeSystemType);
                    }
                }

            }
            /*
             *  * More than one DTC in both stored and pending (Not including MIL DTC) = YES
             */
            else if (storedDtcs.Count >= 3 && pendingDtcs.Count >= 3)
            {
                var dtcNeedToGetFixes = GetOneDTCNeedToGetFixes(primaryErrorCode, storedDtcs, pendingDtcs);
                if (!string.IsNullOrWhiteSpace(dtcNeedToGetFixes))
                {
                    return GetFixesSorted(dtcNeedToGetFixes, secondaryDtcs, logDiscrepancies, onlyProcessFixes, diagnosticReportErrorCodeSystemType);
                }
            }


            //Not in all cases above, just find fixes for MIL DTC as current process
            return GetFixesSorted(primaryErrorCode, secondaryDtcs, logDiscrepancies, onlyProcessFixes, diagnosticReportErrorCodeSystemType);
        }

        //#Logic for P0420, P0421, P0422, P0430, P0431, P0432
        /// <summary>
        /// Get DTCs need to get fix for Case #7
        /// </summary>
        /// <returns></returns>
        //#P04XX_Replace
        private List<string> GetDTCsNeedToGetFixes(string primaryErrorCode, List<string> storedDtcList, List<string> pendingDtcList)
        {
            List<string> dtcs = new List<string>();
            if (storedDtcList.Count < 2 || pendingDtcList.Count < 2)
                return dtcs;

            var storedDtcs = new List<string>();
            var pendingDtcs = new List<string>();
            foreach (var stored in storedDtcList)
            {
                if (string.Equals(primaryErrorCode, stored)) //Skip stored DTC if matching with MIL Code
                    continue;

                storedDtcs.Add(stored);
            }
            foreach (var pending in pendingDtcList)
            {
                if (string.Equals(primaryErrorCode, pending)) //Skip pending DTC if matching with MIL Code
                    continue;

                pendingDtcs.Add(pending);
            }

            for (var i = 0; i < storedDtcs.Count; i++)
            {
                if (pendingDtcs.Contains(storedDtcs[i]))
                {
                    dtcs.Add(storedDtcs[i]);
                }
            }

            return dtcs;
        }

        //#IWD-12
        //#P04XX_Replace
        private string GetOneDTCNeedToGetFixes(string primaryErrorCode, List<string> storedDtcList, List<string> pendingDtcList)
        {
            var dtcsNeedFixes = GetDTCsNeedToGetFixes(primaryErrorCode, storedDtcList, pendingDtcList);

            return HighlightDtcList.GetHighestPriorityDtc(dtcsNeedFixes);
        }

        //#P04XX_Replace
        private List<string> TrimUCodes(List<string> source)
        {
            var newList = new List<string>();

            if (source == null) return newList;
            foreach (var code in source)
            {
                if (code.ToUpper().StartsWith("U")) continue;
                newList.Add(code.ToUpper().Trim());
            }

            return newList;
        }

    }

    public class AbsDtcInfo
    {
        public string AbsLightStatus { get; set; }
        public List<string> RecommendRepairDtcs { get; set; }
    }

    //#SP36
    public class OemDtcInfo
    {
        public AbsDtcInfo AbsDtcInfo { get; set; }
        public List<string> SrsRecommendRepairDtcs { get; set; }
    }
}