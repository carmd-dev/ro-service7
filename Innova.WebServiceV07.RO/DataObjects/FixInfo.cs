using Innova.Articles;
using Innova.DiagnosticReports;
using Innova.Fixes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Innova.WebServiceV07.RO.DataObjects
{
    /// <summary>
    /// A fix that is associated with the primary error code on the diagnostic report.
    /// </summary>
    public class FixInfo
    {
        /// <summary>
        /// Default constructor for the fix info object.
        /// </summary>
        public FixInfo()
        {
        }

        //Added on 2017-08-10 by Nam Lu - INNOVA Dev Team: Support ProRS Fix
        /// <summary>
        /// The <see cref="Guid"/> Fix of the fix name
        /// </summary>
        public Guid FixId { get; set; }

        /// <summary>
        /// The <see cref="Guid"/> ID of the fix name, which is passed to the repair procedures web services.
        /// </summary>
        public Guid? FixNameId = null;

        /// <summary>
        /// The <see cref="string"/> name of the fix.
        /// </summary>
        public string Name = "";

        /// <summary>
        /// The <see cref="string"/>pro name of the fix.
        /// </summary>
        public string ProName = "";

        /// <summary>
        /// The <see cref="string"/> error code (PrimaryDTC, or OBD1, ABS, SRS code the fix is for).  Does not apply to Fix Info when returned with the predictive diagnostic report.
        /// </summary>
        public string ErrorCode = "";

        /// <summary>
        /// The <see cref="int"/> error code system type.  Possible values include: 0 - Powertrain, 1 - OBD1, 2 - ABS, 3 - SRS.  Does not apply to fix info when returned for the predictive diagnostic report.
        /// </summary>
        public int ErrorCodeSystemType;

        /// <summary>
        /// The <see cref="int"/> sort order (within system type of the fixes).
        /// </summary>
        public int SortOrder;

        /// <summary>
        /// The <see cref="string"/> description of the fix.
        /// </summary>
        public string Description = "";

        /// <summary>
        /// The <see cref="decimal"/> labor hours required for the fix.
        /// </summary>
        public decimal LaborHours = 0;

        /// <summary>
        /// The <see cref="decimal"/> labor rate for the fix (for your state).
        /// </summary>
        public decimal LaborRate = 0;

        /// <summary>
        /// The <see cref="decimal"/> calculated labor cost.
        /// </summary>
        public decimal LaborCost = 0;

        /// <summary>
        /// The <see cref="decimal"/> calculated parts cost.
        /// </summary>
        public decimal PartsCost = 0;

        /// <summary>
        /// The <see cref="decimal"/> additional cost (if any).
        /// </summary>
        public decimal AdditionalCost = 0;

        /// <summary>
        /// The	<see cref="decimal"/> total cost.
        /// </summary>
        public decimal TotalCost;

        /// <summary>
        /// The <see cref="int"/> rating of the fix.
        /// 0 = Unrated
        /// 1 = Easily accessible, Basic tools may be required, No special skill required, No floor jack required.
        ///	2 = Easily accessible, Basic Tools Required, No special skill required, May be time consuming, Floor jack may be required.
        ///	3 = Semi difficult accessibility, Special tools may be required, Basic level automotive training recommended, Floor jack may be required.
        ///	4 = Difficult accessibility, Special tools may be required, Basic level automotive training recommended, Vehicle lift may be required.
        ///	5 = Difficult accessibility, Special tools may be required, Advanced level automotive training recommended, Vehicle lift may be required.
        /// </summary>
        public int FixRating = 0;

        /// <summary>
        /// The array of <see cref="FixFeedbackInfo"/> objects that have been submitted for this fix. Could be null if no feedback has been provided.
        /// </summary>
        public FixFeedbackInfo[] FixFeedbacks;

        /// <summary>
        /// The array of <see cref="ArticleInfo"/> objects that that are related to this fix.
        /// </summary>
        public ArticleInfo[] RelatedArticles;

        /// <summary>
        ///
        /// </summary>
        public int FrequencyCount { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int CommunityVotes { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool OBDFixApproved { get; set; }

        /// <summary>
        /// The <see cref="decimal"/> predictive diagnostic percentage within the report period mileage.
        /// </summary>
        public decimal PredictiveDiagnosticPercentInMileage = 0;

        /// <summary>
        /// The <see cref="int"/> predictive diagnostic count.
        /// </summary>
        public int PredictiveDiagnosticCount = 0;

        /// <summary>
        /// The array of <see cref="FixPartInfo"/> objects required for this fix.  Could be null, does not apply to Predictive Diagnostic Report.
        /// </summary>
        public FixPartInfo[] FixParts;

        //ToolDB_
        /// <summary>
        /// FixTools
        /// </summary>
        public FixToolInfo[] FixTools;

        //ToolDB_

        /// <summary>
        /// Repair Tips
        /// </summary>
        public RepairTipInfo RepairTipInfo { get; set; }

        /// <summary>
        /// Method updates the web service object from the supplied SDK object
        /// </summary>
        /// <param name="sdkObject"><see cref="DiagnosticReportResultFixPart"/> object to create the object from.</param>
        /// <returns><see cref="FixInfo"/> object created from the supplied SDK object.</returns>
        protected internal static FixInfo GetWebServiceObject(DiagnosticReportResultFix sdkObject)
        {
            FixInfo wsObject = new FixInfo();

            //if we have a fix name, then apply it to the fix info class
            if (sdkObject.FixName != null)
            {
                wsObject.FixNameId = sdkObject.FixName.Id;
                //#ProName
                wsObject.ProName = sdkObject.FixName.ProNameDescription_Translated;
            }
            wsObject.Name = sdkObject.Name_Translated;

            wsObject.Description = sdkObject.Description_Translated;

            ReCalculateFixInfoCosts(wsObject, sdkObject);

            //#ProName
            wsObject.FixRating = (int)sdkObject.FixName.FixRating;
            if (sdkObject.FixName != null)
            {
                wsObject.FixRating = (int)sdkObject.FixName.FixRating;
            }

            //add the error code and other info
            wsObject.ErrorCode = sdkObject.PrimaryErrorCode;
            wsObject.ErrorCodeSystemType = (int)sdkObject.DiagnosticReportErrorCodeSystemType;
            wsObject.SortOrder = sdkObject.SortOrder;

            if (sdkObject.Fix != null)
            {
                DiagnosticReportFixFeedbackCollection fixFeedbacks = new DiagnosticReportFixFeedbackCollection(sdkObject.Registry);
                fixFeedbacks.LoadByFixAndDtc(sdkObject.Fix, sdkObject.PrimaryErrorCode);

                wsObject.FixFeedbacks = new FixFeedbackInfo[fixFeedbacks.Count];

                for (int i = 0; i < fixFeedbacks.Count; i++)
                {
                    wsObject.FixFeedbacks[i] = FixFeedbackInfo.GetWebServiceObject(fixFeedbacks[i]);
                }

                ArticleCollection articles = sdkObject.Fix.FixName.GetRelatedArticles();

                wsObject.RelatedArticles = new ArticleInfo[articles.Count];

                for (int i = 0; i < articles.Count; i++)
                {
                    wsObject.RelatedArticles[i] = ArticleInfo.GetWebServiceObject(articles[i]);
                }
            }

            wsObject.FixParts = new FixPartInfo[sdkObject.DiagnosticReportResultFixParts.Count];

            for (int i = 0; i < sdkObject.DiagnosticReportResultFixParts.Count; i++)
            {
                wsObject.FixParts[i] = FixPartInfo.GetWebServiceObject(sdkObject.DiagnosticReportResultFixParts[i]);
            }

            //Ticket IWD-7: Update Part Ranking logic for a fix with Multiple Parts
            if (wsObject.FixParts.Any())
            {
                wsObject.FixParts = MatchingFixPart.Sort(wsObject.FixParts, wsObject.Name);
            }
            //Ticket IWD-7: Update Part Ranking logic for a fix with Multiple Parts

            //ToolDB_
            wsObject.FixTools = new FixToolInfo[sdkObject.DiagnosticReportResultFixTools.Count];

            for (int i = 0; i < sdkObject.DiagnosticReportResultFixTools.Count; i++)
            {
                wsObject.FixTools[i] = FixToolInfo.GetWebServiceObject(sdkObject.DiagnosticReportResultFixTools[i]);
            }
            //ToolDB_

            //Added on 2017-09-21 by Nam Lu - INNOVA Dev Team:  Attach FixId to FixInfo

            if (sdkObject.Fix != null)
            {
                wsObject.FixId = sdkObject.Fix.Id;
            }

            //Added on 2017-09-21 by Nam Lu - INNOVA Dev Team:  Attach FixId to FixInfo

            return wsObject;
        }

        /// <summary>
        /// Method updates the web service object from the supplied SDK object
        /// </summary>
        /// <param name="sdkObject"><see cref="Innova.Fixes.Fix"/> object to create the object from.</param>
        /// <returns><see cref="FixInfo"/> object created from the supplied SDK object.</returns>
        protected internal static FixInfo GetWebServiceObject(Innova.Fixes.Fix sdkObject)
        {
            FixInfo wsObject = new FixInfo();

            //if we have a fix name, then apply it to the fix info class
            if (sdkObject.FixName != null)
            {
                wsObject.FixNameId = sdkObject.FixName.Id;
                //#ProName
                wsObject.ProName = sdkObject.FixName.ProNameDescription_Translated;
                wsObject.Name = sdkObject.FixName.Description_Translated;
            }

            if (sdkObject.FixName != null)
            {
                wsObject.Description = sdkObject.FixName.Description_Translated;
            }
            wsObject.LaborHours = sdkObject.Labor;

            //#ABS_SRS_Fixes
            var fixDTCs = new List<string>();
            if (sdkObject.FixDTCs.Count > 0)
            {
                foreach (FixDTC fixDtc in sdkObject.FixDTCs)
                {
                    fixDTCs.Add(fixDtc.PrimaryDTC);
                    if (!string.IsNullOrWhiteSpace(fixDtc.SecondaryDTCList))
                        fixDTCs.Add(fixDtc.SecondaryDTCList);
                }
            }

            wsObject.ErrorCode = fixDTCs.Count == 1 ? fixDTCs[0] : string.Join(",", fixDTCs);

            ReCalculateFixInfoCosts(wsObject, sdkObject);

            if (sdkObject.FixName != null)
            {
                wsObject.FixRating = (int)sdkObject.FixName.FixRating;
            }

            DiagnosticReportFixFeedbackCollection fixFeedbacks = new DiagnosticReportFixFeedbackCollection(sdkObject.Registry);
            if (sdkObject.FixDTCs != null && sdkObject.FixDTCs.Count > 0)
            {
                fixFeedbacks.LoadByFixAndDtc(sdkObject, ((FixDTC)sdkObject.FixDTCs[0]).PrimaryDTC);

                wsObject.FixFeedbacks = new FixFeedbackInfo[fixFeedbacks.Count];

                for (int i = 0; i < fixFeedbacks.Count; i++)
                {
                    wsObject.FixFeedbacks[i] = FixFeedbackInfo.GetWebServiceObject(fixFeedbacks[i]);
                }
            }
            wsObject.FixParts = new FixPartInfo[sdkObject.FixParts.Count];

            for (int i = 0; i < sdkObject.FixParts.Count; i++)
            {
                wsObject.FixParts[i] = FixPartInfo.GetWebServiceObject(sdkObject.FixParts[i]);
            }

            //ToolDB_
            wsObject.FixTools = new FixToolInfo[sdkObject.FixTools.Count];

            for (int i = 0; i < sdkObject.FixTools.Count; i++)
            {
                wsObject.FixTools[i] = FixToolInfo.GetWebServiceObject(sdkObject.FixTools[i]);
            }
            //ToolDB_

            return wsObject;
        }

        /// <summary>
        ///  Re-calculate FixInfo costs for DiagnosticReportResultFix
        /// </summary>
        protected static void ReCalculateFixInfoCosts(FixInfo wsObject, DiagnosticReportResultFix sdkObject)
        {
            // Calculate the total cost of the solution
            wsObject.LaborHours = sdkObject.Labor;
            wsObject.LaborRate = sdkObject.RuntimeInfo.CurrentStateLaborRateInUSD;
            wsObject.LaborCost = sdkObject.Labor * wsObject.LaborRate;
            wsObject.AdditionalCost = sdkObject.AdditionalCost;
            wsObject.PartsCost = 0;
            foreach (DiagnosticReportResultFixPart p in sdkObject.DiagnosticReportResultFixParts)
            {
                wsObject.PartsCost += (p.Quantity * p.Part.Price);
            }

            wsObject.TotalCost = wsObject.LaborCost + wsObject.AdditionalCost + wsObject.PartsCost;

            // Now calculate the local curreny values if necessary.
            if (sdkObject.RuntimeInfo.CurrentCurrency != Currency.USD)
            {
                wsObject.LaborRate = sdkObject.RuntimeInfo.GetLocalCurrencyValueFromUSDollars(wsObject.LaborRate);
                wsObject.LaborCost = sdkObject.RuntimeInfo.GetLocalCurrencyValueFromUSDollars(wsObject.LaborCost);
                wsObject.AdditionalCost = sdkObject.RuntimeInfo.GetLocalCurrencyValueFromUSDollars(wsObject.AdditionalCost);
                wsObject.PartsCost = sdkObject.RuntimeInfo.GetLocalCurrencyValueFromUSDollars(wsObject.PartsCost);
                wsObject.TotalCost = sdkObject.RuntimeInfo.GetLocalCurrencyValueFromUSDollars(wsObject.TotalCost);
            }
        }

        /// <summary>
        ///  Re-calculate FixInfo costs for Fix
        /// </summary>
        /// <param name="wsObject"></param>
        /// <param name="fix"></param>
        protected static void ReCalculateFixInfoCosts(FixInfo wsObject, Fix fix)
        {
            // Calculate the total cost of the solution
            wsObject.LaborHours = fix.Labor;
            wsObject.LaborRate = fix.RuntimeInfo.CurrentStateLaborRateInUSD;
            wsObject.LaborCost = fix.Labor * wsObject.LaborRate;
            wsObject.AdditionalCost = fix.AdditionalCost;
            wsObject.PartsCost = 0;
            foreach (FixPart p in fix.FixParts)
            {
                wsObject.PartsCost += (p.Quantity * p.Part.Price);
            }

            wsObject.TotalCost = wsObject.LaborCost + wsObject.AdditionalCost + wsObject.PartsCost;

            // Now calculate the local curreny values if necessary.
            if (fix.RuntimeInfo.CurrentCurrency != Currency.USD)
            {
                wsObject.LaborRate = fix.RuntimeInfo.GetLocalCurrencyValueFromUSDollars(wsObject.LaborRate);
                wsObject.LaborCost = fix.RuntimeInfo.GetLocalCurrencyValueFromUSDollars(wsObject.LaborCost);
                wsObject.AdditionalCost = fix.RuntimeInfo.GetLocalCurrencyValueFromUSDollars(wsObject.AdditionalCost);
                wsObject.PartsCost = fix.RuntimeInfo.GetLocalCurrencyValueFromUSDollars(wsObject.PartsCost);
                wsObject.TotalCost = fix.RuntimeInfo.GetLocalCurrencyValueFromUSDollars(wsObject.TotalCost);
            }
        }
    }

    //Ticket IWD-7: Update Part Ranking logic for a fix with Multiple Parts
    public class MatchingFixPart
    {
        public decimal PartTotalCost { get; set; }
        public double MatchPercentage { get; set; }
        public FixPartInfo FixPartInfo { get; set; }

        public static FixPartInfo[] Sort(FixPartInfo[] fixParts, string fixName)
        {
            var sortedList = new List<MatchingFixPart>();
            foreach (var fp in fixParts)
            {
                sortedList.Add(new MatchingFixPart
                {
                    MatchPercentage = GetMatchingPercentage(fp.Name, fixName),
                    PartTotalCost = fp.Price,
                    FixPartInfo = fp
                });
            }

            return sortedList.OrderByDescending(s => s.MatchPercentage)
                .ThenByDescending(s => s.PartTotalCost)
                .Select(s => s.FixPartInfo).ToArray();
        }

        private static double GetMatchingPercentage(string partName, string fixName)
        {
            var fixNameParts = fixName.ToLower().Trim().Split(' ');
            var partNameParts = partName.ToLower().Trim().Split(' ');

            var matchingItems = fixNameParts.Intersect(partNameParts);

            return ((double)matchingItems.Distinct().Count() / fixNameParts.Count()) * 100;
        }
    }

    //Ticket IWD-7: Update Part Ranking logic for a fix with Multiple Parts
}