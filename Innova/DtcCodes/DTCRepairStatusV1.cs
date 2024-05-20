using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;

namespace Innova.DtcCodes
{
    //#SP36Extra
    /// <summary>
    /// DTC repair status for payload V1
    /// </summary>
    public class DTCRepairStatusV1
    {
        public string Make { get; set; }
        public string DTCStatus { get; set; }
        public string RepairStatus { get; set; }
        public string SuggestRepair { get; set; }

        private static List<DTCRepairStatusV1> statusList = null;

        private static void LoadStatusList(Registry registry)
        {
            if (statusList == null)
            {
                statusList = new List<DTCRepairStatusV1>();
                const string sql = @"SELECT [Make]
                                  ,[DTCStatus]
                                  ,[RepairStatus]
                                  ,[SuggestRepair]

                              FROM [DTC_RepairStatus]";
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(registry.ConnectionStringDefault))
                {
                    dr.CommandTimeout = 120;
                    dr.SqlProcedureCommand.SqlCommand.CommandText = sql;
                    dr.SqlProcedureCommand.SqlCommand.CommandType = CommandType.Text;

                    dr.Execute();

                    while (dr.Read())
                    {
                        statusList.Add(new DTCRepairStatusV1
                        {
                            Make = dr.GetString("Make").ToUpper().Trim(),
                            DTCStatus = dr.GetString("DTCStatus").Trim(),
                            RepairStatus = dr.GetString("RepairStatus"),
                            SuggestRepair = dr.GetString("SuggestRepair")
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Get DTC Repair Status by make and type
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="make"></param>
        /// <param name="dtcStatuses"></param>
        /// <returns></returns>
        public static DTCRepairStatusV1 GetStatus(Registry registry,
            string make, List<string> dtcStatuses)
        {
            if (statusList == null)
                LoadStatusList(registry);

            foreach (var st in dtcStatuses)
            {
                var r = statusList.FirstOrDefault(s =>
                    string.Equals(make, s.Make, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(s.DTCStatus, st, StringComparison.OrdinalIgnoreCase));

                if (r != null)
                    return r;
            }

            return null;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class OEMDTCTypeRepairStatusV1
    {
        public OEMDTCType Oemdtc { get; set; }
        public string RepairStatus { get; set; }
        public string RepairStatusText { get; set; }

        /// <summary>
        /// Get DTC Repair Status
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="dtcTypes"></param>
        /// <param name="make"></param>
        /// <returns></returns>
        public static List<OEMDTCTypeRepairStatus> GetStatuses(Registry registry, List<OEMDTCType> dtcTypes, string make)
        {
            make = make.ToUpper().Trim();
            List<OEMDTCType> unknownStatusList = new List<OEMDTCType>();

            var result = new List<OEMDTCTypeRepairStatus>();
            foreach (var dtcType in dtcTypes)
            {
                var r = new OEMDTCTypeRepairStatus
                {
                    Oemdtc = dtcType,
                    RepairStatus = dtcType.RepairStatus ?? string.Empty
                };

                if (string.IsNullOrWhiteSpace(r.RepairStatus))
                {
                    if (dtcType.TypeList == null || !dtcType.TypeList.Any())
                    {
                        dtcType.TypeList = new List<string> { "" };
                    }

                    var status = DTCRepairStatusV1.GetStatus(registry, make, dtcType.TypeList);
                    if (status != null)
                    {
                        r.RepairStatus = status.RepairStatus;
                        r.Oemdtc.RepairStatus = status.RepairStatus;
                    }
                    else
                    {
                        r.RepairStatus = "Unknown";
                        r.Oemdtc.RepairStatus = "Unknown";
                        r.RepairStatusText = "Unknown Repair Status";
                        //
                        unknownStatusList.Add(dtcType);
                        //
                    }
                }
                if (string.Equals("Recommend Repair", r.RepairStatus) || string.Equals("Recommended Repair", r.RepairStatus))
                {
                    r.RepairStatusText = "Recommend Repair = This fault should be repaired immediately.";
                }
                else if (string.Equals("Warning", r.RepairStatus))
                {
                    r.RepairStatusText = "Warning = There may be a fault developing but does not need to be repaired at this time.";
                }
                else if (string.Equals("History", r.RepairStatus))
                {
                    r.RepairStatusText = "History = There is no fault currently to repair.";
                }

                result.Add(r);
            }

            return result;
        }
    }
}