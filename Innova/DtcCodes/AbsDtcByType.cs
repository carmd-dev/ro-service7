using System.Collections.Generic;
using System.Linq;

namespace Innova.DtcCodes
{
    public class SrsDtcByStatus : AbsDtcByStatus
    { } //#Sprint36

    public class AbsDtcByStatus
    {
        public List<string> CodeList => (CodeString ?? "").Split('|').ToList();
        public string CodeString { get; set; }
        public string DtcStatus { get; set; }
    }

    public class AbsDtcByStatusGroup
    {
        public AbsDtcByStatusGroup()
        {
            RecommendRepairDtcs = new List<AbsDtcByStatus>();
            WarningDtcs = new List<AbsDtcByStatus>();
            HistoryDtcs = new List<AbsDtcByStatus>();
            UnknownDtcs = new List<AbsDtcByStatus>();
        }

        /// <summary>
        ///
        /// </summary>
        public bool HasDtc =>
            RecommendRepairDtcs.Any()
            || WarningDtcs.Any()
            || HistoryDtcs.Any()
            || UnknownDtcs.Any();

        public List<AbsDtcByStatus> RecommendRepairDtcs { get; set; }
        public List<AbsDtcByStatus> WarningDtcs { get; set; }
        public List<AbsDtcByStatus> HistoryDtcs { get; set; }
        public List<AbsDtcByStatus> UnknownDtcs { get; set; }
    }

    public class SrsDtcByStatusGroup
    {
        public SrsDtcByStatusGroup()
        {
            RecommendRepairDtcs = new List<SrsDtcByStatus>();
            WarningDtcs = new List<SrsDtcByStatus>();
            HistoryDtcs = new List<SrsDtcByStatus>();
            UnknownDtcs = new List<SrsDtcByStatus>();
        }

        /// <summary>
        ///
        /// </summary>
        public bool HasDtc =>
            RecommendRepairDtcs.Any()
            || WarningDtcs.Any()
            || HistoryDtcs.Any()
            || UnknownDtcs.Any();

        public List<SrsDtcByStatus> RecommendRepairDtcs { get; set; }
        public List<SrsDtcByStatus> WarningDtcs { get; set; }
        public List<SrsDtcByStatus> HistoryDtcs { get; set; }
        public List<SrsDtcByStatus> UnknownDtcs { get; set; }
    }

    public class ReportExtraData
    {
        public string Name { get; set; }
        public string ValueLongString { get; set; }
        public string ValueStatus { get; set; }
    }

    public class DTCTitle
    {
        public string Dtc { get; set; }
        public string Title { get; set; }
    }
}