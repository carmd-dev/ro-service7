using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Innova.DtcCodes
{
    public class ManufacturerMapping
    {
        public string DtcStatusManufacturerName { get; set; }
        public List<string> PolkManufacturerList { get; set; }
    }

    ////#ABSDTCs
    /// <summary>
    /// DTC repair status
    /// </summary>
    public class DTCRepairStatus
    {
        private static List<ManufacturerMapping> manufacturerMappings;

        static DTCRepairStatus()
        {
            manufacturerMappings = new List<ManufacturerMapping>
            {
                new ManufacturerMapping
                {
                    DtcStatusManufacturerName = "Chrysler",
                    PolkManufacturerList = new List<string> { "CHRYSLER", "CHRYSLER GROUP LLC" }
                },
                new ManufacturerMapping
                {
                    DtcStatusManufacturerName = "Ford",
                    PolkManufacturerList = new List<string> { "FORD" }
                },
                new ManufacturerMapping
                {
                    DtcStatusManufacturerName = "GM",
                    PolkManufacturerList = new List<string> { "GENERAL MOTORS", "GENERAL MOTORS COMPANY" }
                },
                new ManufacturerMapping
                {
                    DtcStatusManufacturerName = "Honda",
                    PolkManufacturerList = new List<string> { "HONDA", "HONDA MOTOR CO. LTD." }
                },
                new ManufacturerMapping
                {
                    DtcStatusManufacturerName = "Hyundai",
                    PolkManufacturerList = new List<string> { "HYUNDAI" }
                },
                new ManufacturerMapping
                {
                    DtcStatusManufacturerName = "Jaguar Land Rover",
                    PolkManufacturerList = new List<string> { "JAGUAR LAND ROVER LIMITED", "JAGUAR LAND ROVER" }
                },
                new ManufacturerMapping
                {
                    DtcStatusManufacturerName = "Mazda",
                    PolkManufacturerList = new List<string> { "MAZDA MOTOR CORPORATION", "MAZDA" }
                },
                new ManufacturerMapping
                {
                    DtcStatusManufacturerName = "Mercedes",
                    PolkManufacturerList = new List<string> { "MERCEDES-BENZ", "MERCEDES-BENZ AG", "MERCEDES-BENZ USA LLC" }
                },
                new ManufacturerMapping
                {
                    DtcStatusManufacturerName = "Mitsubishi",
                    PolkManufacturerList = new List<string> { "MITSUBISHI" }
                },
                new ManufacturerMapping
                {
                    DtcStatusManufacturerName = "Nissan",
                    PolkManufacturerList = new List<string> { "NISSAN", "NISSAN MOTOR CO. LTD." }
                },
                new ManufacturerMapping
                {
                    DtcStatusManufacturerName = "Subaru",
                    PolkManufacturerList = new List<string> { "SUBARU" }
                },
                new ManufacturerMapping
                {
                    DtcStatusManufacturerName = "Toyota",
                    PolkManufacturerList = new List<string> { "TOYOTA", "TOYOTA MOTOR NORTH AMERICA INC." }
                },
                new ManufacturerMapping
                {
                    DtcStatusManufacturerName = "Volkswagen",
                    PolkManufacturerList = new List<string> { "VOLKSWAGEN" }
                },
                new ManufacturerMapping
                {
                    DtcStatusManufacturerName = "Volvo",
                    PolkManufacturerList = new List<string> { "VOLVO CARS OF NORTH AMERICA LLC", "VOLVO" }
                }
            };
        }

        //public string Make { get; set; }
        public string Manufacturer { get; set; }

        public string DTCStatus { get; set; }
        public string RepairStatus { get; set; }
        public string System { get; set; }

        private static List<DTCRepairStatus> statusList = null;

        private static void LoadStatusList(Registry registry)
        {
            if (statusList == null)
            {
                statusList = new List<DTCRepairStatus>();
                const string sql = @"SELECT [Manufacturer]
                                  ,[System]
                                  ,[DTC Status]
                                  ,[System Status]

                              FROM [DTC_RepairStatus2]";
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(registry.ConnectionStringDefault))
                {
                    dr.CommandTimeout = 120;
                    dr.SqlProcedureCommand.SqlCommand.CommandText = sql;
                    dr.SqlProcedureCommand.SqlCommand.CommandType = CommandType.Text;

                    dr.Execute();

                    while (dr.Read())
                    {
                        statusList.Add(new DTCRepairStatus
                        {
                            Manufacturer = dr.GetString("Manufacturer").ToUpper().Trim(),
                            DTCStatus = dr.GetString("DTC Status").Trim(),
                            RepairStatus = dr.GetString("System Status"),
                            System = dr.GetString("System")
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Get DTC Repair Status by make and type
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="manufacturer"></param>
        /// <param name="dtcStatuses"></param>
        /// <returns></returns>
        public static DTCRepairStatus GetStatus(Registry registry,
            string manufacturer, List<string> dtcStatuses)
        {
            if (statusList == null)
                LoadStatusList(registry);

            //make correct manufacturer name using mapping
            var correctManufacturer =
                manufacturerMappings.FirstOrDefault(m => m.PolkManufacturerList.Contains(manufacturer));
            if (correctManufacturer != null)
            {
                manufacturer = correctManufacturer.DtcStatusManufacturerName;
            }

            foreach (var st in dtcStatuses)
            {
                var r = statusList.FirstOrDefault(s =>
                    //manufacturer == s.Manufacturer &&
                    string.Equals(manufacturer, s.Manufacturer, StringComparison.OrdinalIgnoreCase) &&
                    //s.DTCStatus.ToUpper().Contains(st.ToUpper()));
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
    public class OEMDTCType
    {
        public string DTC { get; set; }
        public string Title { get; set; }
        public string RepairStatus { get; set; }
        public List<string> TypeList { get; set; }
        public string Group { get; set; } //ABS/SRS/OBD1/OBD2...
        public string Type { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    public class OEMDTCTypeRepairStatus
    {
        public OEMDTCType Oemdtc { get; set; }
        public string RepairStatus { get; set; }
        public string RepairStatusText { get; set; }

        /// <summary>
        /// Get DTC Repair Status
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="dtcTypes"></param>
        /// <param name="manufacturer"></param>
        /// <returns></returns>
        public static List<OEMDTCTypeRepairStatus> GetStatuses(Registry registry, List<OEMDTCType> dtcTypes, string manufacturer)
        {
            manufacturer = manufacturer.ToUpper();
            List<OEMDTCType> unknownStatusList = new List<OEMDTCType>();

            var result = new List<OEMDTCTypeRepairStatus>();
            foreach (var dtcType in dtcTypes)
            {
                var r = new OEMDTCTypeRepairStatus
                {
                    Oemdtc = dtcType,
                    RepairStatus = dtcType.RepairStatus ?? string.Empty
                };

                //#Sprint36
                //if (dtcType.TypeList == null || !dtcType.TypeList.Any())
                //{
                //    dtcType.TypeList = new List<string> { "" };
                //}

                //var status = DTCRepairStatus.GetStatus(registry, manufacturer, dtcType.TypeList);
                //if (status != null)
                //{
                //    r.RepairStatus = status.RepairStatus;
                //    r.Oemdtc.RepairStatus = status.RepairStatus;
                //}
                //else
                //{
                //    r.RepairStatus = "Unknown";
                //    r.Oemdtc.RepairStatus = "Unknown";
                //    r.RepairStatusText = "Unknown Repair Status";
                //    //
                //    unknownStatusList.Add(dtcType);
                //    //
                //}

                if (string.IsNullOrWhiteSpace(r.RepairStatus))
                {
                    if (dtcType.TypeList == null || !dtcType.TypeList.Any())
                    {
                        dtcType.TypeList = new List<string> { "" };
                    }

                    var status = DTCRepairStatus.GetStatus(registry, manufacturer, dtcType.TypeList);
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

            //
            if (unknownStatusList.Any())
            {
                //Save to DTCUnknownStatus table for later review
                var sqls = new List<string>();
                foreach (var dtc in unknownStatusList)
                {
                    sqls.Add($@"If Not Exists(Select DTC From DTCUnknownStatus Where DTC='{dtc.DTC}'
                                    And Manufacturer = '{manufacturer}'
                                    And [DTC Status]='{string.Join(",", dtc.TypeList)}')
                                BEGIN
	                                Insert Into DTCUnknownStatus
	                                Values('{dtc.DTC}','{manufacturer}','{dtc.Group}','{string.Join(",", dtc.TypeList)}','')
                                END");
                }
                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(registry.ConnectionStringDefault))
                {
                    dr.CommandTimeout = 120;
                    dr.SqlProcedureCommand.SqlCommand.CommandText = string.Join(Environment.NewLine, sqls);
                    dr.SqlProcedureCommand.SqlCommand.CommandType = CommandType.Text;
                    dr.ExecuteNonQuery();
                }
            }

            return result;
        }
    }
}