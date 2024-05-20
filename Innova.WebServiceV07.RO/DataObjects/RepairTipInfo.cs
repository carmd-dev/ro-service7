using System;

namespace Innova.WebServiceV07.RO.DataObjects
{
    //#RepairTips
    /// <summary>
    ///
    /// </summary>
    public class RepairTipInfo
    {
        public string FixName { get; set; }
        public string InitialInspection { get; set; }
        public string PossibleCause { get; set; }
        public string DiagnosticProcedure { get; set; }
        public string RepairValidation { get; set; }
    }
}