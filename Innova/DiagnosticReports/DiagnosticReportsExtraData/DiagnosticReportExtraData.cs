using Innova.DtcCodes;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Innova.DiagnosticReports.DiagnosticReportsExtraData
{
    //#SP36Extra
    /// <summary>
    /// DiagnosticReportExtraData
    /// </summary>
    public class DiagnosticReportExtraData
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="diagnosticReportId"></param>
        /// <param name="silStatus"></param>
        public static void SaveExtraDataForSIL(Registry registry, Guid diagnosticReportId, string silStatus)
        {
            Task.Run(() =>
            {
                if (string.IsNullOrWhiteSpace(silStatus))
                {
                    return;
                }

                int silValue = 0;

                if (string.Equals("ON", silStatus.ToUpper()))
                {
                    silValue = 1;
                }
                else if (string.Equals("OFF", silStatus.ToUpper()))
                {
                    silValue = 2;
                }

                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(registry.ConnectionStringDefault))
                {
                    dr.ProcedureName = "DiagnosticReportExtraData_Create";
                    dr.AddGuid("DiagnosticReportExtraDataId", Guid.NewGuid());
                    dr.AddGuid("DiagnosticReportId", diagnosticReportId);
                    dr.AddNVarChar("Name", "SIL");
                    dr.AddInt32("ValueInt", silValue);
                    dr.AddNVarChar("ValueString", silStatus);

                    dr.ExecuteNonQuery();
                }
            });
        }

        /// <summary>
        /// SetAbsDTCsToReportExtraData
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="diagnosticReportId"></param>
        /// <param name="vehicleMake"></param>
        /// <param name="decodedDtcs"></param>
        /// <param name="dtcRepairStatuses"></param>
        /// <returns></returns>
        public static string SetAbsDTCsToReportExtraData(Registry registry, Guid diagnosticReportId,
            string vehicleMake, List<OEMDTCType> decodedDtcs, List<OEMDTCTypeRepairStatus> dtcRepairStatuses = null)
        {
            //If dtcRepairStatuses already set outside, not need to get repair status again
            //this case used for Service6 -- All Stored/Pending ABS codes will be set to 'Warning' status table
            var dtcTypes = dtcRepairStatuses ?? OEMDTCTypeRepairStatus.GetStatuses(registry, decodedDtcs, vehicleMake);

            var recommendRepairDtcs = string.Join("|",
                dtcTypes.Where(s =>
                    s.RepairStatus.ToLower() == "recommend repair"
                    || s.RepairStatus.ToLower() == "recommended repair"
                    ).Select(s => s.Oemdtc.DTC));

            var sqlUpdate1 = $@"Update DiagnosticReport
                                Set AbsRecommendRepairCodesString = N'{recommendRepairDtcs}'
                                Where DiagnosticReportId='{diagnosticReportId}'";

            var dtcExtraObjs = new List<OEMDTCType>();
            foreach (var dtc in dtcTypes)
            {
                if (dtc.Oemdtc.TypeList.Count == 1)
                {
                    dtc.Oemdtc.Type = dtc.Oemdtc.TypeList[0];
                    dtcExtraObjs.Add(dtc.Oemdtc);
                }
                else
                {
                    foreach (var tpe in dtc.Oemdtc.TypeList)
                    {
                        var dtco = new OEMDTCType
                        {
                            DTC = dtc.Oemdtc.DTC,
                            RepairStatus = dtc.RepairStatus,
                            Group = dtc.Oemdtc.Group,
                            Title = dtc.Oemdtc.Title,
                            Type = tpe
                        };
                        dtcExtraObjs.Add(dtco);
                    }
                }
            }

            var sqlInserts = new List<string>
            {
                sqlUpdate1
            };

            //Remove existing ABS extra data if any
            var removeSql = $@"DELETE FROM [dbo].[DiagnosticReportExtraData]
                                WHERE [Name] IN (N'Recommend Repair',N'Recommended Repair',N'Warning',N'History',N'Unknown')
                                AND DiagnosticReportId='{diagnosticReportId}'";
            sqlInserts.Add(removeSql);

            //Group by Repair Status
            var grpByRepairStatus = dtcExtraObjs.GroupBy(s => s.RepairStatus);
            foreach (var grp1 in grpByRepairStatus)
            {
                var repairStatus = grp1.Key;
                //Group by Type
                var groupByDTCStatus = grp1.GroupBy(s => s.Type);
                foreach (var grp2 in groupByDTCStatus)
                {
                    var dtcs = string.Join("|", grp2.Select(s => s.DTC));
                    var dtcStatus = grp2.Key;
                    sqlInserts.Add($@"INSERT INTO [dbo].[DiagnosticReportExtraData]
                                               ([DiagnosticReportExtraDataId]
                                               ,[DiagnosticReportId]
                                               ,[Name]
                                               ,[ValueInt]
                                               ,[ValueString]
                                               ,[ValueLongString]
                                               ,[ValueStatus])
                                         VALUES
                                               ('{Guid.NewGuid()}'
                                               ,'{diagnosticReportId}'
                                               ,N'{repairStatus}'
                                               ,0
                                               ,N''
                                               ,N'{dtcs}'
                                               ,N'{dtcStatus}')");
                }
            }

            if (sqlInserts.Any())
            {
                using (var connection = new SqlConnection(registry.ConnectionStringDefault))
                {
                    using (var cmd = new SqlCommand(string.Join(Environment.NewLine, sqlInserts), connection))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandTimeout = 0; //Set the timeout to 0 second.
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            return recommendRepairDtcs;
        }
    }
}