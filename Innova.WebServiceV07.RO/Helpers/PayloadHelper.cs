using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace Innova.WebServiceV07.RO.Helpers
{
    public static class PayloadHelper
    {
        public static void SavePayloadToFile(string externalSystemName, string newPayloadFolder, string fileName, IEnumerable<string> payloadInfo)
        {
            var folderName = DateTime.Now.ToString("yyyy-MM-dd");
            var fullPath = Path.Combine(newPayloadFolder, externalSystemName, folderName);

            try
            {
                DateTime dtNow = DateTime.Now;

                //Save payload files in payload folder #2
                TimeSpan startTimePL2 = new TimeSpan(6, 0, 0);
                TimeSpan endTimePL2 = new TimeSpan(0, 11, 59, 59, 999); //Cutoff time;
                TimeSpan startNowPL2 = dtNow.TimeOfDay;

                if (startNowPL2 >= startTimePL2 && startNowPL2 <= endTimePL2) //Saves payloads to TEXT files
                {
                    fullPath = fullPath.Replace("PayloadLog", "PayloadLogV2");
                }

                //Save payload files in payload folder #3
                TimeSpan startTimePL3 = new TimeSpan(12, 0, 0);
                TimeSpan endTimePL3 = new TimeSpan(0, 17, 59, 59, 999); //Cutoff time;
                TimeSpan startNowPL3 = dtNow.TimeOfDay;

                if (startNowPL3 >= startTimePL3 && startNowPL3 <= endTimePL3) //Saves payloads to TEXT files
                {
                    fullPath = fullPath.Replace("PayloadLog", "PayloadLogV3");
                }

                //Save payload files in payload folder #4
                TimeSpan startTimePL4 = new TimeSpan(18, 0, 0);
                TimeSpan endTimePL4 = new TimeSpan(0, 23, 59, 59, 999); //Cutoff time;
                TimeSpan startNowPL4 = dtNow.TimeOfDay;

                if (startNowPL4 >= startTimePL4 && startNowPL4 <= endTimePL4) //Saves payloads to TEXT files
                {
                    fullPath = fullPath.Replace("PayloadLog", "PayloadLogV4");
                }

                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }

                File.WriteAllText(Path.Combine(fullPath, fileName), string.Join("|", payloadInfo));
            }
            catch (IOException)
            {
                fileName = $"{Guid.NewGuid().GetHashCode()}_{fileName}";
                File.WriteAllText(Path.Combine(fullPath, fileName), string.Join("|", payloadInfo));
            }
            catch (Exception fileEx)
            {
                //Write to log files
                Logger.Write(
                    "The payload could not be logged to the log text file."
                    + Environment.NewLine
                    + Environment.NewLine
                    + string.Join("|", payloadInfo)
                    + Environment.NewLine
                    + Environment.NewLine
                    + fileEx.ToString()
                );
            }
        }

        public static void SavePayloadToFileByDay(string externalSystemName, string newPayloadFolder, string fileName, IEnumerable<string> payloadInfo)
        {
            try
            {
                var folderName = DateTime.Now.ToString("yyyy-MM-dd");
                var fullPath = Path.Combine(newPayloadFolder, folderName);

                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }

                File.WriteAllText(Path.Combine(fullPath, fileName), string.Join("|", payloadInfo));
            }
            catch (Exception fileEx)
            {
                //Write to log files
                Logger.Write(
                    "SavePayloadToFileByDay: failed"
                    + Environment.NewLine
                    + Environment.NewLine
                    + string.Join("|", payloadInfo)
                    + Environment.NewLine
                    + Environment.NewLine
                    + fileEx.ToString()
                );
            }
        }

        public static IEnumerable<string> BuildPayloadInfo
            (
                string serviceName,
                string methodName,
                string reportID,
                string externalSystemUserIdString,
                string vin,
                string vehicleMileage,
                string pwrPrimaryDtc,
                string pwrStoredCodesCommaSeparatedList,
                string pwrPendingCodesCommaSeparatedList,
                string pwrPermanentCodesCommaSeparatedList,
                string obd1StoredCodesCommaSeparatedList,
                string obd1PendingCodesCommaSeparatedList,
                string absStoredCodesCommaSeparatedList,
                string absPendingCodesCommaSeparatedList,
                string srsStoredCodesCommaSeparatedList,
                string srsPendingCodesCommaSeparatedList
            )
        {
            var payloadInfo = new List<string>
            {
                serviceName,
                methodName,
                DateTime.UtcNow.ToString(),
                reportID,
                externalSystemUserIdString,
                vin,
                vehicleMileage,
                pwrPrimaryDtc,
                pwrStoredCodesCommaSeparatedList,
                pwrPendingCodesCommaSeparatedList,
                pwrPermanentCodesCommaSeparatedList,
                obd1StoredCodesCommaSeparatedList,
                obd1PendingCodesCommaSeparatedList,
                absStoredCodesCommaSeparatedList,
                absPendingCodesCommaSeparatedList,
                srsStoredCodesCommaSeparatedList,
                srsPendingCodesCommaSeparatedList
            };

            return payloadInfo;
        }

        public static IEnumerable<string> BuildPayloadInfo(string serviceName, string methodName, string reportID, string externalSystemUserIdString, string vin, string vehicleMileage, string rawToolPayload)
        {
            var payloadInfo = new List<string>
            {
                serviceName,
                methodName,
                DateTime.UtcNow.ToString(),
                reportID,
                externalSystemUserIdString,
                vin,
                vehicleMileage,
                rawToolPayload
            };

            return payloadInfo;
        }
    }
}